using Balls.Core.StateMachine;
using System;
using System.Collections.Generic;
using Balls.Infrastructure.StateMachine.States;
using Balls.Source.Infrastructure.Factories;

namespace Balls.Infrastructure.StateMachine
{
    public sealed class GlobalFsm : Fsm
    {
        private readonly IGlobalFsmStateFactory _stateFactory;

        public GlobalFsm(IGlobalFsmStateFactory stateFactory)
        {
            _stateFactory = stateFactory;
        }

        protected override Dictionary<Type, IState> InitializeStates()
        {
            return new Dictionary<Type, IState>()
            {
                [typeof(BootstrapState)] = _stateFactory.CreateBootstrapState(),
                [typeof(GameplayState)] = _stateFactory.CreateGameplayState(),
            };
        }
    }
}