namespace BDSADominion
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    using BDSADominion.Gamestate;
    using BDSADominion.Gamestate.Card_Types;
    using BDSADominion.GUI;

    using Action = BDSADominion.Gamestate.Card_Types.Action;

    /// <summary>
    /// The main class of the game. Contains the network, GUI, gamestate and logic.
    /// </summary>
    public class Control
    {
        #region Fields
        /// <summary>
        /// The gui used by this client.
        /// </summary>
        private GUIInterface gui;

        /// <summary>
        /// The gamestate used for this game.
        /// </summary>
        private Gamestate.Gamestate gs;

        /// <summary>
        /// The player number of this client.
        /// </summary>
        private uint clientPlayerNumber;

        #region CardCost

        /// <summary>
        /// Used to determine costs of cards.
        /// </summary>
        private Dictionary<CardName, uint> cardCost = new Dictionary<CardName, uint>
                {
                    // The treasure cards.
                    { CardName.Copper, 0 },
                    { CardName.Silver, 3 },
                    { CardName.Gold, 6 },

                    // The victory cards.
                    { CardName.Curse, 0 },
                    { CardName.Estate, 2 },
                    { CardName.Duchy, 5 },
                    { CardName.Province, 8 },
                    
                    // The kingdom cards.
                    // Cost : 2
                    { CardName.Moat, 2 },

                    // Cost : 3
                    { CardName.Village, 3 },
                    { CardName.Woodcutter, 3 },

                    // Cost : 4
                    { CardName.Gardens, 4 },
                    { CardName.Smithy, 4 },

                    // Cost : 5
                    { CardName.CouncilRoom, 5 },
                    { CardName.Laboratory, 5 },
                    { CardName.Festival, 5 },
                    { CardName.Market, 5 },
                    
                    // Cost : 6
                    { CardName.Adventurer, 6 }
                };
        #endregion

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="Control"/> class.
        /// </summary>
        public Control()
        {
            gui = new GUIInterface();
        }

        /// <summary>
        /// Sets up a new game with the number of players pass as parameter.
        /// </summary>
        /// <param name="numberOfPlayers">
        /// The number Of Players.
        /// </param>
        private void SetUpGame(uint numberOfPlayers)
        {
            Dictionary<CardName, uint> startSupply = new Dictionary<CardName, uint>
                {
                    // The treasure cards.
                    { CardName.Copper, 51 },
                    { CardName.Silver, 30 },
                    { CardName.Gold, 30 },

                    // The victory cards.
                    { CardName.Curse, 15 },
                    { CardName.Estate, 19 },
                    { CardName.Duchy, 10 },
                    { CardName.Province, 10 },
                    
                    // The kingdom cards.
                    { CardName.Adventurer, 10 },
                    { CardName.CouncilRoom, 10 },
                    { CardName.Festival, 10 },
                    { CardName.Gardens, 10 },
                    { CardName.Laboratory, 10 },
                    { CardName.Moat, 10 },
                    { CardName.Market, 10 },
                    { CardName.Smithy, 10 },
                    { CardName.Village, 10 },
                    { CardName.Woodcutter, 10 }
                };

            CardFactory.SetUpCards(startSupply.Keys);

            gs = new Gamestate.Gamestate(numberOfPlayers, startSupply);

            foreach (Player player in gs.Players)
            {
                for (int i = 1; i < 7; i++)
                {
                    gs.PlayerGainsCard(player, CardName.Copper);
                }

                for (int i = 1; i < 3; i++)
                {
                    gs.PlayerGainsCard(player, CardName.Estate);
                }

                player.DrawCards(5);
            }

            StartTurn();
        }

        /// <summary>
        /// Updates the GUI with new values.
        /// </summary>
        private void UpdateGui()
        {
            // TODO: Not implemented yet.
        }

        #region EventCheckers

        /// <summary>
        /// Checks if it is possible for the player to buy a specific card.
        /// </summary>
        /// <param name="cardName">
        /// The name of the card being checked.
        /// </param>
        private void CanBuyCard(CardName cardName)
        {
            if (gs.NumberOfCoins >= cardCost[cardName])
            {
                BuyCard(clientPlayerNumber, cardName);
            }

            // TODO: Send to server.
        }

        /// <summary>
        /// Checks if the card is playable.
        /// </summary>
        /// <param name="handIndex">
        /// The index of the card in the hand of the active player.
        /// </param>
        private void CanPlayCard(int handIndex)
        {
            Card card = this.gs.ActivePlayer.Hand[handIndex];

            if (card is Action)
            {
                CardPlayed(handIndex);
            }
            else
            {
                // TODO: Remove this after testing
                Console.WriteLine("Card is not an action card!");
            }

            // TODO: Send to server.
        }

        #endregion

        #region TurnMethods
        /// <summary>
        /// Starts the turn of the next player.
        /// </summary>
        private void StartTurn()
        {
            if (gs.ActivePlayer.PlayerNumber == gs.Players.Count | gs.ActivePlayer == null)
            {
                gs.ActivePlayer = gs.Players[0];
            }
            else
            {
                gs.ActivePlayer = gs.Players[(int)gs.ActivePlayer.PlayerNumber];
            }

            gs.StartActionPhase();

            // TODO: Set GUI one-time stuff.
            UpdateGui();
        }

        /// <summary>
        /// Ends the turn of the active player.
        /// </summary>
        private void EndTurn()
        {
            Contract.Requires(!gs.InActionPhase & !gs.InBuyPhase);

            // TODO: Send end turn message to server.
            CleanUp();
            StartTurn();
        }

        /// <summary>
        /// Cleans up the player and other areas.
        /// </summary>
        private void CleanUp()
        {
            gs.ActivePlayer.CleanUp();
            UpdateGui();
        }

        #endregion

        #region PlayerActions

        /// <summary>
        /// Plays a card from the hand of the player indicated.
        /// </summary>
        /// <param name="handIndex">
        /// The index of the card in the hand.
        /// </param>
        private void CardPlayed(int handIndex)
        {
            Card card = this.gs.ActivePlayer.Hand[handIndex];
            switch (card.Name)
            {
                case CardName.Village:
                    gs.ActivePlayer.DrawCards(1);
                    gs.NumberOfActions += 2;
                    break;

                case CardName.Woodcutter:
                    gs.NumberOfBuys += 1;
                    gs.NumberOfCoins += 2;
                    break;

                case CardName.Smithy:
                    gs.ActivePlayer.DrawCards(3);
                    break;

                case CardName.CouncilRoom:
                    foreach (Player player in gs.Players)
                    {
                        player.DrawCards(1);
                    }

                    gs.ActivePlayer.DrawCards(3);
                    gs.NumberOfBuys += 1;
                    break;

                case CardName.Festival:
                    gs.NumberOfActions += 2;
                    gs.NumberOfBuys += 1;
                    gs.NumberOfCoins += 2;
                    break;

                case CardName.Laboratory:
                    gs.ActivePlayer.DrawCards(2);
                    gs.NumberOfActions += 1;
                    break;

                case CardName.Market:
                    gs.ActivePlayer.DrawCards(1);
                    gs.NumberOfActions += 1;
                    gs.NumberOfBuys += 1;
                    gs.NumberOfCoins += 1;
                    break;

                case CardName.Adventurer:
                    int numberOfTreasures = 0;
                    Player p = gs.ActivePlayer;
                    int temporarySizeAtStart = p.TempZone.Count; // In most cases will be zero.

                    while (numberOfTreasures < 2 & !(p.DeckSize == 0 & p.DiscardSize == 0))
                    {
                        p.MoveFromZoneToTemporary(Zone.Deck);
                        Card c = p.TempZone[p.TempZone.Count - 1];
                        if (c is Treasure)
                        {
                            p.MoveFromTemporaryToZone(c, Zone.Hand);
                            numberOfTreasures++;
                        }
                    }

                    while (p.TempZone.Count > temporarySizeAtStart)
                    {
                        Card c = p.TempZone[p.TempZone.Count - 1];
                        p.MoveFromTemporaryToZone(c, Zone.Discard);
                    }

                    break;

                case CardName.Moat:
                    gs.ActivePlayer.DrawCards(2);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (gs.NumberOfActions == 0)
            {
                gs.EndActionPhase();
            }
        }

        /// <summary>
        /// Makes a player buy a card.
        /// </summary>
        /// <param name="playerNumber">
        /// The number of the player buying a card.
        /// </param>
        /// <param name="cardName">
        /// The card name to be bought.
        /// </param>
        private void BuyCard(uint playerNumber, CardName cardName)
        {
            gs.PlayerGainsCard(gs.Players[(int)playerNumber - 1], cardName);

            if (gs.NumberOfBuys == 0)
            {
                gs.EndBuyPhase();
                EndTurn();
            }
        }

        #endregion

        #region Delegates

        /// <summary>
        /// Delegates used for the 
        /// </summary>
        /// <param name="message">
        /// The message received from the server.
        /// </param>
        /// <param name="sender">
        /// The sender.
        /// </param>
        private void MessageFromServer(string message, int sender)
        {

        }
        #endregion
    }
}