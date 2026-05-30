namespace ProjectGame2526;

public class MenuItem
{
    protected string name;
    protected GameState targetState;
    public string Name
    {
        get{return name;}
        set{name = value;}
    }
    public GameState TargetState
    {
        get{return targetState;}
        set{targetState = value;}
    }
    public MenuItem(string newName, GameState newTargetState)
    {
        Name = newName;
        targetState = newTargetState;
    }
    public void Activate(Game game)
    {
        if(TargetState == GameState.Playing)
        {
            game.StartNewGame();
        }
        game.CurrentGameState = targetState;
    }
}