
namespace ProjectGame2526;

public class MovingSprite : Sprite
{
    protected int speed;

    public int Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    public MovingSprite(int speed, int x, int y, char symbol, ConsoleColor color, int offsetX, int offsetY) : base(x, y, symbol, color, offsetX, offsetY)
    {
        Speed = speed;
    }
    public override void Update(double dt, Maze maze)
    {

    }
    public virtual bool Move(int direction, Maze maze)
    {
        int targetX = PosX; //put movement into extra variable for extra checks before commiting
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

        TileType tile = (TileType)maze.Grid[targetY, targetX];

        if (tile == TileType.Wall || tile == TileType.Exit)
        {
            return false;
        }

        PosX = targetX;
        PosY = targetY;
        return true;
    }
}
