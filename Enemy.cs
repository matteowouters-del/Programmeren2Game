using System;
using System.Security.Cryptography.X509Certificates;

namespace ProjectGame2526;

public class Enemy:MovingSprite
{
    protected Random rndGen = new Random();
public Enemy(int speed, int x, int y, char symbol,ConsoleColor color, int offsetX, int offsetY) : base(speed, x, y, symbol, color, offsetX, offsetY)
    {
        
    }
    public override void Update(double dt, int[,] maze)
    {
        
        for(int i = 0; i<speed; i++)
        {
            bool moved = false;
            while (!moved)
            {
                int direction = rndGen.Next(0,4);
                moved = Move(direction, maze);
            }
        }
    }
}
