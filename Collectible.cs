namespace ProjectGame2526;

public enum CollectibleType
{
    Reveal,
    Score,
    Ghost
}

public class Collectible
{
    protected int posX;
    protected int posY;
    protected char symbol;
    protected ConsoleColor color;
    protected int offsetX;
    protected int offsetY;
    protected bool isCollected;

    public int PosX
    {
        get { return posX; }
    }

    public int PosY
    {
        get { return posY; }
    }

    public bool IsCollected
    {
        get { return isCollected; }
    }

    public Collectible(int newPosX, int newPosY, char newSymbol, ConsoleColor newColor, int newOffsetX, int newOffsetY)
    {
        posX = newPosX;
        posY = newPosY;
        symbol = newSymbol;
        color = newColor;
        offsetX = newOffsetX;
        offsetY = newOffsetY;
        isCollected = false;
    }

    public void Draw()
    {
        if (!isCollected)
        {
            Console.SetCursorPosition(posX + offsetX, posY + offsetY);
            Console.ForegroundColor = color;
            Console.Write(symbol);
        }
    }

    public bool CheckPosition(int x, int y)
    {
        return !isCollected && posX == x && posY == y;
    }

    public void Collect(Game game, Maze maze, Player player)
    {
        if (!isCollected)
        {
            // marks collectible as taken before applying its effect
            isCollected = true;
            ApplyEffect(game, maze, player);
        }
    }

    public virtual void ApplyEffect(Game game, Maze maze, Player player)
    {
    }
}