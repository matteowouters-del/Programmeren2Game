using System;
using System.Diagnostics;

namespace ProjectGame2526;

class Game
{
    protected Stopwatch stopwatch;
    Maze maze = new Maze();
    protected Player player;
    protected Enemy enemy;
    //protected int width, height;
    //protected int playerPosX = 5, playerPosY = 5;

    public Game(/*int newWidth, int newHeight*/)
    {
        maze.ChooseRandomMaze();
        maze.Draw();

        player = new Player(maze.PlayerSpawn[0],maze.PlayerSpawn[1],'@',ConsoleColor.Yellow);
        enemy = new Enemy(1,maze.EnemySpawn[0],maze.EnemySpawn[1],'E',ConsoleColor.Red);

        stopwatch = new Stopwatch();
        stopwatch.Start();

        /*
        // set the size
        width = newWidth;
        height = newHeight;

        // set the window
        Console.WindowWidth = width + 1;
        Console.WindowHeight = height + 1;
        */
    }
    public void DrawMaze()
    {
        maze.Draw();
    }
    public void DrawPlayer()
    {
        player.Draw();
    }
    public bool MovePlayer()
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
            return player.Move(direction, maze.Grid);
            }
            return false;
    }
    public void Update(double dt)
    {
        
    }
    public void DrawEnemy()
    {
        enemy.Draw();
    }
    public void MoveEnemy(double dt)
    {
        enemy.Update(dt, maze.Grid);
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