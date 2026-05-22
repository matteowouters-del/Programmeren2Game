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
        PosY = newPosY;
        Symbol = newSymbol;
        Color = newColor;
    }
    public void Draw()
        {
            Console.SetCursorPosition((int)posX, (int)posY);
            Console.ForegroundColor = Color;
            Console.Write(Symbol);
        }
    public bool Move(int direction, int [,] maze)
    {
        bool moved = false;
        switch (direction)
        {
            case 0:
                if ((TileType)maze[(int)PosY,(int)PosX-1]!=TileType.Wall && (TileType)maze[(int)PosY,(int)PosX-1]!=TileType.Exit)
                {
                    PosX--;
                    moved = true;
                }
            break;
            case 1:
            if((TileType)maze[(int)PosY,(int)PosX+1]!=TileType.Wall && (TileType)maze[(int)PosY, (int)PosX + 1] != TileType.Exit)
                {
                    PosX++; 
                    moved = true;
                }
            break;
            case 2:
            if((TileType)maze[(int)PosY+1,(int)PosX]!=TileType.Wall && (TileType)maze[(int)PosY + 1, (int)PosX] != TileType.Exit)
                {
                    PosY++; 
                    moved = true;
                }
            break;
            case 3:
            if((TileType)maze[(int)PosY-1,(int)PosX]!=TileType.Wall && (TileType)maze[(int)PosY - 1, (int)PosX] != TileType.Exit)
                {
                    PosY--;
                    moved = true;
                }
            break;
            default:
            break;
            }
            return moved;
    }
    public virtual void Update(double dt, int[,] maze)
    {
        
    }
}
