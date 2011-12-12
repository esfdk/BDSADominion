using BDSADominion.Gamestate;
using BDSADominion.Gamestate.Card_Types;
using BDSADominion.Networking;

namespace BDSADominion
{
    using Microsoft.Xna.Framework.Graphics;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

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
            game.Run();

            game.discardZone.SetEmpty();
            game.deckZone.SetFilled();

            game.HandCardClicked += HandCardToControl;
        }

        public event PressedHandIndex CardInHandPressed;

        void HandCardToControl(CardSprite card)
        {
            CardInHandPressed(card.Index);
        }

        //public DrawHand(List<Card> ) TODO
    }
}
