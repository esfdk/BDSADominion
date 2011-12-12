using System.Collections.Generic;
using BDSADominion.Gamestate;
using Microsoft.Xna.Framework.Graphics;

namespace BDSADominion.GUI
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            GUIInterface gui = new GUIInterface();
        }
    }
#endif
}

