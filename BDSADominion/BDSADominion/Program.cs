using System;
using System.Collections.Generic;
using System.Net;
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
            IPAddress ipAddress = null;

            while (false)
            {
                string ip = Console.ReadLine();
                bool parseSuccess = false;

                Console.WriteLine("Please input IP for server:");

                while (!parseSuccess)
                {
                    parseSuccess = IPAddress.TryParse(ip, out ipAddress);
                    if (!parseSuccess)
                    {
                        Console.WriteLine("ip not valid, try again:");
                    }
                }
            }

            startGUI();
        }

        static void startGUI()
        {
            GUIInterface gui = new GUIInterface();
        }
    }
#endif
}

