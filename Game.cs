using System.Diagnostics;
using System.Text.Json;

namespace ProjectGame2526;

public enum GameState
{
    StartingScreen,
    MainMenu,
    Playing,
    Won,
    Lost,
    HighScores,
    Rules,
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
    protected Screen startingScreen, gameOverScreen, winScreen, rulesScreen;
    protected HighscoresScreen highscoresScreen;
    protected int uIOffsetX = 3;
    protected int uIOffsetY = 5;
    protected GameState currentGameState;
    protected Menu mainMenu;
    protected int currentDifficulty = 1;
    protected List<Collectible> collectibles;
    protected int score;
    protected int lastUiSecond = -1;
    protected string currentPlayerName;
    protected bool scoreSaved;

    public GameState CurrentGameState
    {
        get { return currentGameState; }
        set { currentGameState = value; }
    }

    public Game()
    {
        currentGameState = GameState.StartingScreen;
        stopwatch = new Stopwatch();

        // load all static text screens from file
        startingScreen = new Screen("StartingScreen.txt");
        gameOverScreen = new Screen("GameOverScreen.txt");
        winScreen = new Screen("WinScreen.txt");
        rulesScreen = new Screen("RulesScreen.txt");
        highscoresScreen = new HighscoresScreen();

        currentPlayerName = "";
        scoreSaved = false;

        // creates the main menu with all options
        mainMenu = new Menu("MainMenuScreen.txt", ConsoleColor.DarkGreen, ConsoleColor.Black, ConsoleColor.White, ConsoleColor.DarkYellow);
        mainMenu.AddMenuItem(new MenuItem("Start game", GameState.Playing));
        mainMenu.AddMenuItem(new MenuItem("HighScores", GameState.HighScores));
        mainMenu.AddMenuItem(new MenuItem("Rules", GameState.Rules));
        mainMenu.AddMenuItem(new MenuItem("Exit", GameState.Quit));
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
                // draws the maze and all gameplay objects
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

                // places dynamic win text under the txt screen
                int winTextY = winScreen.LineCount + 2;

                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(10, winTextY);
                Console.WriteLine("Score: " + GetFinalScore() + "          ");

                if (!scoreSaved)
                {
                    // shows name input before the score is saved
                    Console.SetCursorPosition(10, winTextY + 2);
                    Console.WriteLine("Enter your name: " + currentPlayerName + "_          ");
                    Console.SetCursorPosition(10, winTextY + 4);
                    Console.WriteLine("Press Enter to save score     ");
                }
                else
                {
                    // shows confirmation after saving
                    Console.SetCursorPosition(10, winTextY + 2);
                    Console.WriteLine("Highscore saved!              ");
                    Console.SetCursorPosition(10, winTextY + 4);
                    Console.WriteLine("Press Enter to return         ");
                }
                break;

            case GameState.Lost:
                gameOverScreen.Draw();
                break;

            case GameState.HighScores:
                highscoresScreen.Draw();
                break;

            case GameState.Rules:
                rulesScreen.Draw();
                break;
        }
    }

    public bool MovePlayer(ConsoleKeyInfo key)
    {
        int direction;

        // converts arrow keys to movement directions
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
                    // stores the old player position so the old tile can be redrawn
                    int oldPlayerX = player.PosX;
                    int oldPlayerY = player.PosY;

                    bool playerMoved = MovePlayer(key);

                    // redraws any maze tiles changed by collectibles or exits
                    RedrawChangedMazeTiles();

                    if (playerMoved)
                    {
                        RedrawMazeAt(oldPlayerX, oldPlayerY);
                        player.Draw();

                        // checks if the enemy is now on the player
                        CheckEnemyHit();
                        if (currentGameState == GameState.Lost)
                        {
                            break;
                        }

                        // checks if the player picked up a collectible
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
                            MoveEnemy();
                        }
                    }

                    // debug shortcut to force a win
                    if (key.Key == ConsoleKey.W)
                    {
                        stopwatch.Stop();
                        currentGameState = GameState.Won;
                        ResetScreen();
                    }
                    if (key.Key == ConsoleKey.L)
                    {
                        stopwatch.Stop();
                        currentGameState = GameState.Lost;
                        ResetScreen();
                    }
                    break;

                case GameState.Won:
                    if (!scoreSaved)
                    {
                        if (key.Key == ConsoleKey.Backspace)
                        {
                            // removes the last typed character
                            if (currentPlayerName.Length > 0)
                            {
                                currentPlayerName = currentPlayerName.Substring(0, currentPlayerName.Length - 1);
                            }
                        }
                        else if (key.Key == ConsoleKey.Enter)
                        {
                            // saves the score only when a name was entered
                            if (currentPlayerName.Length > 0)
                            {
                                SaveHighscore();
                                scoreSaved = true;
                                ResetScreen();
                            }
                        }
                        else
                        {
                            char c = key.KeyChar;

                            // accepts only letters and digits for the name
                            if (char.IsLetterOrDigit(c) && currentPlayerName.Length < 12)
                            {
                                currentPlayerName += c;
                            }
                        }
                    }
                    else
                    {
                        if (key.Key == ConsoleKey.Enter)
                        {
                            currentGameState = GameState.MainMenu;
                            ResetScreen();
                        }
                    }
                    break;

                case GameState.Lost:
                    if (key.Key == ConsoleKey.Enter)
                    {
                        currentGameState = GameState.MainMenu;
                        ResetScreen();
                    }
                    break;

                case GameState.HighScores:
                    if (key.Key == ConsoleKey.Enter)
                    {
                        currentGameState = GameState.MainMenu;
                        ResetScreen();
                    }
                    break;

                case GameState.Rules:
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

        // updates the timer in the UI
        uI.UpdateUIElementValue("Time", elapsedSeconds);

        // lowers the score over time but never below 0
        int currentScore = score - elapsedSeconds * 10;
        if (currentScore < 0)
        {
            currentScore = 0;
        }
        uI.UpdateUIElementValue("Score", currentScore);

        int difficulty = 1;

        // increases difficulty based on time survived
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

        // redraws the UI only once every second
        if (elapsedSeconds != lastUiSecond)
        {
            uI.Draw();
            lastUiSecond = elapsedSeconds;
        }
    }

    public void MoveEnemy()
    {
        for (int i = 0; i < enemy.Speed; i++)
        {
            // stores the old enemy position so the old tile can be redrawn
            int oldEnemyX = enemy.PosX;
            int oldEnemyY = enemy.PosY;

            enemy.MoveRandomStep(maze);

            RedrawTileContents(oldEnemyX, oldEnemyY);
            enemy.Draw();

            CheckEnemyHit();
            if (currentGameState == GameState.Lost)
            {
                return;
            }

            // faster enemies wait less between steps
            Thread.Sleep(150 / enemy.Speed);
        }
    }

    public void StartNewGame()
    {
        // creates a new maze and loads a random file
        maze = new Maze();
        maze.ChooseRandomMaze();

        // creates a new list of collectibles for this run
        collectibles = new List<Collectible>();
        SpawnCollectibles(CollectibleType.Reveal, 15);
        SpawnCollectibles(CollectibleType.Ghost, 2);
        SpawnCollectibles(CollectibleType.Score, 8);

        // resets player and enemy positions
        player = new Player(1, maze.PlayerSpawn[0], maze.PlayerSpawn[1], '@', ConsoleColor.Yellow, uIOffsetX, uIOffsetY);
        enemy = new Enemy(1, maze.EnemySpawn[0], maze.EnemySpawn[1], 'E', ConsoleColor.Red, uIOffsetX, uIOffsetY);

        score = 3000;

        // rebuilds the UI for a fresh game
        uI = new UI();
        uITimer = new UIElement("Time", 0, 5, 1);
        uIScore = new UIElement("Score", score, 15, 1);
        uIDifficulty = new UIElement("Difficulty", 1, 30, 1);

        uI.Add(uITimer);
        uI.Add(uIScore);
        uI.Add(uIDifficulty);

        currentDifficulty = 1;
        currentPlayerName = "";
        scoreSaved = false;
        lastUiSecond = -1;

        // restarts the timer for the new game
        stopwatch.Reset();
        stopwatch.Start();

        ResetScreen();
        currentGameState = GameState.Playing;
        DrawStaticPlayingScreen();
    }

    public void ResetScreen()
    {
        // fills the whole console with black spaces
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
        // adds score immediately when a score collectible is picked up
        score += amount;
        uI.UpdateUIElementValue("Score", score);
    }

    public int GetFinalScore()
    {
        // calculates the final score shown on the win screen
        int finalScore = score - (int)stopwatch.Elapsed.TotalSeconds * 10;

        if (finalScore < 0)
        {
            finalScore = 0;
        }

        return finalScore;
    }

    public void SaveHighscore()
    {
        List<HighscoreEntry> highscores = new List<HighscoreEntry>();
        StreamReader streamReader = null;
        StreamWriter streamWriter = null;

        try
        {
            // loads existing highscores if the file already exists
            if (File.Exists("highscores.json"))
            {
                streamReader = new StreamReader("highscores.json");
                string jsonText = streamReader.ReadToEnd();
                streamReader.Close();
                streamReader = null;

                if (jsonText != "")
                {
                    highscores = JsonSerializer.Deserialize<List<HighscoreEntry>>(jsonText);
                }

                if (highscores == null)
                {
                    highscores = new List<HighscoreEntry>();
                }
            }

            // adds the new score to the list
            highscores.Add(new HighscoreEntry(currentPlayerName, GetFinalScore()));

            // sorts the list from highest to lowest score
            highscores.Sort(delegate (HighscoreEntry a, HighscoreEntry b)
            {
                return a.Score.CompareTo(b.Score);
            });

            highscores.Reverse();

            // keeps only the best 5 scores
            if (highscores.Count > 5)
            {
                highscores.RemoveRange(5, highscores.Count - 5);
            }

            string jsonTextToSave = JsonSerializer.Serialize(highscores);

            streamWriter = new StreamWriter("highscores.json");
            streamWriter.Write(jsonTextToSave);
        }
        catch (Exception e)
        {
            Console.SetCursorPosition(10, 20);
            Console.WriteLine("Save error: " + e.Message + "      ");
        }
        finally
        {
            if (streamReader != null)
            {
                streamReader.Close();
            }

            if (streamWriter != null)
            {
                streamWriter.Close();
            }
        }
    }

    public void SpawnCollectibles(CollectibleType collectibleType, int amount)
    {
        List<int[]> freeTiles = maze.GetFreeTiles();

        // removes tiles that already contain another collectible
        foreach (Collectible collectible in collectibles)
        {
            freeTiles.RemoveAll(tile => tile[0] == collectible.PosX && tile[1] == collectible.PosY);
        }

        // randomly places the requested amount of collectibles
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
        // loops backwards so collected items can be removed safely
        for (int i = collectibles.Count - 1; i >= 0; i--)
        {
            if (collectibles[i].CheckPosition(player.PosX, player.PosY))
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
        // draws the full gameplay screen at the start of a run
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
        // redraws one tile of the maze
        maze.DrawTile(x, y, uIOffsetX, uIOffsetY);
    }

    public void CheckEnemyHit()
    {
        // loses the game if player and enemy are on the same tile
        if (player.PosX == enemy.PosX && player.PosY == enemy.PosY)
        {
            stopwatch.Stop();
            currentGameState = GameState.Lost;
            ResetScreen();
        }
    }

    public void RedrawChangedMazeTiles()
    {
        // redraws all exit tiles changed by gameplay
        for (int i = 0; i < maze.ChangedTiles.Count; i++)
        {
            int x = maze.ChangedTiles[i][0];
            int y = maze.ChangedTiles[i][1];
            RedrawMazeAt(x, y);
        }

        maze.ChangedTiles.Clear();
    }
    public void RedrawTileContents(int x, int y)
    {
        maze.DrawTile(x, y, uIOffsetX, uIOffsetY);

        foreach (Collectible collectible in collectibles)
        {
            if (collectible.PosX == x && collectible.PosY == y && !collectible.IsCollected)
            {
                collectible.Draw();
            }
        }

        if (player.PosX == x && player.PosY == y)
        {
            player.Draw();
        }

        if (enemy.PosX == x && enemy.PosY == y)
        {
            enemy.Draw();
        }
    }
}