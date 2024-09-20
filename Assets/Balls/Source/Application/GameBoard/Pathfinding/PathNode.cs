using Balls.Core;

public class PathNode
{
    public readonly PathNode FromNode;
    public readonly GridPosition Position;

    public PathNode(PathNode fromNode, GridPosition position)
    {
        FromNode = fromNode;
        Position = position;
    }

    public PathNode[] CreateNeighbours()
    {
        return new PathNode[]
        {
            new PathNode(this, Position + new GridPosition(0, 1)),
            new PathNode(this, Position + new GridPosition(0, -1)),
            new PathNode(this, Position + new GridPosition(-1, 0)),
            new PathNode(this, Position + new GridPosition(1, 0)),
        };
    }
}
