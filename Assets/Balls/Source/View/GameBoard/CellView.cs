using System;
using Balls.Source.View.GameBoard.Balls;
using UnityEngine;

namespace Balls.Source.View.GameBoard
{
    public class CellView
    {
        private readonly CellBackground _cellBackground;

        public CellView(CellBackground cellBackground, BallView ballView = null)
        {
            _cellBackground = cellBackground;
            Ball = ballView;
        }
        
        public void AttachBall(BallView ball)
        {
            if (HasBall())
                throw new InvalidOperationException("Ball already placed");

            Ball = ball;
        }
        
        public bool HasBall() => Ball != null;
        public BallView Ball { get; private set; }
        
        public bool TryGetBall(out BallView ball)
        {
            ball = default;
        
            if (HasBall() == false)
                return false;
        
            ball = Ball;
            return true;
        }

        public void DetachBall()
        {
            Ball = null;
        }
        
        public void TransitToNormalState()
        {
            if (HasBall())
            {
                Ball.TransitToNormalState();
                return;
            }
            
            _cellBackground.TransitFromHoldToNormalState();
        }

        public void TransitToHoldState()
        {
            if (HasBall())
            {
                Ball.TransitToHoldState();
                return;
            }
            
            _cellBackground.TransitToHoldState();

        }
        
        public void TransitToHoldState(Color cursorColor)
        {
            if (HasBall())
                Ball.TransitToHoldState();
            else
                _cellBackground.TransitToHoldState(cursorColor);
        }

        public void TransitToPressedState()
        {
            if (HasBall() == false)
                _cellBackground.TransitToPressedState();
        }

        public void TransitToPerformedState()
        {
            if (HasBall() == false)
                _cellBackground.TransitFromPressedStateToHoldedState();
        }
    }
}