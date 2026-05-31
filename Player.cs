namespace ProjectGame2526;

public class Player : MovingSprite
{
    protected bool hasWon = false;
    protected int ghostCharges = 0;

    public int GhostCharges
    {
        get { return ghostCharges; }
        set { ghostCharges = value; }
    }

    public bool HasWon
    {
        get { return hasWon; }
    }

    public Player(int newspeed, int newPosX, int newPosY, char newSymbol, ConsoleColor newColor, int offsetX, int offsetY)
        : base(newspeed, newPosX, newPosY, newSymbol, newColor, offsetX, offsetY)
    {
    }

    public override bool Move(int direction, Maze maze)
    {
        int targetX = PosX;
        int targetY = PosY;

        switch (direction)
        {
            case 0:
                targetX--;
                break;
            case 1:
                targetX++;
                break;
            case 2:
                targetY++;
                break;
            case 3:
                targetY--;
                break;
            default:
                return false;
        }

        int mazeHeight = maze.Grid.GetLength(0);
        int mazeWidth = maze.Grid.GetLength(1);

        // stops the player from moving outside the maze
        if (targetX < 0 || targetX >= mazeWidth || targetY < 0 || targetY >= mazeHeight)
        {
            return false;
        }

        TileType tile = (TileType)maze.Grid[targetY, targetX];

        // lets the player walk through a wall if they have a ghost charge
        if (tile == TileType.Wall)
        {
            if (ghostCharges > 0)
            {
                ghostCharges--;
            }
            else
            {
                return false;
            }
        }

        // checks if the player touched an exit tile
        if (tile == TileType.Exit)
        {
            if (maze.CheckWin(targetX, targetY))
            {
                PosX = targetX;
                PosY = targetY;
                hasWon = true;
                return true;
            }
            else
            {
                maze.RemoveWrongExit(targetX, targetY);
                return false;
            }
        }

        PosX = targetX;
        PosY = targetY;
        return true;
    }
}