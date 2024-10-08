using Balls.Source.Core.Struct;
using Balls.Source.Logic.GameBoard;
using UnityEngine;

namespace Balls.Source.View.GameBoard
{
    public sealed class GridView : MonoBehaviour, IReadOnlyGridView
    {
        [SerializeField] private CellBackground _cellBackgroundPrefab;
        [SerializeField] private float _cellSize = 0.8f;
        
        private CellView[,] _cells;
        
        public float CellSize => _cellSize;
        public Bounds Bounds { get; private set; } = new Bounds(Vector3.zero, Vector3.zero);
        public GridSize Size { get; private set;  } = new GridSize(0, 0);

        public CellView this[GridPosition position] => _cells[position.X, position.Y];

        public void CreateGrid(GridSize size)
        {
            _cells = new CellView[size.Width, size.Height];
            
            for (int x = 0; x < size.Width; x++)
            {
                for (int y = 0; y < size.Height; y++)
                    _cells[x, y] = new CellView(Instantiate(_cellBackgroundPrefab, new Vector3(x, y) * _cellSize, Quaternion.identity, transform));
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
                   gridPosition.Y < Size.Height &&
                   _cells[gridPosition.X, gridPosition.Y] != null ;
        }
        
        public bool IsBallExist(GridPosition position)
        {
            return IsCellExist(position) 
                   && _cells[position.X, position.Y].HasBall();
        }
        
        private Bounds CalculateBounds(GridSize gridSize)
        {
            Vector3 size = new Vector3(gridSize.Width, gridSize.Height) * _cellSize;
            float partCellSize = _cellSize / 2;
            return new Bounds(size / 2 - new Vector3(partCellSize, partCellSize), size);
        }
        
        #if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawCube(Bounds.center, Bounds.size);
        }
        #endif
    }
}