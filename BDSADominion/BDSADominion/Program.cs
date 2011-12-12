using System;
using System.Collections.Generic;
using System.Net;
using BDSADominion.Gamestate;
using BDSADominion.Networking;
using Microsoft.Xna.Framework.Graphics;

namespace BDSADominion.GUI
{
#if WINDOWS || XBOX
    static class Program
    {
        private static NetworkingInterface network;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            string input = null;

            while (input == null)
            {
                Console.WriteLine("Please select server or client:");

                input = Console.ReadLine();

                if (input.Equals("client"))
                {
                    runClient();
                }
                else if (input.Equals("server"))
                {
                    runHost();
                }
                else
                {
                    Console.WriteLine("Unrecognized input");
                    input = null;
                }
            }

            startGUI();
        }

        static void startGUI()
        {
            GUIInterface gui = new GUIInterface();
        }

        static void runHost()
        {
            Console.WriteLine("Host Started");
            network = new NetworkingInterface();
            Console.WriteLine(network.GetServerIp());

            while (true)
            {
                string input = Console.ReadLine();

                network.TurnMessage(input);
            }
        }

        static void runClient()
        {
            Console.WriteLine("Client started");
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
            network.MessageReceived += MessageRecieved;


            while (true)
            {
                string input = Console.ReadLine();

                network.TurnMessage(input);
            }
            
        }

        static void MessageRecieved(string message, int playerId)
        {
            Console.WriteLine("<Interface> Client recieved {0} from {1}", message, playerId);
        }
    }
#endif
}

