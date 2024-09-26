using Balls.Source.Core.Struct;
using UnityEngine;

public class CellPointerInput
{
    private readonly Camera _camera;

    public CellPointerInput(Camera camera)
    {
        _camera = camera;
    }

    public GridPosition GetMouseCellPosition(float cellSize)
    {
        Vector3 position = _camera.ScreenToWorldPoint(Input.mousePosition);

        int x = (int)Mathf.Round(position.x / cellSize);
        int y = (int)Mathf.Round(position.y / cellSize);
        return new GridPosition(x, y);
    }
}