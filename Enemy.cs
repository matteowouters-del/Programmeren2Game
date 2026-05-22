using System;
using System.Security.Cryptography.X509Certificates;

namespace ProjectGame2526;

public class Enemy:MovingSprite
{
public Enemy(int xSpeed, int ySpeed, int x, int y, char symbol,ConsoleColor color) : base(xSpeed, ySpeed, x, y, symbol, color)
    {
        
    }
    public override void Update(float dt)
    {
        Random rndGen = new Random();
        int random = rndGen.Next(0,4);
        switch (random)
        {
            case 0:
            PosX+=XSpeed;
            break;
            case 1:
            PosX-=XSpeed;
            break;
            case 2:
            PosY+=YSpeed;
            break;
            case 3:
            PosY-=YSpeed;
            break;
            default:
            break;
        }
    }
}
