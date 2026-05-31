namespace ProjectGame2526;

public class HighscoreEntry
{
    protected string name;
    protected int score;

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public int Score
    {
        get { return score; }
        set { score = value; }
    }

    public HighscoreEntry()
    {
        Name = "";
        Score = 0;
    }

    public HighscoreEntry(string newName, int newScore)
    {
        Name = newName;
        Score = newScore;
    }
}