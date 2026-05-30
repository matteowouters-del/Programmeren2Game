namespace ProjectGame2526;


public class ScoreCollectible : Collectible
{
    protected int amount;


    public ScoreCollectible(int newPosX, int newPosY, int newOffsetX, int newOffsetY, int newAmount)
        : base(newPosX, newPosY, '$', ConsoleColor.DarkYellow, newOffsetX, newOffsetY)
    {
        amount = newAmount;
    }


    public override void ApplyEffect(Game game, Maze maze, Player player, Enemy enemy)
    {
        game.AddScore(amount);
    }
}