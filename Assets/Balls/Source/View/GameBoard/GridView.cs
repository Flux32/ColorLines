using System;
using Balls.Source.Core.Struct;
using Balls.Source.Logic.GameBoard;
using Balls.Source.View.GameBoard.Balls;
using UnityEngine;

namespace Balls.Source.View.GameBoard
{
    public class GridView : MonoBehaviour, IReadOnlyGridView
    {
        [SerializeField] private SpriteRenderer _dotPrefab;
        [SerializeField] private float _cellSize = 0.8f;
        
        private SpriteRenderer[,] _dots;
        private BallView[,] _balls;

        public BallView this[GridPosition position]
        {
            get => _balls[position.X, position.Y];
            set => _balls[position.X, position.Y] = value;
        }

        public float CellSize => _cellSize;
        
        public void CreateGrid(GridSize size)
        {
            _dots = new SpriteRenderer[size.Width, size.Height];
            _balls = new BallView[size.Width, size.Height];
            
            for (int x = 0; x < size.Width; x++)
            {
                for (int y = 0; y < size.Height; y++)
                    _dots[x, y] = Instantiate(_dotPrefab, new Vector3(x, y) * _cellSize, Quaternion.identity, transform);
            }
        }
        
        public Vector3 GridToWorldPosition(GridPosition gridPosition)
        {
            return new Vector3(gridPosition.X, gridPosition.Y) * _cellSize;
        }
    }
}