using Balls.Source.Core.Struct;
using Balls.Source.Logic.GameBoard;
using UnityEngine;

namespace Balls.Source.View.GameBoard
{
    public interface IReadOnlyGridView
    {
        public float CellSize { get; }
        public GridSize Size { get; }
        public Vector3 GridToWorldPosition(GridPosition gridPosition);
    }
}