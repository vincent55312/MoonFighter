using System;

namespace MoonFighter
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new MoonFighter())
                game.Run();
        }
    }
}