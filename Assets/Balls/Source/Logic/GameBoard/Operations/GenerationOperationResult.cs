using System;
using System.Collections.ObjectModel;
using Balls.Source.Logic.GameBoard.Balls;

namespace Balls.Source.Logic.GameBoard.Operations
{
    public sealed class GenerationOperationResult
    {
        public GenerationOperationResult(ReadOnlyCollection<Ball> spawnedBalls)
        {
            SpawnedBalls = spawnedBalls;
        }

        public GenerationOperationResult()
        {
            SpawnedBalls = new ReadOnlyCollection<Ball>(Array.Empty<Ball>());
        }

        public ReadOnlyCollection<Ball> SpawnedBalls { get; private set; }
    }
}