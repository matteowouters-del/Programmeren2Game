using System;

namespace ProjectGame2526;

public enum TileType
{
    Empty = 0,
    Wall = 1,
    Exit = 2,
    EnemySpawn = 3,
    PlayerSpawn = 4,
    Collectible = 5
}
public class Maze
{
    protected int[,] grid;
    protected int[] playerSpawn = new int[2];
    protected int[] enemySpawn = new int[2];
    protected const int mazeWidth = 25;
    protected const int mazeHeight = 25;
    protected int[] correctExit = new int[2];
    protected Random rndGen = new Random();
    protected List<int[]> possibleExits = new List<int[]>();

    public int[,] Grid
    {
        get { return grid; }
        set { grid = value; }
    }
    public int[] PlayerSpawn
    {
        get { return playerSpawn; }
        set { playerSpawn = value; }
    }
    public int[] EnemySpawn
    {
        get { return enemySpawn; }
        set { enemySpawn = value; }
    }
    public Maze()
    {
        Grid = new int[mazeWidth, mazeHeight];
    }
    public int[] CorrectExit
    {
        get { return correctExit; }
        set { correctExit = value; }
    }
    public List<int[]> PossibleExits
    {
        get { return possibleExits; }
        set { possibleExits = value; }
    }


    public void ChooseRandomMaze()
    {
        string path = "Mazes"; //Name of folder with text files containing mazes
        string[] fileNames = Directory.GetFiles(path); // create array of filenames

        string chosenMaze = fileNames[rndGen.Next(0, fileNames.Length)]; //randomly select maze file

        LoadMazeFromFile(chosenMaze); //reads chosen .txt file and saves inside Grid[]
        FindSpecialTiles(); //saves playerspawn, enemyspawn and exit tile locations
        ChooseCorrectExit(); //randomly selects and saves random exit tile
    }
    public void LoadMazeFromFile(string mazePath)
    {
        StreamReader reader = new StreamReader(mazePath); //create streamreader inside folder with .txt files containing mazes
        try
        {
            for (int row = 0; row < mazeHeight; row++)
            {
                string line = reader.ReadLine(); // stores a line from the .txt file into a string

                string[] values = line.Split(','); //splits string containing one line from text file into separate strings for each character after a ','

                for (int col = 0; col < mazeWidth; col++)
                {
                    int.TryParse(values[col], out Grid[row, col]); // converts each separate character (in a string) to int and puts it into the grid (=int[])
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        finally
        {
            reader.Close();
        }

    }
    public void FindSpecialTiles()
    {
        PossibleExits.Clear();

        for (int row = 0; row < mazeHeight; row++)
        {
            for (int col = 0; col < mazeWidth; col++)
            {
                switch ((TileType)Grid[row, col])
                {
                    case TileType.PlayerSpawn:
                        PlayerSpawn[0] = col; //saves playerspawn in 2D array
                        PlayerSpawn[1] = row;
                        break;

                    case TileType.EnemySpawn:
                        EnemySpawn[0] = col; //saves enemyspawn in 2D array
                        EnemySpawn[1] = row;
                        break;
                    case TileType.Exit:
                        PossibleExits.Add(new int[] { col, row }); //saves all exittiles in list of 2D arrays
                        break;
                }
            }
        }
    }
    public void ChooseCorrectExit() //randomly selects an exit tile from the list
    {
        int random = rndGen.Next(0, PossibleExits.Count);
        CorrectExit = PossibleExits[random];
    }
    public void Draw(int offsetX, int offsetY)
    {
        for (int row = 0; row < Grid.GetLength(0); row++)
        {
            for (int col = 0; col < Grid.GetLength(1); col++)
            {
                switch ((TileType)Grid[row, col])
                {
                    case TileType.Empty:
                        Console.SetCursorPosition(col + offsetX, row + offsetY);
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write(' ');
                        break;
                    case TileType.Wall:
                        Console.SetCursorPosition(col + offsetX, row + offsetY);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write('#');
                        break;
                    case TileType.Exit:
                        Console.SetCursorPosition(col + offsetX, row + offsetY);
                        bool isStillPossibleExit = PossibleExits.Any(exit => exit[0] == col && exit[1] == row);
                        //Checks if the current exit tile is still in the list

                        if (isStillPossibleExit)
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write('X');
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write('#');
                        }

                        break;
                    case TileType.PlayerSpawn:
                        Console.SetCursorPosition(col + offsetX, row + offsetY);
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write(' ');
                        break;
                    case TileType.EnemySpawn:
                        Console.SetCursorPosition(col + offsetX, row + offsetY);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(' ');
                        break;
                    case TileType.Collectible:
                        Console.SetCursorPosition(col + offsetX, row + offsetY);
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write('.');
                        break;
                }
            }
            Console.WriteLine();
        }
    }
    public bool CheckWall(int x, int y)
    {
        bool result = false;
        if ((TileType)Grid[y, x] == TileType.Wall || (TileType)Grid[y, x] == TileType.Exit)
        {
            result = true;
        }
        return result;
    }
    public bool CheckWin(int x, int y)
    {
        bool result = false;
        if (CorrectExit[0] == x && CorrectExit[1] == y) //checks whether or not the current playerposition is the correct exit
        {
            result = true;
        }
        return result;
    }
    public void RemoveWrongExit(int x, int y)
    {
        PossibleExits.RemoveAll(exit => exit[0] == x && exit[1] == y);
    }
    public void RemoveRandomExit(int amount) //removes certain amount of random items from list
    {
        for (int i = 0; i < amount && PossibleExits.Count > 1; i++)
        {
            int random = rndGen.Next(0, PossibleExits.Count);
            if (PossibleExits[random][0] == CorrectExit[0] && PossibleExits[random][1] == CorrectExit[1]) //checks if randomly selected exit isn't the correct one
            {
                i--;
            }
            else
            {
                PossibleExits.RemoveAt(random); //removes random item from list
            }
        }
    }
}
