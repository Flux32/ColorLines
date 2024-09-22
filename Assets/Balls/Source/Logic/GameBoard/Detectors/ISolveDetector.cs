using System.Collections.Generic;
using Balls.Source.Core.Struct;
using Balls.Source.Logic.GameBoard.Balls;

namespace Balls.Source.Logic.GameBoard.Detectors
{
    public interface ISolveDetector
    {
        Ball[] Detect(GridPosition position, Grid grid);
    }
}