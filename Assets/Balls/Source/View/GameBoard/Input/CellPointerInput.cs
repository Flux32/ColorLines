using Balls.Source.Core.Struct;
using UnityEngine;

namespace Balls.Source.View.GameBoard.Input
{
    public class CellPointerInput
    {
        public GridPosition GetMouseCellPosition(Vector2 position, float cellSize)
        {
            int x = (int)Mathf.Round(position.x / cellSize);
            int y = (int)Mathf.Round(position.y / cellSize);
            return new GridPosition(x, y);
        }
    }
}