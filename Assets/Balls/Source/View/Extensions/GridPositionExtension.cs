using Balls.Core;
using UnityEngine;

public static class GridPositionExtension
{
    public static Vector2 ToVector2(this GridPosition gridPosition)
    {
        return new Vector2(gridPosition.X, gridPosition.Y);
    }

    public static Vector2Int ToVector2Int(this GridPosition gridPosition)
    {
        return new Vector2Int(gridPosition.X, gridPosition.Y);
    }

    public static GridPosition ToGridPosition(this Vector2Int gridPosition)
    {
        return new GridPosition(gridPosition.x, gridPosition.y);
    }
}