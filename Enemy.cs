namespace ProjectGame2526;

public class Enemy : MovingSprite
{
    protected Random rndGen = new Random();

    public Enemy(int speed, int x, int y, char symbol, ConsoleColor color, int offsetX, int offsetY)
        : base(speed, x, y, symbol, color, offsetX, offsetY)
    {
    }

    public void MoveRandomStep(Maze maze)
    {
        bool moved = false;

        // keeps trying random directions until one works
        while (!moved)
        {
            int direction = rndGen.Next(0, 4);
            moved = Move(direction, maze);
        }
    }
}