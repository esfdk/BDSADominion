using BDSADominion.Gamestate.Card_Types;

namespace BDSADominion.GUI
{
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

        public GUIInterface()
        {
            game = new GameClass();
            game.Run();
        }

        //public DrawHand(List<Card> ) TODO
    }
}
