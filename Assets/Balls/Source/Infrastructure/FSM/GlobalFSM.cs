using Balls.Core.FSM;
using Balls.Infrastructure.FSM.States;
using System;
using System.Collections.Generic;

namespace Balls.Infrastructure.FSM
{
    public sealed class GlobalFSM : Core.FSM.FSM //TODO: Rename
    {
        private readonly IGlobalFSMStateFactory _stateFactory;

        public GlobalFSM(IGlobalFSMStateFactory stateFactory)
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