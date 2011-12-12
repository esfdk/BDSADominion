using BDSADominion.GUI;

namespace BDSADominion
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

