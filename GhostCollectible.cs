namespace ProjectGame2526;

public class GhostCollectible : Collectible
{
    protected int amount;

    public GhostCollectible(int newPosX, int newPosY, int newOffsetX, int newOffsetY, int newAmount)
        : base(newPosX, newPosY, '°', ConsoleColor.White, newOffsetX, newOffsetY)
    {
        amount = newAmount;
    }

    public override void ApplyEffect(Game game, Maze maze, Player player)
    {
        // gives the player one extra wall pass
        player.GhostCharges += amount;
        //Draw player with ghostcolor
        player.Draw();
    }
}