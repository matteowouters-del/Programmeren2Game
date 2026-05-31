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
        int refreshRate = 20;
        Console.CursorVisible = false;

        Reset(game);
        game.Draw();
        Console.SetCursorPosition(0, 0);

        while (game.CurrentGameState != GameState.Quit)
        {
            // redraw full screen only when not in gameplay
            if (game.CurrentGameState != GameState.Playing)
            {
                game.Draw();
            }

            game.Input();
            game.Update();

            Thread.Sleep(1000 / refreshRate);
        }
    }

    protected static void Reset(Game game)
    {
        // resets background and redraws the first screen
        Console.BackgroundColor = ConsoleColor.Black;
        Console.SetCursorPosition(0, 0);
        game.Draw();
        Console.SetCursorPosition(0, 0);
    }
}