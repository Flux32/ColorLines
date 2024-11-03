using System;
using System.Collections.Generic;
using Balls.Core.StateMachine;
using UnityEngine;

namespace Balls.Source.Core.StateMachine
{
    public abstract class Fsm : IFsm, IUpdatable
    {
        private Dictionary<Type, IState> _states;

        private IState _currentState;
        private ITickableState _tickable;

        private bool _initialized;

        public event Action<Type> StateChanged;
        public Type CurrentState => _currentState.GetType();

        public void Update(float deltaTime)
        {
            if (_initialized == false)
                return;

            _tickable?.Tick(deltaTime);
        }

        public void Initialize<TState, TValue>(TValue value) where TState : IPayloadState<TValue>
        {
            CheckIfInitialized();

            _states = InitializeStates();
            _initialized = true;
            Enter<TState, TValue>(value);

        }

        public void Initialize<TState>() where TState : ISimpleState
        {
            CheckIfInitialized();
            _states = InitializeStates();
            _initialized = true;
            Enter<TState>();
            Debug.Log("Initialized");
        }

        protected abstract Dictionary<Type, IState> InitializeStates();

        public void Enter<TState>() where TState : ISimpleState
        {
            Debug.Log(typeof(TState).Name);
            CheckIfNotInititalized();
            ThrowExceptionIfStateNotRegistered<TState>();

            Enter(typeof(TState));
        }

        public void Enter<TState, TValue>(TValue value) where TState : IPayloadState<TValue>
        {
            Debug.Log(typeof(TState).Name);

            CheckIfNotInititalized();
            ThrowExceptionIfStateNotRegistered<TState>();

            Enter(typeof(TState), value);
        }

        private void CheckIfInitialized()
        {
            if (_initialized == true)
                throw new InvalidOperationException("FSM is already initialized");
        }

        private void CheckIfNotInititalized()
        {
            if (_initialized == false)
                throw new InvalidOperationException("FSM is not initialized");
        }

        private void ThrowExceptionIfStateNotRegistered<TState>() where TState : IState
        {
            Type stateType = typeof(TState);

            if (_states.ContainsKey(stateType) == false)
                throw new InvalidOperationException($"State {stateType} doesn't registered in fsm");
        }

        private void Enter(Type state)
        {
            TryExecuteExit(_currentState);
            _currentState = _states[state];
            BindTickable(_currentState);
            ExecuteEnterable();
            StateChanged?.Invoke(CurrentState);
        }

        private void Enter<TValue>(Type state, TValue value)
        {
            TryExecuteExit(_currentState);

            _currentState = _states[state];
            BindTickable(_currentState);
            ExecuteEnterableWithValue(value);
            StateChanged?.Invoke(CurrentState);
        }

        private void ExecuteEnterable()
        {
            if (_currentState is IEnterableState enterableState)
                enterableState.Enter();
            else
                throw new InvalidOperationException("State has not enter without value");
        }

        private void ExecuteEnterableWithValue<TValue>(TValue value)
        {
            if (_currentState is IEnterableState<TValue> enterableState)
                enterableState.Enter(value);
            else
                throw new InvalidOperationException("State has not enter value");
        }

        private void BindTickable(object state)
        {
            if (state is ITickableState tickableState)
                _tickable = tickableState;
            else
                _tickable = null;
        }

        private void TryExecuteExit(object state)
        {
            if (state is IExitableState exitableState)
                exitableState?.Exit();
        }

        public void Trigger(IFsmCommand command)
        {
            _currentState.Trigger(command);
        }
    }
}