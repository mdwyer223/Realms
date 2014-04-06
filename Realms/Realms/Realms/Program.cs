using System;

namespace Realms
{
#if WINDOWS || XBOX
    static class Program
    {
        static bool running = true;

        static void Main(string[] args)
        {
            using (Game1 game = new Game1())
            {
                game.Run();
            }
            Game1.changeActive();
        }
    }
#endif
}

