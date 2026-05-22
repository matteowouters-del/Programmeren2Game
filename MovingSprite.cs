using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace ProjectGame2526;

public class MovingSprite : Sprite
{
    protected float speed;

    public float Speed
    {
        get{return speed;}
        set{speed = value;}
    }

    public MovingSprite(int speed, int x,int y,char symbol, ConsoleColor color, int offsetX, int offsetY):base(x,y,symbol,color,offsetX, offsetY)
    {
        Speed = speed;
    }
    public override void Update(double dt, int[,] maze)
    {
        
    }
}
