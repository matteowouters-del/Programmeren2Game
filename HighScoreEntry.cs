namespace ProjectGame2526;

public class HighscoreEntry
{
    public string Name { get; set; }
    public int Score { get; set; }

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