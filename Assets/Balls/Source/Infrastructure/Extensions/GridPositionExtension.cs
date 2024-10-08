using Balls.Source.Core.Struct;
using UnityEngine;

namespace Balls.Source.Infrastructure.Extensions
{
    public static class GridPositionExtension
    {
        public static Vector2 ToVector2(this GridPosition gridPosition)
        {
            return new Vector2(gridPosition.X, gridPosition.Y);
        }

        public static Vector3 ToVector3(this GridPosition gridPosition)
        {
            return new Vector3(gridPosition.X, gridPosition.Y, 0);
        }

        public static Vector3Int ToVector3Int(this GridPosition gridPosition)
        {
            return new Vector3Int(gridPosition.X, gridPosition.Y, 0);
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
}