using System;
using System.Threading;
using System.Collections.Generic;
using Balls.Core.StateMachine;
using Balls.Source.Core.Struct;
using Balls.Source.Logic.GameBoard;
using Balls.Source.View.Factories;
using Balls.Source.View.GameBoard.Grid;
using Balls.Source.View.GameBoard.States;

namespace Balls.Source.View.GameBoard
{
    public sealed class BoardView : Fsm, IDisposable
    {
        private readonly GridView _gridView;
        private readonly IJobFactory _jobFactory;
        private readonly Board _board;

        private readonly IJobExecutor _jobExecutor = new JobExecutor();
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public IReadOnlyGridView Grid => _gridView;
        
        public BoardView(
            Board board, 
            IJobFactory jobFactory,
            GridView gridView)
        {
            _board = board;
            _jobFactory = jobFactory;
            _gridView = gridView;
        }

        protected override Dictionary<Type, IState> InitializeStates()
        {
            return new Dictionary<Type, IState>()
            {
                [typeof(CreateGameBoardState)] = new CreateGameBoardState(_jobFactory, _jobExecutor, _board, _gridView, this),
                [typeof(IdleGameBoardState)] = new IdleGameBoardState(this),
                [typeof(ChoiceTargetPositionState)] = new ChoiceTargetPositionState(_gridView, this),
                [typeof(MakeMoveBoardState)] = new MakeMoveBoardState(_jobExecutor, _jobFactory, _board, _gridView, this),
                [typeof(FilledBoardState)] = new FilledBoardState(),
                [typeof(RestartBoardState)] = new RestartBoardState(_jobFactory, _jobExecutor, _board, this),
            };
        }
        
        public void StartNewGame(GridSize size)
        {
            Initialize<CreateGameBoardState, GridSize>(size);
        }

        public void RestartGame()
        {

        }
        
        public void Input(BoardInputAction action, GridPosition position)
        { 
            if (_gridView.IsCellExist(position) == false)
                return;
            
            Trigger(new InputCommand(action, _gridView[position], position));
        }
        
        public void Dispose()
        {
            _cancellationTokenSource.Dispose();
        }
    }
}