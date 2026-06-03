namespace ProjectGame2526;

public class Sprite
{
    protected int posX;
    protected int posY;
    protected char symbol;
    protected ConsoleColor color;
    protected int offsetX;
    protected int offsetY;

    public int PosX
    {
        get { return posX; }
        set { posX = value; }
    }

    public int PosY
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

    public int OffsetX
    {
        get { return offsetX; }
        set { offsetX = value; }
    }

    public int OffsetY
    {
        get { return offsetY; }
        set { offsetY = value; }
    }

    public Sprite(int newPosX, int newPosY, char newSymbol, ConsoleColor newColor, int newOffsetX, int newOffsetY)
    {
        PosX = newPosX;
        PosY = newPosY;
        Symbol = newSymbol;
        Color = newColor;
        OffsetX = newOffsetX;
        OffsetY = newOffsetY;
    }

    public virtual void Draw()
    {
        // draws the sprite at its maze position plus the screen offset
        Console.SetCursorPosition(posX + OffsetX, posY + OffsetY);
        Console.ForegroundColor = Color;
        Console.Write(Symbol);
    }
}