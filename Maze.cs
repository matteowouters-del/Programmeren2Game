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
    public void ChooseRandomMaze()
    {
        string path = "Mazes"; //Name of folder with text files containing mazes
        string[] fileNames = Directory.GetFiles(path); // create array of filenames
        
        Random rndGen = new Random();
        string chosenMaze = fileNames[rndGen.Next(0,fileNames.Length)]; //randomly select maze file

        LoadMazeFromFile(chosenMaze);
    }
    public void LoadMazeFromFile(string mazePath)
    {
        StreamReader reader = null;
        try
        {
            reader = new StreamReader(mazePath);

        for(int row = 0; row<mazeHeight; row++)
        {
            string line = reader.ReadLine(); // stores a line from the .txt file into a string
            
            string[] values = line.Split(','); //splits string containing one line from text file into separate strings for each character after a ','

            for(int col = 0; col<mazeWidth; col++)
            {
                int.TryParse(values[col], out Grid[row, col]); // converts each separate character (in a string) to int and puts it into the grid (=int[])
            }
        }
        }
        catch(Exception e)
        {
            Console.WriteLine(e);
        }
        finally
        {
            reader.Close();
        }
        
    }
    public void Draw()
    {
        for (int row = 0; row < Grid.GetLength(0); row++)
        {
            for (int col = 0; col < Grid.GetLength(1); col++)
            {
                switch ((TileType)Grid[row, col])
                {
                    case TileType.Empty:
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write(' ');
                        break;
                    case TileType.Wall:
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write('#');
                        break;
                    case TileType.Exit:
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write('X');
                        break;
                    case TileType.PlayerSpawn:
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write(' ');
                        PlayerSpawn[0] = col; //save the x location of the playerspawn
                        PlayerSpawn[1] = row; // ""      y ""    ""      ""      ""
                        break;
                    case TileType.EnemySpawn:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(' ');
                        EnemySpawn[0] = col;
                        EnemySpawn[1] = row;
                        break;
                    case TileType.Collectible:
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write('.');
                        break;
                }
            }
            Console.WriteLine();
        }
    }
    public bool CheckWall(int[,] map, int x, int y)
    {
        bool result;
        if ((TileType)map[y, x] == TileType.Wall || (TileType)map[y, x] == TileType.Exit)
        {
            result = true;
        }
        else
        {
            result = false;
        }
        return result;
    }
}
