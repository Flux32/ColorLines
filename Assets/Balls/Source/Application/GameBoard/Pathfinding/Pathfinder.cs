using Balls.Core;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder
{
    private const int _findOperationsLimit = 10000;

    public Path FindPath(GridPosition startPosition, GridPosition endPosition, GameBoardGrid grid)
    {
        HashSet<GridPosition> closedPositions = new HashSet<GridPosition>();
        Queue<PathNode> openedNodes = new Queue<PathNode>();

        openedNodes.Enqueue(new PathNode(null, startPosition));
        closedPositions.Add(openedNodes.Peek().Position);

        int operationAmount = 0;

        PathNode finalNode = new PathNode(null, startPosition);

        bool pathFailed = true;

        if (IsObstacle(endPosition, grid) == false)
        {
            while (openedNodes.Count > 0 && operationAmount < _findOperationsLimit)
            {
                PathNode currentNode = openedNodes.Dequeue();

                if (currentNode.Position == endPosition)
                {
                    DebugPosition(currentNode.Position, Color.green, 0.3f);
                    finalNode = currentNode;
                    pathFailed = false;
                    break;
                }

                DebugPosition(currentNode.Position, Color.yellow, 0.3f);

                PathNode[] neighbours = currentNode.CreateNeighbours();

                foreach (PathNode neghbour in neighbours)
                {
                    GridPosition neigbourPosition = neghbour.Position;

                    if (closedPositions.Contains(neigbourPosition) == false)
                    {
                        closedPositions.Add(neigbourPosition);

                        if (IsObstacle(neigbourPosition, grid) == true)
                            continue;

                        openedNodes.Enqueue(neghbour);
                    }
                }

                operationAmount++;
            }
        }

        Debug.Log("Find operation amount: " + operationAmount);
        return new Path(ConvertNodeToPath(finalNode).ToArray(), pathFailed);
    }

    private bool IsObstacle(GridPosition position, GameBoardGrid grid)
    {
        return grid.IsCellExist(position) == false || grid.IsBallExist(position);
    }

    private List<GridPosition> ConvertNodeToPath(PathNode node)
    {
        List<GridPosition> wayPoints = new List<GridPosition>();

        while (node != null)
        {
            wayPoints.Add(node.Position);
            node = node.FromNode;
        }

        wayPoints.Reverse();

        return wayPoints;
    }

    private void DebugPosition(GridPosition position, Color color, float duration)
    {
        const float cubeRadius = -0.3f;

        Debug.DrawLine(position.ToVector2() + new Vector2(-cubeRadius, cubeRadius), position.ToVector2() + new Vector2(cubeRadius, cubeRadius), color, duration);
        Debug.DrawLine(position.ToVector2() + new Vector2(-cubeRadius, -cubeRadius), position.ToVector2() + new Vector2(cubeRadius, -cubeRadius), color, duration);
        Debug.DrawLine(position.ToVector2() + new Vector2(-cubeRadius, -cubeRadius), position.ToVector2() + new Vector2(-cubeRadius, cubeRadius), color, duration);
        Debug.DrawLine(position.ToVector2() + new Vector2(cubeRadius, -cubeRadius), position.ToVector2() + new Vector2(cubeRadius, cubeRadius), color, duration);
    }
}