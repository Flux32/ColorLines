﻿using Balls.Source.Core.Struct;
using Balls.Source.Logic.GameBoard;
using UnityEngine;

namespace Balls.Source.View.GameBoard.Grid
{
    public interface IReadOnlyGridView
    {
        public float CellSize { get; }
        public GridSize Size { get; }
        public Bounds Bounds { get; }
        public Vector3 GridToWorldPosition(GridPosition gridPosition);
    }
}