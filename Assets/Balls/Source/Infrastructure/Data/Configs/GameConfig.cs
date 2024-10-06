using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Balls/Configs/GameConfig")]
public class GameConfig : ScriptableObject
{
    [Header("GameBoard")]
    [SerializeField] private Vector2Int _gridSize = new Vector2Int(9, 9);
    [SerializeField, Min(0)] private int _generationBallsAmount = 3;
    [FormerlySerializedAs("_minBallsToMatchAmount")] [SerializeField, Min(2)] private int _minBallsToSolveAmount = 5;

    [Header("Score")]
    [SerializeField, Min(0)] private int _scoreForBall;
    [SerializeField, Min(0)] private int _startIncreaseWhenSolveAmount;
    
    public Vector2Int GridSize => _gridSize;
    public int GenerationBallsAmount => _generationBallsAmount;
    public int MinBallsToSolveAmount => _minBallsToSolveAmount;
    
    public int ScoreForBall => _scoreForBall;
    public int StartIncreaseWhenSolveAmount => _startIncreaseWhenSolveAmount;
    
    #if UNITY_EDITOR
    private void OnValidate()
    {
        if (_gridSize.x < 0)
            _gridSize.x = 0;

        if (_gridSize.y < 0)
            _gridSize.y = 0;
    }
#endif
}
