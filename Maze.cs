using System;

namespace ProjectGame2526;

public enum TyleType
{
    Empty,
    Wall,
    Exit,
    EnemySpawn,
    PlayerSpawn,
    Collectible
}
public class Maze
{
    protected int[,] grid;
    public int[,] Grid
    {
        get { return grid; }
        set { grid = value; }
    }
    public Maze()
    {
        Grid = new int[25, 25]{
        {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,2},
        {1,4,1,0,1,0,0,0,1,0,1,0,0,0,1,0,0,0,1,0,1,0,0,0,2},
        {1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,2},
        {1,0,0,0,1,5,1,0,0,5,1,0,1,0,1,5,1,0,0,0,5,0,1,0,2},
        {1,0,1,1,1,1,1,0,1,1,1,0,1,1,1,1,1,0,1,1,1,0,1,1,2},
        {1,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,1,0,0,0,0,0,2},
        {1,0,1,0,1,0,1,1,1,0,1,0,1,0,1,0,1,0,1,0,1,1,1,0,2},
        {1,0,1,0,0,0,0,0,0,5,1,0,0,0,1,0,5,0,1,0,0,0,1,0,2},
        {1,1,1,5,1,1,1,0,1,1,1,1,1,0,1,1,1,1,1,1,1,1,1,0,2},
        {1,0,1,0,1,0,0,0,1,0,1,0,0,0,0,0,0,0,1,0,0,0,0,0,2},
        {1,0,1,0,1,0,1,0,1,0,1,1,1,0,1,1,1,0,1,0,1,1,1,0,2},
        {1,0,0,0,1,0,1,5,0,0,1,0,0,5,1,0,0,0,1,0,0,5,1,0,2},
        {1,5,1,1,1,1,1,0,1,0,1,1,1,0,1,0,1,1,1,1,1,0,1,1,2},
        {1,0,0,0,0,0,0,0,1,0,1,0,0,0,1,0,0,0,1,5,0,0,0,0,2},
        {1,1,1,1,1,1,1,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,2},
        {1,5,0,0,1,3,0,0,0,0,1,0,0,0,1,0,0,0,1,0,5,0,0,0,2},
        {1,1,1,0,1,0,1,0,1,1,1,1,1,0,1,1,1,0,1,1,1,1,1,0,2},
        {1,0,0,0,0,0,1,0,0,0,1,0,0,0,0,5,0,0,1,0,0,0,0,0,2},
        {1,0,1,1,1,1,1,1,1,0,1,1,1,1,1,1,1,0,1,0,1,1,1,1,2},
        {1,0,0,0,0,5,1,0,1,0,1,5,0,0,1,0,0,0,1,0,0,0,1,0,2},
        {1,1,1,0,1,0,1,0,1,0,1,1,1,0,1,0,1,0,1,0,1,0,1,0,2},
        {1,0,0,0,1,0,1,5,0,0,1,0,0,0,1,0,1,0,0,0,1,5,0,0,2},
        {1,0,1,1,1,0,1,1,1,0,1,0,1,1,1,0,1,1,1,1,1,0,1,0,2},
        {1,0,0,0,1,0,1,0,0,5,0,0,0,0,0,0,0,0,1,0,0,0,1,0,2},
        {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2}};
    }
    public void Draw()
    {
        for (int col = 0; col < Grid.GetLength(0); col++)
        {
            for (int row = 0; row < Grid.GetLength(1); row++)
            {
                switch ((TyleType)Grid[col, row])
                {
                    case TyleType.Empty:
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write(' ');
                        break;
                    case TyleType.Wall:
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write('#');
                        break;
                    case TyleType.Exit:
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write('X');
                        break;
                    case TyleType.PlayerSpawn:
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write(' ');
                        break;
                    case TyleType.EnemySpawn:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(' ');
                        break;
                    case TyleType.Collectible:
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
        if ((TyleType)map[x, y] == TyleType.Wall)
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
