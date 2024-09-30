using Balls.Source.Core.Struct;
using Balls.Source.Logic.GameBoard.Balls;

namespace Balls.Source.Logic.GameBoard.Detectors
{
    public interface IPatternDetector
    {
        Ball[] Detect(GridPosition position, IReadOnlyGrid grid);
    }
}