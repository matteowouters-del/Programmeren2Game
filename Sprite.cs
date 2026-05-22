using System;

namespace ProjectGame2526;

public class Sprite
{
    protected float posX;
    protected float posY;
    protected char symbol;
    protected ConsoleColor color;
    public float PosX
    {
        get { return posX; }
        set { posX = value; }
    }
    public float PosY
    {
        get { return posY; }
        set { posY = value; }
    }
    public char Symbol
    {
        get { return symbol; }
        set { symbol = value; }
    }
    public ConsoleColor Color
    {
        get { return color; }
        set { color = value; }
    }
    public Sprite(int newPosX, int newPosY, char newSymbol, ConsoleColor newColor)
    {
        PosX = newPosX;
        posY = newPosY;
        Symbol = newSymbol;
        Color = newColor;
    }
    public void Draw()
        {
            Console.SetCursorPosition((int)posX, (int)posY);
            Console.ForegroundColor = Color;
            Console.Write(Symbol);
        }
    public void Move(int move, int [,] maze)
    {
        switch (move)
        {
            case 0:
                if ((TyleType)maze[(int)PosY,(int)PosX-1]!=TyleType.Wall && (TyleType)maze[(int)PosY,(int)PosX-1]!=TyleType.Exit)
                {
                    PosX--;
                }
            break;
            case 1:
            if((TyleType)maze[(int)PosY,(int)PosX+1]!=TyleType.Wall && (TyleType)maze[(int)PosY, (int)PosX + 1] != TyleType.Exit)
                {
                    PosX++; 
                }
            break;
            case 2:
            if((TyleType)maze[(int)PosY+1,(int)PosX]!=TyleType.Wall && (TyleType)maze[(int)PosY + 1, (int)PosX] != TyleType.Exit)
                {
                    PosY++; 
                }
            break;
            case 3:
            if((TyleType)maze[(int)PosY-1,(int)PosX]!=TyleType.Wall && (TyleType)maze[(int)PosY - 1, (int)PosX] != TyleType.Exit)
                {
                    PosY--;
                }
            break;
            default:
            break;
            }
    }
    public virtual void Update(float dt)
    {
        
    }
}
