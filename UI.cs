namespace ProjectGame2526;

public class UI
{
    protected List<UIElement> uIElements;

    public UI()
    {
       uIElements = new List<UIElement>();
    }

    public void Add(UIElement element)
    {
        uIElements.Add(element);
    }

    public void Draw()
    {
        foreach (UIElement element in uIElements)
        {
            element.Draw();
        }
    }

    public void UpdateUIElementValue(string elementName, int newValue)
    {
        bool updated = false;
        for(int i = 0; i < uIElements.Count && !updated; i++)
        {
            if(uIElements[i].Name == elementName) 
            {
                uIElements[i].ElementValue = newValue;
                updated = true;
            }
        }
    }
}