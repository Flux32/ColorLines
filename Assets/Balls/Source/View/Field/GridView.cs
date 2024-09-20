using UnityEngine;

namespace Balls.View.Field
{
    public class GridView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _dotPrefab;

        private SpriteRenderer[,] _grid;

        public void CreateGrid(int sizeX, int sizeY)
        {
            _grid = new SpriteRenderer[sizeX, sizeY];

            for (int x = 0; x < sizeX; x++)
            {
                for (int y = 0; y < sizeY; y++)
                {
                    _grid[x, y] = Instantiate(_dotPrefab, new Vector3(x, y), Quaternion.identity, transform);
                }
            }
        }
    }
}