using System.Collections.Generic;
using Balls.Source.Logic.GameBoard.Balls;

namespace Balls.Source.Logic.GameBoard.Generators
{
    public interface IBallGenerator
    {
        List<Ball> Generate(Grid gameBoard);
    }
}