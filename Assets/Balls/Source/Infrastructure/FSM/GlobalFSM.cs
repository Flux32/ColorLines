using Balls.Core.FSM;
using System;
using System.Collections.Generic;
using Balls.Infrastructure.Fsm.States;
using Balls.Source.Infrastructure.Factories;

namespace Balls.Infrastructure.Fsm
{
    public sealed class GlobalFSM : Core.FSM.FSM //TODO: Rename
    {
        private readonly IGlobalFsmStateFactory _stateFactory;

        public GlobalFSM(IGlobalFsmStateFactory stateFactory)
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