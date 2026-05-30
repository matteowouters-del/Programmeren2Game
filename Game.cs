using System.Diagnostics;

namespace ProjectGame2526;

public enum GameState
{
    StartingScreen,
    MainMenu,
    Playing,
    Won,
    Lost,
    Quit
}
public class Game
{
    protected Stopwatch stopwatch;
    protected Maze maze;
    protected Player player;
    protected Enemy enemy;
    protected UI uI;
    protected UIElement uITimer, uIScore, uIDifficulty;
    protected Screen startingScreen, gameOverScreen, mainMenuScreen, winScreen;
    protected int uIOffsetX = 3;
    protected int uIOffsetY = 5;
    protected GameState currentGameState;
    protected Menu mainMenu;
    //protected int width, height;
    //protected int playerPosX = 5, playerPosY = 5;

    public GameState CurrentGameState
    {
        get { return currentGameState; }
        set { currentGameState = value; }
    }

    public Game(/*int newWidth, int newHeight*/)
    {
        currentGameState = GameState.StartingScreen;
        stopwatch = new Stopwatch();
        /*maze = new Maze();
        maze.ChooseRandomMaze();
        //maze.Draw(uIOffsetX, uIOffsetY);

        player = new Player(1, maze.PlayerSpawn[0], maze.PlayerSpawn[1], '@', ConsoleColor.Yellow, uIOffsetX, uIOffsetY);
        enemy = new Enemy(1, maze.EnemySpawn[0], maze.EnemySpawn[1], 'E', ConsoleColor.Red, uIOffsetX, uIOffsetY);


        stopwatch = new Stopwatch();
        stopwatch.Start();

        uI = new UI();
        uITimer = new UIElement("Time", 0, 5, 1);
        uIScore = new UIElement("Score", 3000, 15, 1);
        uIDifficulty = new UIElement("Difficulty", 1, 30, 1);
        //uI.Add(uITimer);
        uI.Add(uIScore);
        uI.Add(uIDifficulty);
        */

        startingScreen = new Screen("StartingScreen.txt");
        gameOverScreen = new Screen("GameOverScreen.txt");
        mainMenuScreen = new Screen("MainMenuScreen.txt");
        winScreen = new Screen("WinScreen.txt");

        mainMenu = new Menu("MainMenuScreen.txt", ConsoleColor.DarkGreen, ConsoleColor.Black, ConsoleColor.White, ConsoleColor.DarkYellow);

        mainMenu.AddMenuItem(new MenuItem("Start game", GameState.Playing));
        mainMenu.AddMenuItem(new MenuItem("Exit", GameState.Quit));
        /*
        // set the size
        width = newWidth;
        height = newHeight;

        // set the window
        Console.WindowWidth = width + 1;
        Console.WindowHeight = height + 1;
        */
    }
    public void Draw()
    {
        switch (currentGameState)
        {
            case GameState.StartingScreen:
                startingScreen.Draw();
                break;
            case GameState.MainMenu:
                mainMenu.Draw();
                break;
            case GameState.Playing:
                maze.Draw(uIOffsetX, uIOffsetY);
                player.Draw();
                enemy.Draw();
                uI.Draw();
                break;
            case GameState.Won:
                winScreen.Draw();
                break;
            case GameState.Lost:
                gameOverScreen.Draw();
                break;
            default:
                break;
        }
    }
    public bool MovePlayer(ConsoleKeyInfo key)
    {
        int direction;

        switch (key.Key)
        {
            case ConsoleKey.LeftArrow:
                direction = 0;
                break;
            case ConsoleKey.RightArrow:
                direction = 1;
                break;
            case ConsoleKey.DownArrow:
                direction = 2;
                break;
            case ConsoleKey.UpArrow:
                direction = 3;
                break;
            default:
                return false;
        }

        return player.Move(direction, maze);
    }
    public void Input()
    {
        while (Console.KeyAvailable)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);

            switch (currentGameState)
            {
                case GameState.StartingScreen:
                    if (key.Key == ConsoleKey.Enter)
                    {
                        currentGameState = GameState.MainMenu;
                        ResetScreen();
                    }
                    break;

                case GameState.MainMenu:
                    if (key.Key == ConsoleKey.Enter)
                    {
                        ResetScreen();
                        mainMenu.ActivateMenuItem(this);
                    }
                    else if (key.Key == ConsoleKey.UpArrow)
                    {
                        mainMenu.SelectPreviousItem();
                    }
                    else if (key.Key == ConsoleKey.DownArrow)
                    {
                        mainMenu.SelectNextItem();
                    }
                    break;

                case GameState.Playing:
                    bool playerMoved = MovePlayer(key);
                    if (playerMoved)
                    {
                        if (player.HasWon)
                        {
                            currentGameState = GameState.Won;
                            ResetScreen();
                        }
                        MoveEnemy(0);
                    }
                    break;

                case GameState.Won:
                    if(key.Key == ConsoleKey.Enter)
                    {
                        currentGameState = GameState.MainMenu;
                        ResetScreen();
                    }
                break;
                case GameState.Lost:
                    if (key.Key == ConsoleKey.Enter)
                    {
                        currentGameState = GameState.MainMenu;
                        ResetScreen();
                    }
                    break;
            }
        }
    }
    public void Update(double dt)
    {
    }
    public void MoveEnemy(double dt)
    {
        enemy.Update(dt, maze);
    }
    public void StartNewGame()
    {
        maze = new Maze();
        maze.ChooseRandomMaze();

        player = new Player(1, maze.PlayerSpawn[0], maze.PlayerSpawn[1], '@', ConsoleColor.Yellow, uIOffsetX, uIOffsetY);
        enemy = new Enemy(1, maze.EnemySpawn[0], maze.EnemySpawn[1], 'E', ConsoleColor.Red, uIOffsetX, uIOffsetY);

        uI = new UI();
        uITimer = new UIElement("Time", 0, 5, 1);
        uIScore = new UIElement("Score", 3000, 15, 1);
        uIDifficulty = new UIElement("Difficulty", 1, 30, 1);
        uI.Add(uIScore);
        uI.Add(uIDifficulty);

        //stopwatch.Reset();
        //stopwatch.Start();
        ResetScreen();
        currentGameState = GameState.Playing;
    }
    public void ResetScreen()
    {
        Console.SetCursorPosition(0, 0);
        Console.ForegroundColor = ConsoleColor.Black;
        Console.BackgroundColor = ConsoleColor.Black;
        for (int i = 0; i < Console.WindowHeight-1; i++)
        {
            for (int j = 0; j < Console.WindowWidth-1; j++)
            {
                Console.Write(" ");
            }
            Console.WriteLine();
        }
        Console.SetCursorPosition(0, 0);
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