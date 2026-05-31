using System.IO;

namespace ProjectGame2526;

public class Screen
{
    protected string text;
    protected ConsoleColor foregroundColor;
    protected ConsoleColor backgroundColor;

    public string Text
    {
        get { return text; }
        set { text = value; }
    }

    public int LineCount
    {
        get
        {
            if (text == null)
            {
                return 0;
            }

            // counts how many lines the screen text uses
            return text.Split('\n').Length;
        }
    }

    public Screen(string filepath)
    {
        foregroundColor = ConsoleColor.DarkGreen;
        backgroundColor = ConsoleColor.Black;
        StreamReader streamReader = null;

        try
        {
            streamReader = new StreamReader(filepath);
            text = streamReader.ReadToEnd();
        }
        catch (Exception)
        {
            text = "Screen file not found.";
        }
        finally
        {
            if (streamReader != null)
            {
                streamReader.Close();
            }
        }
    }

    public Screen(string filepath, ConsoleColor newForegroundColor, ConsoleColor newBackgroundColor)
    {
        foregroundColor = newForegroundColor;
        backgroundColor = newBackgroundColor;
        StreamReader streamReader = null;

        try
        {
            streamReader = new StreamReader(filepath);
            text = streamReader.ReadToEnd();
        }
        catch (Exception)
        {
            text = "Screen file not found.";
        }
        finally
        {
            if (streamReader != null)
            {
                streamReader.Close();
            }
        }
    }

    public virtual void Draw()
    {
        // draws the full contents of the text file to the screen
        Console.SetCursorPosition(0, 0);
        Console.ForegroundColor = foregroundColor;
        Console.BackgroundColor = backgroundColor;
        Console.WriteLine(text);
    }
}