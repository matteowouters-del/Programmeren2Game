using System;

namespace ProjectGame2526;

class Game
{
    Maze maze = new Maze();
    Player player = new Player(1,1,'@',ConsoleColor.Yellow);
    Enemy enemy = new Enemy(1,1,6,15,'E',ConsoleColor.Red);
    //protected int width, height;
    //protected int playerPosX = 5, playerPosY = 5;

    public Game(/*int newWidth, int newHeight*/)
    {
        /*
        // set the size
        width = newWidth;
        height = newHeight;

        // set the window
        Console.WindowWidth = width + 1;
        Console.WindowHeight = height + 1;
        */
    }

    public int GetWidth()
    {
        return maze.Grid.GetLength(0);
    }

    public int GetHeight()
    {
        return maze.Grid.GetLength(1);
    }
    public void DrawMaze()
    {
        maze.Draw();
    }
    public void DrawPlayer()
    {
        player.Draw();
    }
    public void MovePlayer()
    {
        int direction;
        while (Console.KeyAvailable)
            {
            ConsoleKeyInfo key = Console.ReadKey(true);
            switch (key.Key)
            {
                case ConsoleKey.LeftArrow:
                direction=0;
                break;
                case ConsoleKey.RightArrow:
                direction=1;
                break;
                case ConsoleKey.DownArrow:
                direction=2;
                break;
                case ConsoleKey.UpArrow:
                direction=3;
                break;
                default:
                direction=4;
                break;
            }
            }
    }
    public void DrawEnemy()
    {
        enemy.Draw();
    }
    public void MoveEnemy()
    {
        enemy.Update();
    }
    /*
        public void Draw(float dt)
        {
            Console.SetCursorPosition(playerPosX, playerPosY);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("@");
        }
        */

}