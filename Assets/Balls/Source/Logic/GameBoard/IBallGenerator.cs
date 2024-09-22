using System.Collections.Generic;
using Balls.Source.Logic.GameBoard.Balls;

namespace Balls.Source.Logic.GameBoard
{
    public interface IBallGenerator
    {
        List<Ball> Generate(Grid gameBoard);
    }
}