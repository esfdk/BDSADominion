namespace BDSADominion.GUI
{
    using System.Collections.Generic;
    using System.Linq;

    using BDSADominion.Gamestate;
    using BDSADominion.Gamestate.Card_Types;
    using BDSADominion.GUI.Sprites;

    /// <summary>
    /// Class responsible for talk between GUI and controller.
    /// </summary>
    /// <author>
    /// Christian 'Troy' Jensen (chrj@itu.dk)
    /// </author>
    public class GUIInterface
    {
        /// <summary>
        /// gamegclass game
        /// </summary>
        private readonly GameClass game;

        /// <summary>
        /// Initializes a new instance of the <see cref="GUIInterface"/> class.
        /// </summary>
        public GUIInterface()
        {
            game = new GameClass();
            game.HandCardClicked += HandCardToControl;
            game.SupplyCardClicked += SupplyAttemptToControl;
            game.EndPhaseClicked += EndPhaseToControl;
            game.StartUpdate += StartUpdateToControl;
            CardInHandPressed += HandPressed;
            BuyAttempt += SupplyPressed;
            EndPhasePressed += PhaseEndPressed;
            StartUpdate += UpdateHappened;
        }

        /// <summary>
        /// event is raised when index in hand is pressed.
        /// </summary>
        public event IndexHandler CardInHandPressed;

        /// <summary>
        /// Listener on cardname.
        /// </summary>
        public event CardNameHandler BuyAttempt;

        /// <summary>
        /// event is raised when endphase button is clicked.
        /// </summary>
        public event ClickHandler EndPhasePressed;

        /// <summary>
        /// event is raised when startupdate
        /// </summary>
        public event ClickHandler StartUpdate;

        /// <summary>
        /// RUN the game!.
        /// </summary>
        public void RunGame()
        {
            game.Run();
            ////string my = "lol";
        }

        /// <summary>
        /// Draw the hand.
        /// </summary>
        /// <param name="cards">
        /// The cards.
        /// </param>
        public void DrawHand(Card[] cards)
        {
            var sprite = cards.Select((t, i) => new CardSprite(t.Name, i)).ToList();
            game.HandZone.NewCards(sprite);
        }

        /// <summary>
        /// Draw the action cards.
        /// </summary>
        /// <param name="cards">
        /// The cards.
        /// </param>
        public void DrawAction(Card[] cards)
        {
            List<CardSprite> sprite = cards.Select((t, i) => new CardSprite(t.Name, i)).ToList();
            game.ActionZone.NewCards(sprite);
        }

        /// <summary>
        /// Draw the dsicard card.
        /// </summary>
        /// <param name="card">
        /// The card.
        /// </param>
        public void DrawDiscard(Card card)
        {
            if (card == null)
            {
                game.DiscardZone.SetEmpty();
            }
            else
            {
                game.DiscardZone.AddCard(new CardSprite(card.Name, -1));
            }
        }

        /// <summary>
        /// Draw the deck.
        /// </summary>
        /// <param name="filled">
        /// The filled.
        /// </param>
        public void DrawDeck(bool filled)
        {
            if (filled)
            {
                game.DeckZone.SetFilled();
            }
            else
            {
                game.DeckZone.SetEmpty();
            }
        }

        /// <summary>
        /// Set actions.
        /// </summary>
        /// <param name="number">
        /// The number.
        /// </param>
        public void SetAction(int number)
        {
            game.Actions = number;
        }

        /// <summary>
        /// Set buys.
        /// </summary>
        /// <param name="number">
        /// The number.
        /// </param>
        public void SetBuys(int number)
        {
            game.Buys = number;
        }

        /// <summary>
        /// Set coins.
        /// </summary>
        /// <param name="number">
        /// The number.
        /// </param>
        public void SetCoins(int number)
        {
            game.Coins = number;
        }

        /// <summary>
        /// Set winner id.
        /// </summary>
        /// <param name="playerId">
        /// The player id.
        /// </param>
        public void EndGame(int playerId)
        {
            game.WinnerNum = playerId;
            game.EndOfGame = true;
        }

        /// <summary>
        /// Set yourturn.
        /// </summary>
        /// <param name="yourTurn">
        /// The your turn.
        /// </param>
        public void YourTurn(bool yourTurn)
        {
            game.Turn = yourTurn;
        }

        /// <summary>
        /// Sets the phase.
        /// </summary>
        /// <param name="phase">
        /// The phase.
        /// </param>
        public void SetPhase(int phase)
        {
            game.Phase = phase;
        }

        /// <summary>
        /// adds all the used cards.
        /// </summary>
        /// <param name="cards">
        /// The cards.
        /// </param>
        public void UsedCards(CardName[] cards)
        {
            game.SupplyZone.NewCards(cards.ToList());
        }

        /// <summary>
        /// Sets playernumber.
        /// </summary>
        /// <param name="number">
        /// The number.
        /// </param>
        public void SetPlayerNumber(int number)
        {
            game.Playernum = number;
        }

        /// <summary>
        /// returns the handcard to interact with.
        /// </summary>
        /// <param name="card">
        /// The card.
        /// </param>
        private void HandCardToControl(CardSprite card)
        {
            if (card != null)
            {
                CardInHandPressed(card.Index);
            }
        }

        /// <summary>
        /// try and buy supply card.
        /// </summary>
        /// <param name="card">
        /// The card.
        /// </param>
        private void SupplyAttemptToControl(CardName card)
        {
            BuyAttempt(card);
        }

        /// <summary>
        /// Endphasebutton is pressed endphase.
        /// </summary>
        private void EndPhaseToControl()
        {
            EndPhasePressed();
        }

        /// <summary>
        /// Start the update.
        /// </summary>
        private void StartUpdateToControl()
        {
            StartUpdate();
        }

        /// <summary>
        /// Not needed class atm.
        /// </summary>
        /// <param name="index">
        /// The index.
        /// </param>
        private void HandPressed(int index)
        {
        }

        /// <summary>
        /// supply is pressed.
        /// </summary>
        /// <param name="card">
        /// The card.
        /// </param>
        private void SupplyPressed(CardName card)
        {
            ////Console.WriteLine("Pressed {0} in Supply", card);
        }

        /// <summary>
        /// endphase button pressed.
        /// </summary>
        private void PhaseEndPressed()
        {
            ////Console.WriteLine("EndPhase has been pressed");
        }

        /// <summary>
        /// update happend.
        /// </summary>
        private void UpdateHappened()
        {
            ////Console.WriteLine("Update has happened");
        }
    }
}