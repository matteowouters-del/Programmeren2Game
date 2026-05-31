namespace ProjectGame2526;

public class RevealCollectible : Collectible
{
    protected int amount = 2;

    public RevealCollectible(int newPosX, int newPosY, int newOffsetX, int newOffsetY)
        : base(newPosX, newPosY, '.', ConsoleColor.Magenta, newOffsetX, newOffsetY)
    {
    }

    public override void ApplyEffect(Game game, Maze maze, Player player, Enemy enemy)
    {
        // removes a number of wrong exits from the maze
        maze.RemoveRandomExit(amount);
    }
}