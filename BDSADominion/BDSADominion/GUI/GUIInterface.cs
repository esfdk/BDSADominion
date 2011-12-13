using System;
using System.Collections.Generic;
using System.Linq;
using BDSADominion.Gamestate;
using BDSADominion.Gamestate.Card_Types;

namespace BDSADominion.GUI
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    /// <author>
    /// Christian 'Troy' Jensen (chrj@itu.dk)
    /// </author>
    public class GUIInterface
    {
        private GameClass game;

        /// <summary>
        /// Initializes a new instance of the <see cref="GUIInterface"/> class. 
        /// </summary>

        public GUIInterface()
        {
            game = new GameClass();

            game.HandCardClicked += HandCardToControl;
            game.SupplyCardClicked += SupplyAttemptToControl;
            game.EndPhaseClicked += EndPhaseToControl;
            CardInHandPressed += HandPressed;
            BuyAttempt += SupplyPressed;
            EndPhasePressed += PhaseEndPressed;
        }

        public void RunGame()
        {
            game.Run();
            string my = "lol";

        }

        public event IndexHandler CardInHandPressed;

        public event CardNameHandler BuyAttempt;

        public event ClickHandler EndPhasePressed;

        void HandCardToControl(CardSprite card)
        {
            if (card != null)
            {
                CardInHandPressed(card.Index);
            }
        }

        void SupplyAttemptToControl(CardName card)
        {
            BuyAttempt(card);
        }

        void EndPhaseToControl()
        {
            EndPhasePressed();
        }

        //Unnessecary //TODO
        void HandPressed(int index)
        {
            Console.WriteLine("Pressed index {0} in Hand", index);
        }

        void SupplyPressed(CardName card)
        {
            Console.WriteLine("Pressed {0} in Supply", card);
        }

        void PhaseEndPressed()
        {
            Console.WriteLine("EndPhase has been pressed");
        }

        public void DrawHand(Card[] cards)
        {
            List<CardSprite> sprite = new List<CardSprite>();

            for (int i = 0; i < cards.Length; i++)
            {
                sprite.Add(new CardSprite(cards[i].Name, i));
            }

            game.handZone.NewCards(sprite);
        }

        public void DrawAction(Card[] cards)
        {
            game.supplyZone.NewCards(cards.Select(card => card.Name).ToList());
        }

        public void DrawDiscard(Card card)
        {
            if (card == null)
            {
                game.discardZone.SetEmpty();
            }
            else
            {
                game.discardZone.AddCard(new CardSprite(card.Name, -1));
            }

        }

        public void SetAction(int number)
        {
            game.actions = number;
        }

        public void SetBuys(int number)
        {
            game.buys = number;
        }

        public void SetCoins(int number)
        {
            game.coins = number;
        }

        public void EndGame(int playerId)
        {
            game.winnerNum = playerId;
            game.endOfGame = true;
        }

        public void YourTurn(bool yourTurn)
        {
            game.turn = yourTurn;
        }

        public void SetPhase(int phase)
        {
            game.phase = phase;
        }

        public void UsedCards(CardName[] cards)
        {
            game.supplyZone.NewCards(cards.ToList());
        }

        public void SetPlayerNumber(int number)
        {
            game.playernum = number;
        }
    }
}
