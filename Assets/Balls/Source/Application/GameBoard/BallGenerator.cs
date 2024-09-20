using Balls.Core;
using System;
using System.Collections.Generic;

public class BallGenerator
{
    private const int BallsAmount = 3;

    public List<Ball> Generate(GameBoardGrid gameBoard)
    {
        List<Ball> balls = new List<Ball>(BallsAmount);

        for (int ballIndex = 0; ballIndex < BallsAmount; ballIndex++) 
        {
            Random random = new Random();

            Array ids = Enum.GetValues(typeof(BallId));
            int idIndex = random.Next(ids.Length);

            if (gameBoard.TryPlaceBall(GetRandomPosition(gameBoard), (BallId)idIndex, out Ball placedBall))
                balls.Add(placedBall);
        }

        return balls;
    }

    private GridPosition GetRandomPosition(GameBoardGrid gameBoard)
    {
        Random random = new Random();

        return new GridPosition(random.Next(gameBoard.SizeX), random.Next(gameBoard.SizeY));
    }
}