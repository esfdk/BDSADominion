namespace BDSADominion
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using System.Net;
    using BDSADominion.Gamestate;
    using BDSADominion.Gamestate.Card_Types;
    using BDSADominion.GUI;
    using BDSADominion.Networking;
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

        private bool serverStarted = false;

        private int numberOfPlayers;

        /// <summary>
        /// The interface for communicating with the network
        /// </summary>
        private NetworkingInterface network;

        /// <summary>
        /// The gamestate used for this game.
        /// </summary>
        /// <author>
        /// Jakob Melnyk (jmel@itu.dk)
        /// </author>
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
        /// <author>
        /// Christian 'Troy' Jensen (chrj@itu.dk)
        /// </author>
        public Control()
        {
            StartNetwork(clientType());

            network.MessageReceived += ReceivePreGameMessage;

            while (!serverStarted)
            {
                string input = Console.ReadLine();

                network.PreGameMessage(input);
            }

            Console.WriteLine("game started");
        }

        /// <summary>
        /// </summary>
        /// <param name="host">
        /// The host.
        /// </param>
        /// <author>
        /// Christian 'Troy' Jensen (chrj@itu.dk)
        /// </author>
        private void StartNetwork(bool host)
        {
            if (host)
            {
                Console.WriteLine("Host Started");
                network = new NetworkingInterface();
                Console.WriteLine(network.GetServerIp());
            }
            else
            {
                IPAddress ipAddress = null;

                bool parseSuccess = false;

                while (!parseSuccess)
                {
                    Console.WriteLine("Please input IP for server:");
                    string ip = Console.ReadLine();

                    parseSuccess = IPAddress.TryParse(ip, out ipAddress);
                    if (!parseSuccess)
                    {
                        Console.WriteLine("ip not valid, try again:");
                    }
                }

                network = new NetworkingInterface(ipAddress);
                Console.WriteLine("Client started");
            }
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        /// <author>
        /// Christian 'Troy' Jensen (chrj@itu.dk)
        /// </author>
        private bool clientType()
        {
            bool host = false;
            string input = null;

            while (input == null)
            {
                Console.WriteLine("Please select server or client:");

                input = Console.ReadLine();

                if (input.Equals("client"))
                {
                    host = false;
                }
                else if (input.Equals("server"))
                {
                    host = true;
                }
                else
                {
                    Console.WriteLine("Unrecognized input");
                    input = null;
                }
            }
            return host;
        }

        /// <summary>
        /// used to recieve messages before the game starts
        /// </summary>
        /// <param name="message">
        /// The message that is being passed. Clean
        /// </param>
        /// <param name="playerId">
        /// The Id of the player that sent it.
        /// </param>
        /// <author>
        /// Christian Jensen (chrj@itu.dk)
        /// </author>
        private void ReceivePreGameMessage(string message, int playerId)
        {
            Console.WriteLine("<Interface> Client recieved {0} from {1}", message, playerId);
            if (playerId == 0 & message.Contains("<STGM>"))
            {
                string[] messageParts = message.Split(new[] { ',' });
                serverStarted = true;
                clientPlayerNumber = uint.Parse(messageParts[2]);
                network.SetNumberOfClients(int.Parse(messageParts[1]));
                numberOfPlayers = int.Parse(messageParts[1]);
                Console.WriteLine("SYSTEM: GAME STARTED. There are {1} players and you are player {0}",
                                  clientPlayerNumber, numberOfPlayers);
                SetUpGame((uint)numberOfPlayers);
            }
        }

        private Dictionary<CardName, uint> startSupply;

        /// <summary>
        /// Sets up a new game with the number of players pass as parameter.
        /// </summary>
        /// <param name="numOfPlayers">
        /// The number Of Players.
        /// </param>
        /// <author>
        /// Jakob Melnyk (jmel@itu.dk)
        /// </author>
        private void SetUpGame(uint numOfPlayers)
        {
            startSupply = new Dictionary<CardName, uint>
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

            gs = new Gamestate.Gamestate(numOfPlayers, startSupply);

            gs.ActivePlayer = gs.Players[0];

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

            network.MessageReceived += MessageFromNetwork;

            gui = new GUIInterface();
            gui.SetPlayerNumber((int)clientPlayerNumber);
            gui.EndPhasePressed += EndPhase;
            gui.BuyAttempt += CanBuyCard;
            gui.CardInHandPressed += CanPlayCard;

            StartTurn();
            gui.RunGame();
        }

        /// <summary>
        /// Updates the GUI with new values.
        /// </summary>
        /// <author>
        /// Jakob Melnyk (jmel@itu.dk)
        /// </author>
        private void UpdateGui()
        {
            gui.DrawAction(gs.ActivePlayer.Played.ToArray());
            gui.DrawDiscard(gs.ActivePlayer.DiscardSize != 0 ? gs.ActivePlayer.TopOfDiscard : null);
            gui.DrawHand(gs.Players[(int)clientPlayerNumber - 1].Hand.ToArray());
            gui.UsedCards(startSupply.Keys.ToArray());

            if (gs.ActivePlayer.PlayerNumber == clientPlayerNumber)
            {
                gui.SetAction((int)gs.NumberOfActions);
                gui.SetBuys((int)gs.NumberOfBuys);
                gui.SetCoins((int)gs.NumberOfCoins);

                switch (gs.GetPhase)
                {
                    case 1:
                        gui.SetPhase(0);
                        break;
                    case 2:
                        gui.SetPhase(1);
                        break;
                }

                gui.YourTurn(true);
            }
            else
            {
                gui.YourTurn(false);
            }
        }

        /// <summary>
        /// Called when game finishes.
        /// </summary>
        /// <author>
        /// Jakob Melnyk (jmel@itu.dk)
        /// </author>
        private void EndOfGame()
        {
            List<int> scores = gs.GetScores();
            gui.EndGame(scores.IndexOf(scores.Max()) + 1); // TODO: In case of a draw, lowest numbered player wins.
        }

        #region TurnMethods

        /// <summary>
        /// Starts the turn of the next player.
        /// </summary>
        /// <author>
        /// Jakob Melnyk (jmel@itu.dk)
        /// </author>
        private void StartTurn()
        {
            if (gs.ActivePlayer.PlayerNumber == gs.Players.Count)
            {
                gs.ActivePlayer = gs.Players[0];
            }
            else
            {
                gs.ActivePlayer = gs.Players[(int)gs.ActivePlayer.PlayerNumber];
            }

            gs.StartActionPhase();

            UpdateGui();
        }

        /// <summary>
        /// Ends the turn of the active player.
        /// </summary>
        /// <author>
        /// Jakob Melnyk (jmel@itu.dk)
        /// </author>
        private void EndTurn()
        {
            Contract.Requires(!gs.InActionPhase & !gs.InBuyPhase);

            if (gs.GameOver)
            {
                EndOfGame();
            }
            else
            {
                CleanUp();
                StartTurn();
            }

            if (gs.ActivePlayer.PlayerNumber == clientPlayerNumber)
            {
                network.TurnMessage("!ep");
            }
        }

        /// <summary>
        /// Cleans up the player and other areas.
        /// </summary>
        /// <author>
        /// Jakob Melnyk (jmel@itu.dk)
        /// </author>
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
        /// <author>
        /// Jakob Melnyk (jmel@itu.dk)
        /// </author>
        private void CardPlayed(int handIndex)
        {
            Player p = gs.ActivePlayer;
            Card card = p.Hand[handIndex];
            switch (card.Name)
            {
                case CardName.Village:
                    p.DrawCards(1);
                    gs.NumberOfActions += 2;
                    break;

                case CardName.Woodcutter:
                    gs.NumberOfBuys += 1;
                    gs.NumberOfCoins += 2;
                    break;

                case CardName.Smithy:
                    p.DrawCards(3);
                    break;

                case CardName.CouncilRoom:
                    foreach (Player player in gs.Players)
                    {
                        player.DrawCards(1);
                    }

                    p.DrawCards(3);
                    gs.NumberOfBuys += 1;
                    break;

                case CardName.Festival:
                    gs.NumberOfActions += 2;
                    gs.NumberOfBuys += 1;
                    gs.NumberOfCoins += 2;
                    break;

                case CardName.Laboratory:
                    p.DrawCards(2);
                    gs.NumberOfActions += 1;
                    break;

                case CardName.Market:
                    p.DrawCards(1);
                    gs.NumberOfActions += 1;
                    gs.NumberOfBuys += 1;
                    gs.NumberOfCoins += 1;
                    break;

                case CardName.Adventurer:
                    int numberOfTreasures = 0;
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
                    p.DrawCards(2);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            p.MoveFromHandToTemporary(p.Hand[handIndex]);
            p.MoveFromTemporaryToZone(p.TempZone[p.TempZone.Count - 1], Zone.Played);
            gs.NumberOfActions = gs.NumberOfActions - 1;

            if (gs.NumberOfActions == 0 | gs.ActivePlayer.Hand.Count(c => c is Action) == 0)
            {
                gs.EndActionPhase();
                gs.StartBuyPhase();
            }

            UpdateGui();
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
        /// <author>
        /// Jakob Melnyk (jmel@itu.dk)
        /// </author>
        private void BuyCard(uint playerNumber, CardName cardName)
        {
            gs.PlayerGainsCard(gs.Players[(int)playerNumber - 1], cardName);

            gs.NumberOfBuys = gs.NumberOfBuys - 1;
            gs.NumberOfCoins = gs.NumberOfCoins - cardCost[cardName];

            if (gs.NumberOfBuys == 0)
            {
                gs.EndBuyPhase();
                EndTurn();
            }

            UpdateGui();
        }

        #endregion

        #region Delegates

        /// <summary>
        /// Delegate used for messages received from the network.
        /// </summary>
        /// <param name="message">
        /// The message received from the server.
        /// </param>
        /// <param name="sender">
        /// The id of the client that sent the message.
        /// </param>
        /// <author>
        /// Jakob Melnyk (jmel@itu.dk)
        /// </author>
        private void MessageFromNetwork(string message, int sender)
        {
            if (message.Substring(0, 3).Equals("!cp"))
            {
                string msg = message.Substring(message.IndexOf("["), message.IndexOf("]") - message.IndexOf("[") - 1);
                CardPlayed(int.Parse(msg));
            }

            if (message.Substring(0, 3).Equals("!bc"))
            {
                string msg = message.Substring(message.IndexOf("["), message.IndexOf("]") - message.IndexOf("[") - 1);
                CardName cardOut;
                if (!Enum.TryParse(msg, out cardOut))
                {
                    throw new Exception("Could not parse the CardName from server.");
                }

                BuyCard((uint)sender, cardOut);
            }

            if (message.Substring(0, 3).Equals("!ep"))
            {
                switch (gs.GetPhase)
                {
                    case 0:
                        break;
                    case 1:
                        gs.EndActionPhase();
                        gs.StartBuyPhase();
                        break;
                    case 2:
                        gs.EndBuyPhase();
                        EndTurn();
                        break;
                }
            }
        }

        /// <summary>
        /// Delegate for BuyAttempt. Checks if it is possible for the player to buy a specific card.
        /// </summary>
        /// <param name="cardName">
        /// The name of the card being checked.
        /// </param>
        /// <author>
        /// Jakob Melnyk (jmel@itu.dk)
        /// </author>
        private void CanBuyCard(CardName cardName)
        {
            if (gs.ActivePlayer.PlayerNumber == clientPlayerNumber & cardName != CardName.Empty & cardName != CardName.Backside)
            {
                if (gs.NumberOfCoins >= cardCost[cardName] & gs.Supply[cardName] != 0)
                {
                    network.TurnMessage("!bc [" + cardName + "]");
                    BuyCard(clientPlayerNumber, cardName);
                }
            }
        }

        /// <summary>
        /// Delegate for CardInHandPressed. Checks if the card is playable.
        /// </summary>
        /// <param name="handIndex">
        /// The index of the card in the hand of the active player.
        /// </param>
        /// <author>
        /// Jakob Melnyk (jmel@itu.dk)
        /// </author>
        private void CanPlayCard(int handIndex)
        {
            if (gs.ActivePlayer.PlayerNumber == clientPlayerNumber)
            {
                Card card = gs.ActivePlayer.Hand[handIndex];

                if (card is Action)
                {
                    network.TurnMessage("!cp [" + handIndex + "]");
                    CardPlayed(handIndex);
                }
            }
        }

        /// <summary>
        /// Delegate for the EndPhase button.
        /// </summary>
        /// <author>
        /// Jakob Melnyk (jmel@itu.dk)
        /// </author>
        private void EndPhase()
        {
            Console.WriteLine("EndPhase Called"); //TODO Remove
            if (gs.ActivePlayer.PlayerNumber == clientPlayerNumber)
            {
                switch (gs.GetPhase)
                {
                    case 0:
                        break;
                    case 1:
                        gs.EndActionPhase();
                        gs.StartBuyPhase();
                        network.TurnMessage("!ep");
                        break;
                    case 2:
                        gs.EndBuyPhase();
                        EndTurn();
                        break;
                }
            }
            UpdateGui(); //TODO Remove

        }

        #endregion
    }
}