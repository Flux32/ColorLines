using System.Collections.Generic;
using Balls.Source.Core.Struct;
using Balls.Source.Logic.GameBoard.Balls;
using UnityEngine;

namespace Balls.Source.Logic.GameBoard
{
    public sealed class Grid : IReadOnlyGrid
    {
        private readonly Ball[,] _balls;
        private int _fillAmount;

        public Ball this[GridPosition position]
        { 
            get => _balls[position.X, position.Y];
            private set => _balls[position.X, position.Y] = value;
        }

        public GridSize Size { get; private set; }
        public bool IsFilled() => _fillAmount >= _balls.GetLength(0) * _balls.GetLength(1);

        public Grid(GridSize gridSize)
        {
            _balls = new Ball[gridSize.Width, gridSize.Height];
            Size = gridSize;
        }
        
        public IEnumerable<GridPosition> GetEmptyCells()
        {
            for (int x = 0; x < Size.Width; x++)
            {
                for (int y = 0; y < Size.Height; y++)
                {
                    GridPosition position = new GridPosition(x, y);
                    
                    if (this[position] == null)
                        yield return position;
                }
            }
        }
        
        public bool IsBallExist(GridPosition position)
        {
            return IsCellExist(position) && this[position] != null;
        }

        public bool IsCellExist(GridPosition position)
        {
            return position.X >= 0 
                   && position.Y >= 0 
                   && position.X < _balls.GetLength(0) 
                   && position.Y < _balls.GetLength(1);
        }

        public bool TryPlaceBall(GridPosition position, BallId ballId, out Ball placedBall)
        {
            placedBall = null;

            if (CanPlaceBall(position) == false)
                return false;

            placedBall = new Ball(ballId, position);
            this[position] = placedBall;
            _fillAmount += 1;
            return true;
        }
        
        public bool TryRemoveBall(GridPosition position)
        {
            this[position] = null;
            _fillAmount -= 1;
            return true;
        }

        public bool TryReplaceBall(GridPosition fromPosition, GridPosition toPosition)
        {
            if (IsBallExist(fromPosition) == false || IsBallExist(toPosition) == true)
                return false;

            this[toPosition] = this[fromPosition].WithPosition(toPosition);
            this[fromPosition] = null;
            return true;
        }
        
        public bool CanPlaceBall(GridPosition position)
        {
            return IsBallExist(position) == false;
        }

        public bool IsEmpty() => _fillAmount <= 0;
    }
}