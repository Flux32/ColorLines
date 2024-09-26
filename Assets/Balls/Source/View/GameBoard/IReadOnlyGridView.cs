using Balls.Source.Core.Struct;
using UnityEngine;

namespace Balls.Source.View.GameBoard
{
    public interface IReadOnlyGridView
    {
        public float CellSize { get; }
        public Vector3 GridToWorldPosition(GridPosition gridPosition);
    }
}