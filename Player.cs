using System;

namespace ProjectGame2526;

public class Player : MovingSprite
{
    public Player(int newspeed, int newPosX, int newPosY, char newSymbol, ConsoleColor newColor, int offsetX, int offsetY) : base(newspeed, newPosX, newPosY, newSymbol, newColor, offsetX, offsetY)
    {

    }
    public override bool Move(int direction, Maze maze)
    {
        int targetX = PosX;
        int targetY = PosY;

        switch (direction)
        {
            case 0:
                targetX--;
                break;
            case 1:
                targetX++;
                break;
            case 2:
                targetY++;
                break;
            case 3:
                targetY--;
                break;
            default:
                return false;
        }

        TileType tile = (TileType)maze.Grid[targetY, targetX];

        if (tile == TileType.Wall)
        {
            return false;
        }

        if (tile == TileType.Exit) //check if target tile is exit
        {
            if (maze.CheckWin(targetX, targetY)) //check if exit is correct
            {
                PosX = targetX;
                PosY = targetY;
                return true;
            }
            else
            {
                maze.RemoveWrongExit(targetX, targetY); //remove exit from list if wrong
                return false;
            }
        }

        PosX = targetX;
        PosY = targetY;
        return true;
    }
}
