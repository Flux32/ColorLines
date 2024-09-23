using Balls.Source.Core.Struct;
using UnityEngine;

public class CellPointerInput
{
    private readonly Camera _camera;

    public CellPointerInput(Camera camera)
    {
        _camera = camera;
    }

    public GridPosition GetMouseCellPosition()
    {
        Vector3 position = _camera.ScreenToWorldPoint(Input.mousePosition);

        int x = (int)Mathf.Round(position.x);
        int y = (int)Mathf.Round(position.y);
        return new GridPosition(x, y);
    }
}