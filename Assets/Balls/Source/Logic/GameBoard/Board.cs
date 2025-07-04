﻿using System;
using System.Collections.Generic;
using System.Linq;
using Balls.Source.Core.Struct;
using Balls.Source.Infrastructure.Factories;
using Balls.Source.Logic.GameBoard.Generators;
using Balls.Source.Logic.GameBoard.Operations;
using Balls.Source.Logic.GameBoard.Pathfinding;
using Balls.Source.Logic.GameBoard.Solvers;

namespace Balls.Source.Logic.GameBoard
{
    public sealed class Board
    {
        private IBallGenerator _ballGenerator;
        private IPathfinder _pathfinder;
        private ISolver _solver;

        private readonly IGameBoardModulesFactory _modulesFactory;
        
        private Grid _grid;
        
        public Board(IGameBoardModulesFactory modulesFactory)
        {
            _modulesFactory = modulesFactory;
        }
        
        public event Action<MoveOperationResult> Moved;
        public event Action Filled;
        
        public IReadOnlyGrid Grid => _grid;
        
        public GenerationOperationResult NewGame(GridSize gridSize)
        {
            _grid = new Grid(gridSize);

            _ballGenerator = _modulesFactory.CreateBallGenerator();
            _pathfinder = _modulesFactory.CreatePathfinder();
            _solver = _modulesFactory.CreateSolver();
            
            GenerationOperationResult generationOperationResult = _ballGenerator.Generate(_grid);
            return generationOperationResult;
        }

        public GenerationOperationResult RestartGame()
        {
            _grid = new Grid(_grid.Size);
            return _ballGenerator.Generate(_grid);
        }
        
        public MoveOperationResult MakeMove(GridPosition fromPosition, GridPosition toPosition) //TODO: visitor
        {
            if (_grid.IsBallExist(fromPosition) == false)
                return new MoveOperationResult(MoveResult.BallDoesNotExist);
            
            Path path = _pathfinder.FindPath(fromPosition, toPosition, _grid);

            if (path.Failed)
                return new MoveOperationResult(MoveResult.PathFailed, new BallMovingResult(path, _grid[fromPosition]));

            BallMovingResult ballMovingResult = null;
            
            if (_grid.TryReplaceBall(fromPosition, toPosition)) 
                ballMovingResult = new BallMovingResult(path, _grid[toPosition]);

            SolveResult solveResult = _solver.Solve(toPosition, _grid);
            GenerationOperationResult generationOperationResult = new GenerationOperationResult();
            
            if (solveResult.Balls.Count <= 0 || _grid.IsEmpty() == true)
                generationOperationResult = _ballGenerator.Generate(_grid);

            List<SolveResult> solvedBallsAfterGeneration = new List<SolveResult>(solveResult.Balls.Count);
            solvedBallsAfterGeneration.AddRange(generationOperationResult.SpawnedBalls
                        .Select(generatedBall => _solver.Solve(generatedBall.Position, _grid)));

            if (_grid.IsFilled() == true)
                Filled?.Invoke();

            MoveOperationResult moveOperation =
                new MoveOperationResult(MoveResult.Success, 
                    generationOperationResult, 
                    solveResult, 
                    solvedBallsAfterGeneration.AsReadOnly(), 
                    ballMovingResult);
            
            Moved?.Invoke(moveOperation);
            return moveOperation;
        }
    }
}