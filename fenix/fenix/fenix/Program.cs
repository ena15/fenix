using System;

namespace fenix
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (GameLevel game = new GameLevel())
            {
                game.Run();
            }
        }
    }
#endif
}

