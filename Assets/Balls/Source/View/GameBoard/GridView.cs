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
        public Bounds Bounds { get; private set; } = new Bounds(Vector3.zero, Vector3.zero);
        public GridSize Size { get; private set;  } = new GridSize(0, 0);
        
        public void CreateGrid(GridSize size)
        {
            _dots = new SpriteRenderer[size.Width, size.Height];
            _balls = new BallView[size.Width, size.Height];
            
            for (int x = 0; x < size.Width; x++)
            {
                for (int y = 0; y < size.Height; y++)
                    _dots[x, y] = Instantiate(_dotPrefab, new Vector3(x, y) * _cellSize, Quaternion.identity, transform);
            }

            Size = size;
            Bounds = CalculateBounds(size);
        }
        
        public Vector3 GridToWorldPosition(GridPosition gridPosition)
        {
            return new Vector3(gridPosition.X, gridPosition.Y) * _cellSize;
        }

        public bool IsCellExist(GridPosition gridPosition)
        {
            return gridPosition.X >= 0 &&
                   gridPosition.Y >= 0 &&
                   gridPosition.X < Size.Width &&
                   gridPosition.Y < Size.Height;
        }
        
        private Bounds CalculateBounds(GridSize gridSize)
        {
            Vector3 size = new Vector3(gridSize.Width * _cellSize, gridSize.Height * _cellSize);
            return new Bounds(size / 2, size);
        }
    }
}