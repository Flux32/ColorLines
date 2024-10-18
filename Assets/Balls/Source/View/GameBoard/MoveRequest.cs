using Balls.Source.Core.Struct;

public class MoveRequest
{
    public MoveRequest(GridPosition fromPosition, GridPosition toPosition)
    {
        FromPosition = fromPosition;
        ToPosition = toPosition;
    }

    public GridPosition FromPosition { get; }
    public GridPosition ToPosition { get; }
}