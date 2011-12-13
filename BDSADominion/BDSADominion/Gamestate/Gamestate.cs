namespace BDSADominion.Gamestate
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using BDSADominion.Gamestate.Card_Types;

    /// <summary>
    /// Keeps track of the players and everything the players share, such as the trash pile and the supply.
    /// </summary>
    /// <author>
    /// Jakob Melnyk (jmel@itu.dk)
    /// </author>
    public class Gamestate
    {
        #region Fields

        /// <summary>
        /// The amount of piles that are empty.
        /// </summary>
        private uint numberOfEmptyPiles;

        /// <summary>
        /// Number Of Actions the active player has.
        /// </summary>
        private uint numberOfActions;

        /// <summary>
        /// Number Of Buys the active player has.
        /// </summary>
        private uint numberOfBuys;

        /// <summary>
        /// Number Of Coins the active player has.
        /// </summary>
        private uint numberOfCoins;

        /// <summary>
        /// The player currently interacting with the game.
        /// </summary>
        private Player activePlayer;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="Gamestate"/> class.
        /// </summary>
        /// <param name="numberOfPlayers">
        /// The number of players in the game.
        /// </param>
        /// <param name="startSupply">
        /// The start supply of the Dominion game.
        /// </param>
        public Gamestate(uint numberOfPlayers, Dictionary<CardName, uint> startSupply)
        {
            // TODO: Contract.Requires(numberOfPlayers >= 2 & numberOfPlayers <= 4);
            Contract.Requires(startSupply != null);

            Players = new List<Player>();
            for (uint i = 1; i <= numberOfPlayers; i++)
            {
                Players.Add(new Player(i));
            }

            Supply = new Dictionary<CardName, uint>();
            foreach (CardName cn in startSupply.Keys)
            {
                Supply.Add(cn, startSupply[cn]);
            }
        }

        #region Properties
        /// <summary>
        /// Gets the Players in the game.
        /// </summary>
        public List<Player> Players { get; private set; }

        /// <summary>
        /// Gets the Number Of Players in the game.
        /// </summary>
        public uint NumberOfPlayers
        {
            get
            {
                // TODO: Contract.Ensures(Contract.Result<uint>() >= 2 & Contract.Result<uint>() <= 4);
                return (uint)Players.Count;
            }
        }

        /// <summary>
        /// Gets or sets ActivePlayer.
        /// </summary>
        public Player ActivePlayer
        {
            get
            {
                return activePlayer;
            }

            set
            {
                Contract.Requires(value.PlayerNumber >= 1 & value.PlayerNumber <= NumberOfPlayers);
                activePlayer = value;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the player is in the action phase.
        /// </summary>
        public bool InActionPhase { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the player is in the buy phase.
        /// </summary>
        public bool InBuyPhase { get; private set; }

        /// <summary>
        /// Gets the current phase the active player is in. 1 for Action Phase, 2 for BuyPhase.
        /// </summary>
        public uint GetPhase
        {
            get
            {
                if (InActionPhase)
                {
                    return 1;
                }

                if (InBuyPhase)
                {
                    return 2;
                }

                return 0;
            }
        }

        /// <summary>
        /// Gets the Supply.
        /// </summary>
        public Dictionary<CardName, uint> Supply { get; private set; }

        /// <summary>
        /// Gets the Trash pile.
        /// </summary>
        public List<Card> Trash { get; private set; }

        /// <summary>
        /// Gets or sets Number Of Actions the active player has.
        /// </summary>
        public uint NumberOfActions
        {
            get
            {
                return numberOfActions;
            }

            set
            {
                Contract.Requires(!(NumberOfActions + value < 0));
                numberOfActions = value;
            }
        }

        /// <summary>
        /// Gets or sets Number Of Buys the active player has.
        /// </summary>
        public uint NumberOfBuys
        {
            get
            {
                return numberOfBuys;
            }

            set
            {
                Contract.Requires(!(NumberOfBuys + value < 0));
                numberOfBuys = value;
            }
        }

        /// <summary>
        /// Gets or sets Number Of Coins the active player has.
        /// </summary>
        public uint NumberOfCoins
        {
            get
            {
                return numberOfCoins;
            }

            set
            {
                Contract.Requires(!(NumberOfCoins + value < 0));
                numberOfCoins = value;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the game is over.
        /// </summary>
        public bool GameOver { get; private set; }

        #endregion

        /// <summary>
        /// Starts the action phase for the active player.
        /// </summary>
        public void StartActionPhase()
        {
            Contract.Requires(!InActionPhase & !InBuyPhase);
            Contract.Ensures(InActionPhase & !InBuyPhase);

            InActionPhase = true;
            NumberOfActions = 1;
            NumberOfBuys = 1;
        }

        /// <summary>
        /// Ends the action phase for the active player.
        /// </summary>
        public void EndActionPhase()
        {
            Contract.Requires(InActionPhase & !InBuyPhase);
            Contract.Ensures(!InActionPhase & !InBuyPhase);

            InActionPhase = false;
            NumberOfActions = 0;
        }

        /// <summary>
        /// Starts the buy phase for the active player.
        /// </summary>
        public void StartBuyPhase()
        {
            Contract.Requires(!InActionPhase & !InBuyPhase);
            Contract.Ensures(!InActionPhase & InBuyPhase);

            InBuyPhase = true;

            ICollection<Card> hand = ActivePlayer.Hand;
            int numCopper = hand.Count(card => card.Name == CardName.Copper);
            int numSilver = hand.Count(card => card.Name == CardName.Silver);
            int numGold = hand.Count(card => card.Name == CardName.Gold);

            numberOfCoins += (uint)(numCopper + (2 * numSilver) + (3 * numGold));
        }

        /// <summary>
        /// End the buy phase for the active player.
        /// </summary>
        public void EndBuyPhase()
        {
            Contract.Requires(!InActionPhase & InBuyPhase);
            Contract.Ensures(!InActionPhase & !InBuyPhase);

            InBuyPhase = false;
            NumberOfCoins = 0;
            NumberOfBuys = 0;
        }

        /// <summary>
        /// Does the CleanUp phase for the player.
        /// </summary>
        public void DoCleanUp()
        {
            ActivePlayer.CleanUp();
        }

        /// <summary>
        /// Player gains a card from the supply.
        /// </summary>
        /// <param name="player">
        /// The player gaining the card.
        /// </param>
        /// <param name="card">
        /// The card the player gains from the supply.
        /// </param>
        public void PlayerGainsCard(Player player, CardName card)
        {
            Contract.Requires(Players.Contains(player));
            Contract.Requires(Supply[card] != 0);

            Contract.Ensures(player.TopOfDiscard.Name == card);

            player.AddCardToZone(CardFactory.CreateCard(card), Zone.Discard);
            Supply[card]--;

            if (Supply[card] == 0)
            {
                if (card == CardName.Province)
                {
                    GameOver = true;
                }
                else
                {
                    numberOfEmptyPiles++;
                }
            }

            if (numberOfEmptyPiles == 3)
            {
                GameOver = true;
            }

        }

        /// <summary>
        /// A list containing the scores of the players.
        /// </summary>
        /// <returns>
        /// The scores of all the players.
        /// </returns>
        public List<int> GetScores()
        {
            List<int> scores = new List<int>();

            for (int i = 0; i < Players.Count - 1; i++)
            {
                ICollection<Card> allCards = Players[i].AllCards.Keys;

                int numEstate = allCards.Count(card => card.Name == CardName.Estate);
                int numDuchy = allCards.Count(card => card.Name == CardName.Duchy);
                int numProvince = allCards.Count(card => card.Name == CardName.Province);
                int numCurse = allCards.Count(card => card.Name == CardName.Curse);
                int numGardens = allCards.Count(card => card.Name == CardName.Gardens);

                int score = numEstate + (3 * numDuchy) + (6 * numProvince) + ((allCards.Count / 10) * numGardens) - numCurse;
                scores.Add(score);
            }

            return scores;
        }
    }
}
