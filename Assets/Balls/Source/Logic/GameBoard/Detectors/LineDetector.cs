using System;
using System.Collections.Generic;
using System.Linq;
using Balls.Source.Core.Struct;
using Balls.Source.Logic.GameBoard.Balls;

namespace Balls.Source.Logic.GameBoard.Detectors
{
    public sealed class LineDetector : ISolveDetector
    {
        private readonly int _minLineSize;

        public LineDetector(int minLineSize)
        {
            _minLineSize = minLineSize;
        }

        public Ball[] Detect(GridPosition position, Grid grid)
        {
            if (grid.IsBallExist(position) == false)
                return Array.Empty<Ball>();

            BallId targetColor = grid[position].Id;

            HashSet<GridPosition> horizontal = DetectByDirection(targetColor, position, GridPosition.Left(), grid);
            horizontal.UnionWith(DetectByDirection(targetColor, position, GridPosition.Right(), grid));

            HashSet<GridPosition> vertical = DetectByDirection(targetColor, position, GridPosition.Up(), grid);
            vertical.UnionWith(DetectByDirection(targetColor, position, GridPosition.Down(), grid));

            HashSet<GridPosition> firstDiagonal = DetectByDirection(targetColor, position, new GridPosition(1, 1), grid);
            firstDiagonal.UnionWith(DetectByDirection(targetColor, position, new GridPosition(-1, -1), grid));

            HashSet<GridPosition> secondDiagonal = DetectByDirection(targetColor, position, new GridPosition(-1, 1), grid);
            firstDiagonal.UnionWith(DetectByDirection(targetColor, position, new GridPosition(1, -1), grid));

            HashSet<GridPosition> result = ValidateAndUnionLines(
                horizontal,
                vertical,
                firstDiagonal,
                secondDiagonal);

            return result.Select(p => grid[p]).ToArray();
        }

        private HashSet<GridPosition> DetectByDirection(BallId targetColor,
            GridPosition position,
            GridPosition direction,
            Grid grid)
        {
            GridPosition checkPosition = position;

            HashSet<GridPosition> result = new HashSet<GridPosition>();

            while (grid.IsBallExist(checkPosition) == true && grid[checkPosition].Id == targetColor)
            {
                result.Add(checkPosition);
                checkPosition += direction;
            }
            return result;
        }

        private HashSet<GridPosition> ValidateAndUnionLines(params HashSet<GridPosition>[] lines)
        {
            HashSet<GridPosition> unionLines = new HashSet<GridPosition>();

            foreach (HashSet<GridPosition> line in lines)
            {
                if (line.Count >= _minLineSize)
                    unionLines.UnionWith(line);
            }

            return unionLines;
        }
    }
}