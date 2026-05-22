using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace ProjectGame2526;

public class MovingSprite : Sprite
{
    protected float xSpeed;
    protected float ySpeed;
    public float YSpeed
    {
        get{return ySpeed;}
        set{ySpeed = value;}
    }
    public float XSpeed
    {
        get{return xSpeed;}
        set{xSpeed = value;}
    }
    public MovingSprite(int newXSpeed, int newYSpeed, int x,int y,char symbol, ConsoleColor color):base(x,y,symbol,color)
    {
        XSpeed = newXSpeed;
        YSpeed = newYSpeed;
    }
    public override void Update(float dt)
    {
        PosX+=XSpeed;
        PosY+=YSpeed;
    }
}
