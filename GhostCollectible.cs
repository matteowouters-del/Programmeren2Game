namespace ProjectGame2526;


public class GhostCollectible : Collectible
{
    protected int amount;


    public GhostCollectible(int newPosX, int newPosY, int newOffsetX, int newOffsetY, int newAmount)
        : base(newPosX, newPosY, 'G', ConsoleColor.Cyan, newOffsetX, newOffsetY)
    {
        amount = newAmount;
    }


    public override void ApplyEffect(Game game, Maze maze, Player player, Enemy enemy)
    {
        player.GhostCharges += amount;
    }
}