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
    protected int currentDifficulty = 1;
    protected List<Collectible> collectibles;
    protected int score;
    protected int lastUiSecond = -1;
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
                foreach (Collectible collectible in collectibles)
                {
                    collectible.Draw();
                }
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
            ConsoleKeyInfo key = Console.ReadKey();

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
                    int oldPlayerX = player.PosX;
                    int oldPlayerY = player.PosY;

                    bool playerMoved = MovePlayer(key);

                    RedrawChangedMazeTiles();

                    if (playerMoved)
                    {
                        RedrawMazeAt(oldPlayerX, oldPlayerY);
                        player.Draw();

                        CheckEnemyHit();
                        if (currentGameState == GameState.Lost)
                        {
                            break;
                        }

                        CheckCollectibles();
                        RedrawChangedMazeTiles();

                        if (player.HasWon)
                        {
                            stopwatch.Stop();
                            currentGameState = GameState.Won;
                            ResetScreen();
                        }
                        else
                        {
                            MoveEnemy(0);
                        }
                    }

                    if (key.Key == ConsoleKey.W)
                    {
                        stopwatch.Stop();
                        currentGameState = GameState.Won;
                        ResetScreen();
                    }
                    break;

                case GameState.Won:
                    if (key.Key == ConsoleKey.Enter)
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
    public void Update()
    {
        if (currentGameState != GameState.Playing)
        {
            return;
        }

        int elapsedSeconds = (int)stopwatch.Elapsed.TotalSeconds;

        if (elapsedSeconds != lastUiSecond)
        {
            uI.Draw();
            lastUiSecond = elapsedSeconds;
        }
        uI.UpdateUIElementValue("Time", elapsedSeconds);

        int currentScore = score - elapsedSeconds * 10;
        if (currentScore < 0)
        {
            currentScore = 0;
        }
        uI.UpdateUIElementValue("Score", currentScore);

        int difficulty = 1;

        if (elapsedSeconds >= 90)
        {
            difficulty = 4;
        }
        else if (elapsedSeconds >= 60)
        {
            difficulty = 3;
        }
        else if (elapsedSeconds >= 30)
        {
            difficulty = 2;
        }

        currentDifficulty = difficulty;
        uI.UpdateUIElementValue("Difficulty", currentDifficulty);

        enemy.Speed = currentDifficulty;
    }
    public void MoveEnemy(double dt)
    {
        for (int i = 0; i < enemy.Speed; i++)
        {
            int oldEnemyX = enemy.PosX;
            int oldEnemyY = enemy.PosY;

            enemy.MoveRandomStep(maze);

            RedrawMazeAt(oldEnemyX, oldEnemyY);
            enemy.Draw();

            CheckEnemyHit();
            if (currentGameState == GameState.Lost)
            {
                return;
            }

            Thread.Sleep(150 / enemy.Speed);
        }
    }
    public void StartNewGame()
    {
        maze = new Maze();
        maze.ChooseRandomMaze();

        collectibles = new List<Collectible>();
        SpawnCollectibles(CollectibleType.Reveal, 15);
        SpawnCollectibles(CollectibleType.Ghost, 2);
        SpawnCollectibles(CollectibleType.Score, 8);

        player = new Player(1, maze.PlayerSpawn[0], maze.PlayerSpawn[1], '@', ConsoleColor.Yellow, uIOffsetX, uIOffsetY);
        enemy = new Enemy(1, maze.EnemySpawn[0], maze.EnemySpawn[1], 'E', ConsoleColor.Red, uIOffsetX, uIOffsetY);

        score = 3000;

        uI = new UI();
        uITimer = new UIElement("Time", 0, 5, 1);
        uIScore = new UIElement("Score", score, 15, 1);
        uIDifficulty = new UIElement("Difficulty", 1, 30, 1);

        uI.Add(uITimer);
        uI.Add(uIScore);
        uI.Add(uIDifficulty);

        currentDifficulty = 1;

        stopwatch.Reset();
        stopwatch.Start();

        ResetScreen();
        currentGameState = GameState.Playing;
        DrawStaticPlayingScreen();
    }
    public void ResetScreen()
    {
        Console.SetCursorPosition(0, 0);
        Console.ForegroundColor = ConsoleColor.Black;
        Console.BackgroundColor = ConsoleColor.Black;
        for (int i = 0; i < Console.WindowHeight - 1; i++)
        {
            for (int j = 0; j < Console.WindowWidth - 1; j++)
            {
                Console.Write(" ");
            }
            Console.WriteLine();
        }
        Console.SetCursorPosition(0, 0);
    }
    public void AddScore(int amount)
    {
        score += amount;
        uI.UpdateUIElementValue("Score", score);
    }
    public void SpawnCollectibles(CollectibleType collectibleType, int amount)
    {
        List<int[]> freeTiles = maze.GetFreeTiles();

        foreach (Collectible collectible in collectibles)
        {
            freeTiles.RemoveAll(tile => tile[0] == collectible.PosX && tile[1] == collectible.PosY);
        }

        for (int i = 0; i < amount && freeTiles.Count > 0; i++)
        {
            int randomIndex = maze.RndGen.Next(0, freeTiles.Count);
            int[] tile = freeTiles[randomIndex];

            collectibles.Add(CreateCollectible(collectibleType, tile[0], tile[1]));
            freeTiles.RemoveAt(randomIndex);
        }
    }
    public Collectible CreateCollectible(CollectibleType collectibleType, int x, int y)
    {
        switch (collectibleType)
        {
            case CollectibleType.Reveal:
                return new RevealCollectible(x, y, uIOffsetX, uIOffsetY);
            case CollectibleType.Score:
                return new ScoreCollectible(x, y, uIOffsetX, uIOffsetY, 100);
            case CollectibleType.Ghost:
                return new GhostCollectible(x, y, uIOffsetX, uIOffsetY, 1);
            default:
                return null;
        }
    }
    public void CheckCollectibles()
    {
        for (int i = collectibles.Count - 1; i >= 0; i--)
        {
            if (collectibles[i].IsOnPosition(player.PosX, player.PosY))
            {
                collectibles[i].Collect(this, maze, player, enemy);

                if (collectibles[i].IsCollected)
                {
                    collectibles.RemoveAt(i);
                }
            }
        }
    }
    public void DrawStaticPlayingScreen()
    {
        maze.Draw(uIOffsetX, uIOffsetY);

        foreach (Collectible collectible in collectibles)
        {
            collectible.Draw();
        }

        player.Draw();
        enemy.Draw();
        uI.Draw();
    }
    public void RedrawMazeAt(int x, int y)
    {
        maze.DrawTile(x, y, uIOffsetX, uIOffsetY);
    }
    public void CheckEnemyHit()
    {
        if (player.PosX == enemy.PosX && player.PosY == enemy.PosY)
        {
            stopwatch.Stop();
            currentGameState = GameState.Lost;
            ResetScreen();
        }
    }
    public void RedrawChangedMazeTiles()
    {
        for (int i = 0; i < maze.ChangedTiles.Count; i++)
        {
            int x = maze.ChangedTiles[i][0];
            int y = maze.ChangedTiles[i][1];
            RedrawMazeAt(x, y);
        }

        maze.ChangedTiles.Clear();
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