namespace ProjectGame2526;

public class UIElement
{
    protected int elementValue;
    protected string name;
    protected int posX;
    protected int posY;

    public int ElementValue
    {
        get { return elementValue; }
        set { elementValue = value; }
    }

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

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

    public UIElement(string newName, int newElementValue, int newPosX, int newPosY)
    {
        elementValue = newElementValue;
        name = newName;
        PosX = newPosX;
        PosY = newPosY;
    }

    public void Draw()
    {
        // draws one UI label and its current value
        Console.SetCursorPosition(PosX, PosY);
        Console.Write(name + ": " + elementValue);
    }
}