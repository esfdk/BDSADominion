using BDSADominion.Gamestate;
using BDSADominion.Gamestate.Card_Types;
using BDSADominion.Networking;

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

        /// <summary>
        /// trolol to
        /// </summary>
        ////private Server server = new Server();

        public GUIInterface()
        {


            game = new GameClass();
            game.Run();

            game.discardZone.AddCard(new CardSprite(CardName.Empty));
            game.deckZone.AddCard(new CardSprite(CardName.Backside));
        }

        //public DrawHand(List<Card> ) TODO
    }
}
