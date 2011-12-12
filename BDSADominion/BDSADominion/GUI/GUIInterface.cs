﻿using System;
using System.Collections.Generic;
using System.Linq;
using BDSADominion.Gamestate;
using BDSADominion.Gamestate.Card_Types;

namespace BDSADominion.GUI
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class GUIInterface
    {
        private GameClass game;

        /// <summary>
        /// trolol to
        /// </summary>
        ////private Server server = new Server();

        public GUIInterface()
        {
            game = new GameClass();

            game.HandCardClicked += HandCardToControl;
            game.SupplyCardClicked += SupplyAttemptToControl;
            game.EndPhaseClicked += EndPhaseToControl;
            CardInHandPressed += HandPressed;
            BuyAttempt += 

            

            RunGame();
        }

        public void RunGame()
        {
            game.Run();
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
            Console.WriteLine("TROLOLO pressed " + index);
        }

        void SupplyPressed(CardName card)
        {
            Console.WriteLine("TROLOLO pressed " + card);
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

        public void DrawDiscard(Card card, int index)
        {
            if (card == null)
            {
                game.discardZone.SetEmpty();
            }
            else
            {
                game.discardZone.AddCard(new CardSprite(card.Name, index));
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
    }
}
