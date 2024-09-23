using System;
using System.Collections.Generic;
using Balls.Source.Core.Struct;
using Balls.Source.Logic.GameBoard.Balls;

namespace Balls.Source.Logic.GameBoard
{
    public class RandomBallGenerator : IBallGenerator
    {
        private readonly int _ballsAmount;

        public RandomBallGenerator(int ballsAmount)
        {
            _ballsAmount = ballsAmount;
        }

        public List<Ball> Generate(Grid gameBoard)
        {
            List<Ball> balls = new List<Ball>(_ballsAmount);

            for (int ballIndex = 0; ballIndex < _ballsAmount; ballIndex++)
            {
                Random random = new Random();

                Array ids = Enum.GetValues(typeof(BallId));
                int idIndex = random.Next(ids.Length);

                if (gameBoard.TryPlaceBall(GetRandomPosition(gameBoard), (BallId)idIndex, out Balls.Ball placedBall))
                    balls.Add(placedBall);
            }

            return balls;
        }

        private GridPosition GetRandomPosition(Grid gameBoard)
        {
            Random random = new Random();

            return new GridPosition(random.Next(gameBoard.SizeX), random.Next(gameBoard.SizeY));
        }
    }
}