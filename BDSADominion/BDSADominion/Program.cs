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
            using (GameClass gameClass = new GameClass())
            {
                gameClass.Run();
            }
        }
    }
#endif
}

