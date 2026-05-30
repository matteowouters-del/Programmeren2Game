namespace ProjectGame2526;

public class RevealCollectible : Collectible
{
    protected int amount = 2;
    public RevealCollectible(int newPosX, int newPosY, int newOffsetX, int newOffsetY)
        : base(newPosX, newPosY, 'R', ConsoleColor.Cyan, newOffsetX, newOffsetY)
    {
    }

    public override void ApplyEffect(Game game, Maze maze, Player player, Enemy enemy)
    {
        maze.RemoveRandomExit(amount);
    }
}