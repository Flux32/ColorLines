using Balls.Source.View.GameBoard;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private GridView _gridView;

    private void Start()
    {
        _gridView.CreateGrid(5, 5);
    }
}