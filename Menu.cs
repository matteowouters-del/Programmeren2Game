namespace ProjectGame2526;

public class Menu : Screen
{
    protected List<MenuItem> menuItems;
    protected int activeMenuItemID;
    protected ConsoleColor activeItemForegroundColor;
    protected ConsoleColor activeItemBackgroundColor;

    public Menu(string filepath)
    : base(filepath)
    {
        menuItems = new List<MenuItem>();
        activeMenuItemID = 0;
        activeItemForegroundColor = ConsoleColor.Black;
        activeItemBackgroundColor = ConsoleColor.White;
    }

    public Menu(string filepath, ConsoleColor newActiveItemForegroundColor, ConsoleColor newActiveItemBackgroundColor) : base(filepath)
    {
        menuItems = new List<MenuItem>();
        activeMenuItemID = 0;
        activeItemForegroundColor = newActiveItemForegroundColor;
        activeItemBackgroundColor = newActiveItemBackgroundColor;
    }

    public void AddMenuItem(MenuItem newMenuItem)
    {
        menuItems.Add(newMenuItem);
    }

    public void SelectNextItem()
    {
        // wraps to the top when the last item is selected
        if (activeMenuItemID == menuItems.Count - 1)
        {
            activeMenuItemID = 0;
        }
        else
        {
            activeMenuItemID++;
        }
    }

    public void SelectPreviousItem()
    {
        // wraps to the bottom when moving up from the first item
        if (activeMenuItemID == 0)
        {
            activeMenuItemID = menuItems.Count - 1;
        }
        else
        {
            activeMenuItemID--;
        }
    }

    public override void Draw()
    {
        base.Draw();

        for (int i = 0; i < menuItems.Count; i++)
        {
            // highlights the currently selected menu item
            if (i == activeMenuItemID)
            {
                Console.ForegroundColor = activeItemForegroundColor;
                Console.BackgroundColor = activeItemBackgroundColor;
            }
            else
            {
                Console.ForegroundColor = foregroundColor;
                Console.BackgroundColor = backgroundColor;
            }

            Console.WriteLine("\n\t{0}\n", menuItems[i].Name);
        }
    }

    public void ActivateMenuItem(Game game)
    {
        menuItems[activeMenuItemID].Activate(game);
    }
}