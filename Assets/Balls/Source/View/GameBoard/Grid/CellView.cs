using System;
using Balls.Source.View.GameBoard.Balls;
using UnityEngine;

namespace Balls.Source.View.GameBoard.Grid
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
        
        public void TransitFromHoldToIdleState()
        {
            if (HasBall())
                Ball.TransitToNormalState();
            
            _cellBackground.TransitFromHoldToNormalState();
        }

        public void TransitToHoldState()
        {
            if (HasBall())
                Ball.TransitToHoldState();
            
            _cellBackground.TransitToHoldState();

        }
        
        public void TransitToHoldState(Color cursorColor)
        {
            if (HasBall())
                Ball.TransitToHoldState();
            
            _cellBackground.TransitToHoldState(cursorColor);
        }

        public void TransitToPressedState()
        {
            if (HasBall() == false)
                _cellBackground.TransitToPressedState();
        }

        public void SelectCell()
        {
            if (HasBall())
                Ball.SetSelectedState();
        }

        public void UnselectCell()
        {
            if (HasBall())
                Ball.SetUnselectedState();
        }
    }
}