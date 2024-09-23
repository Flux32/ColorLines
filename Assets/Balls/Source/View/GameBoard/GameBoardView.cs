using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using Reflex.Attributes;
using Balls.Source.Core.Struct;
using Balls.Source.Logic.GameBoard;
using Balls.Source.Logic.GameBoard.Balls;
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
        
        public void Select(GridPosition position)
        {
            if (_canSelect == false)
                return;
                
            BallView ballView = _ballViews[position.X, position.Y];
            
            if (ballView == null)
            {
                if (_selectedBall != null)
                {
                    _selectedBall.Unselect();
                    MoveOperationResult moveOperationResult = _gameBoard.MakeMove(_selectedBall.CellPosition, 
                                                                                  position);
                    _selectedBall = null;

                    if (moveOperationResult.Result == MoveResult.Success)
                        _jobExecutor.Execute(_cancellationTokenSource.Token, CreateJobs(moveOperationResult));
                }

                return;
            }

            if (ballView == _selectedBall)
                return;

            if (_selectedBall != null)
                _selectedBall.Unselect();

            ballView.Select();
            _selectedBall = ballView;
        }

        private IViewJob[] CreateJobs(MoveOperationResult moveOperationResult)
        {
            IViewJob[] jobs = {
                new MoveBallJob(moveOperationResult.MovedResult.Path, 10f, _ballViews),
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