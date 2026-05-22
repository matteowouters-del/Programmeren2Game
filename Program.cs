using System.Diagnostics;

namespace ProjectGame2526;

class Program
{

    static void Main(string[] args)
    {
        // create the game
        Game game = new Game();

        // start the game loop
        RunGameLoop(game);
    }

    protected static void RunGameLoop(Game game)
    {
        /*Console.BackgroundColor = ConsoleColor.DarkBlue;
        Console.ForegroundColor = ConsoleColor.Gray;*/

        int refreshRate = 20;

        Console.CursorVisible = false;

        /*Console.BackgroundColor = ConsoleColor.DarkBlue;
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.Clear();*/

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        Reset(game);

        game.Draw();
        Console.SetCursorPosition(0, 0);

        while (true)
        {
            double dt = stopwatch.Elapsed.TotalSeconds;
            game.Update(dt);
            bool playerMoved = game.MovePlayer();
            if (playerMoved)
            {
              game.MoveEnemy(dt);  
            }
            Thread.Sleep(1000 / refreshRate);

            Reset(game);
            game.Draw();
        }
    }

    protected static void Reset(Game game)
    {
        Console.BackgroundColor = ConsoleColor.Black;
        //Console.ForegroundColor = ConsoleColor.Black;
        Console.SetCursorPosition(0, 0);
        game.DrawMaze();
        /*for (int y = 0; y < game.GetHeight(); ++y)
        {
            for (int x = 0; x < game.GetWidth(); ++x)
            {
                Console.Write(" ");
            }
            Console.WriteLine();
        }
        */
        Console.SetCursorPosition(0, 0);
    }
}