using System.IO;
using System.Text.Json;

namespace ProjectGame2526;

public class HighscoresScreen : Screen
{
    public HighscoresScreen() : base("HighScoresScreen.txt")
    {
    }

    public static int CompareHighscores(HighscoreEntry a, HighscoreEntry b)
    {
        //AI suggestion to sort highscoreEntries by score
        return a.Score.CompareTo(b.Score);
    }

    public override void Draw()
    {
        base.Draw();

        List<HighscoreEntry> highscores = LoadHighscores();
        int startY = LineCount + 2;

        Console.ForegroundColor = ConsoleColor.White;

        if (highscores.Count == 0)
        {
            Console.SetCursorPosition(10, startY);
            Console.WriteLine("No highscores yet");
        }
        else
        {
            for (int i = 0; i < highscores.Count && i < 5; i++)
            {
                Console.SetCursorPosition(10, startY + i);
                Console.WriteLine((i + 1) + ". " + highscores[i].Name + " - " + highscores[i].Score);
            }
        }

        Console.SetCursorPosition(10, startY + 7);
        Console.WriteLine("Press Enter to return");
    }

    public List<HighscoreEntry> LoadHighscores()
    {
        List<HighscoreEntry> highscores = new List<HighscoreEntry>();
        StreamReader streamReader = null;

        try
        {
            streamReader = new StreamReader("highscores.json");
            string jsonText = streamReader.ReadToEnd();

            if (jsonText != "")
            {
                highscores = JsonSerializer.Deserialize<List<HighscoreEntry>>(jsonText);
            }

            if (highscores == null)
            {
                highscores = new List<HighscoreEntry>();
            }

            // sorts highscores from low to high
            highscores.Sort(CompareHighscores);

            // reverses the list so the highest score comes first
            highscores.Reverse();
        }
        catch (Exception)
        {
            highscores = new List<HighscoreEntry>();
        }
        finally
        {
            if (streamReader != null)
            {
                streamReader.Close();
            }
        }

        return highscores;
    }
}