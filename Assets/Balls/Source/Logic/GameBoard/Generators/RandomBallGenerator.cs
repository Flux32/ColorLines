using System;
using System.Collections.Generic;
using System.Linq;
using Balls.Source.Core.Struct;
using Balls.Source.Logic.GameBoard.Balls;
using UnityEngine;
using Random = System.Random;

namespace Balls.Source.Logic.GameBoard.Generators
{
    public class RandomBallGenerator : IBallGenerator
    {
        private readonly int _spawnBallsAmount;

        public RandomBallGenerator(int spawnBallsAmount)
        {
            _spawnBallsAmount = spawnBallsAmount;
        }

        public List<Ball> Generate(Grid grid)
        {
            GridPosition[] randomPositions = GetRandomPositions(grid, _spawnBallsAmount);
            List<Ball> balls = new List<Ball>(randomPositions.Length);
            
            UnityEngine.Debug.Log("randomPositionsAmount: " + randomPositions.Length);
            foreach (GridPosition randomPosition in randomPositions)
            {
                if (grid.TryPlaceBall(randomPosition, GetRandomBallId(), out Ball placedBall))
                    balls.Add(placedBall);
            }

            return balls;
        }

        private BallId GetRandomBallId()
        { 
            Random random = new Random();
            Array ids = Enum.GetValues(typeof(BallId));
            int idIndex = random.Next(ids.Length);
            return (BallId)idIndex;
        }
        
        private GridPosition[] GetRandomPositions(Grid gameBoard, int maxAmount)
        {
            List<GridPosition> emptyCellsPositions = gameBoard.GetEmptyCells().ToList();
            
            Debug.Log("EmptyCellsPositionAmount" + emptyCellsPositions.Count);
            int needPlaceAmount = Math.Clamp(emptyCellsPositions.Count, 0, maxAmount);
            
            GridPosition[] randomPositions = new GridPosition[needPlaceAmount];
            
            for (int i = 0; i < needPlaceAmount; i++)
            {
                Random random = new Random();
                int randomGridPosition = random.Next(emptyCellsPositions.Count);
                randomPositions[i] = emptyCellsPositions[randomGridPosition];
                emptyCellsPositions.RemoveAt(randomGridPosition);
            }

            return randomPositions;
        }
    }
}