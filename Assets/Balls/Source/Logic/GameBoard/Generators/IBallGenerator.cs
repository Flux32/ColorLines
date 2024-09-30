using Balls.Source.Logic.GameBoard.Operations;

namespace Balls.Source.Logic.GameBoard.Generators
{
    public interface IBallGenerator
    {
        GenerationOperationResult Generate(Grid grid);
    }
}