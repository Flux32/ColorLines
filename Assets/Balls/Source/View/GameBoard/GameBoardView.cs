using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using Reflex.Attributes;
using Balls.Source.Core.Struct;
using Balls.Source.Logic.GameBoard;
using Balls.Source.Logic.GameBoard.Balls;
using Balls.Source.View.GameBoard.Balls;
using Balls.Source.View.GameBoard.Jobs;

namespace Balls.Source.View.GameBoard
{
    public class GameBoardView : MonoBehaviour, IDisposable
    {
        private IBallViewFactory _ballFactory;
        private readonly IJobExecutor _jobExecutor = new JobExecutor();
        
        private Logic.GameBoard.GameBoard _gameBoard;

        private BallView[,] _ballViews = new BallView[5,5];

        private BallView _selectedBall;
        private bool _canSelect = true;
        
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        
        [Inject]
        private void Constructor(Logic.GameBoard.GameBoard gameBoard, IBallViewFactory ballViewFactory)
        {
            _gameBoard = gameBoard;
            _ballFactory = ballViewFactory;
        }

        private void Start()
        {
            IEnumerable<Ball> placedBalls = _gameBoard.InitializeGame();
            _jobExecutor.Execute(_cancellationTokenSource.Token, new SpawnBallJob(_ballFactory, _ballViews, placedBalls));
        }

        private void OnEnable()
        {
            _gameBoard.Filled += Filled;
        }
        
        private void OnDisable()
        {
            _gameBoard.Filled -= Filled;
        }

        private void Filled()
        {
            _canSelect = false;
        }
        
        public async void Select(GridPosition position)
        {
            if (_canSelect == false)
                return;
                
            BallView ballView = _ballViews[position.X, position.Y];

            if (_selectedBall != null && _selectedBall == ballView)
                return;
            
            if (ballView == null && _selectedBall != null)
            { 
                MoveOperationResult moveOperationResult = _gameBoard.MakeMove(_selectedBall.CellPosition, position);
                UnselectBall();

                if (moveOperationResult.Result != MoveResult.Success) 
                    return;
                
                _canSelect = false;
                await _jobExecutor.Execute(_cancellationTokenSource.Token, CreateJobs(moveOperationResult));
                _canSelect = true;

                return;
            }

            UnselectBall();

            ballView.Select();
            _selectedBall = ballView;
        }

        private void UnselectBall()
        {
            if (_selectedBall == null) 
                return;
            
            _selectedBall.Unselect();
            _selectedBall = null;
        }
        
        private IViewJob[] CreateJobs(MoveOperationResult moveOperationResult)
        {
            IViewJob[] jobs = {
                new MoveBallJob(moveOperationResult.MovedResult.Path, _ballViews),
                new SolveBallJob(moveOperationResult.SolvedBalls, _ballViews),
                new SpawnBallJob(_ballFactory, _ballViews, moveOperationResult.BallsPlaced),
            };

            return jobs;
        }

        public void Dispose()
        {
            _cancellationTokenSource.Dispose();
        }
    }
}