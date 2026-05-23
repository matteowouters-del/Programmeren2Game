using System;
using System.Diagnostics;

namespace ProjectGame2526;

class Game
{
    protected Stopwatch stopwatch;
    protected Maze maze;
    protected Player player;
    protected Enemy enemy;
    protected UI uI;
    protected UIElement uITimer, uIScore, uIDifficulty;
    protected int uIOffsetX = 3;
    protected int uIOffsetY = 5;
    //protected int width, height;
    //protected int playerPosX = 5, playerPosY = 5;

    public Game(/*int newWidth, int newHeight*/)
    {
        maze = new Maze();
        maze.ChooseRandomMaze();
        maze.Draw(uIOffsetX,uIOffsetY);

        player = new Player(1,maze.PlayerSpawn[0],maze.PlayerSpawn[1],'@',ConsoleColor.Yellow, uIOffsetX, uIOffsetY);
        enemy = new Enemy(1,maze.EnemySpawn[0],maze.EnemySpawn[1],'E',ConsoleColor.Red, uIOffsetX, uIOffsetY);
        

        stopwatch = new Stopwatch();
        stopwatch.Start();

        uI = new UI();
        uITimer = new UIElement("Time",0,5,1);
        uIScore = new UIElement("Score",3000,15,1);
        uIDifficulty = new UIElement("Difficulty",1,30,1);
        uI.Add(uITimer);
        uI.Add(uIScore);
        uI.Add(uIDifficulty);

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
        maze.Draw(uIOffsetX,uIOffsetY);
    }
    public void Draw()
    {
        player.Draw();
        enemy.Draw();
        uI.Draw();
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
            return player.Move(direction, maze);
            }
            return false;
    }
    public void Update(double dt)
    {
        
    }
    public void MoveEnemy(double dt)
    {
        enemy.Update(dt, maze);
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