using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityBlocks.FSM
{
    public class BaseStateMachine
    {
        private BaseState _currentState;
        private readonly Dictionary<Type, BaseState> _states = new();
        private readonly List<TransitionRule> _transitions = new();
        private readonly List<DelayedRule> _delayedTransitions = new();
        private bool _isDebug;
        private SharedContext _context;
        private StateMachineLogger _logger;

        public BaseState CurrentState => _currentState;

        public BaseStateMachine(SharedContext context)
        {
            if (context == null)
            {
                _context = new SharedContext();
            }
            else
            {
                _context = context;
            }
        }

        public void AddState<TState>(TState state) where TState : BaseState
        {
            _states[typeof(TState)] = state;
            state.SetContext(_context);
            state.Init();
        }

        public void AddTransition<TFrom, TTo>(Func<bool> condition)
            where TFrom : BaseState
            where TTo : BaseState
        {
            _transitions.Add(new TransitionRule(typeof(TFrom), typeof(TTo), condition));
        }

        public void AddDelayedTransition<TFrom, TTo>(float delaySeconds)
            where TFrom : BaseState
            where TTo : BaseState
        {
            _delayedTransitions.Add(new DelayedRule
            {
                From = typeof(TFrom),
                To = typeof(TTo),
                Delay = delaySeconds
            });
        }

        public void Update()
        {
            _currentState?.Update();

            foreach (var transition in _transitions)
            {
                bool fromMatches = transition.From == null || _currentState.GetType() == transition.From;

                if (fromMatches && transition.Condition())
                {
                    Enter(transition.To);
                    break;
                }
            }

            for (var i = 0; i < _delayedTransitions.Count; i++)
            {
                var rule = _delayedTransitions[i];
                if (_currentState.GetType() == rule.From)
                {
                    if (!rule.Started)
                    {
                        rule.StartTime = Time.time;
                        rule.Started = true;
                    }

                    if (Time.time - rule.StartTime >= rule.Delay)
                    {
                        Enter(rule.To);
                        return;
                    }
                }
            }
        }

        private void Enter(Type stateType)
        {
            if (_states.TryGetValue(stateType, out var newState))
            {
                if (_currentState == newState)
                {
                    return;
                }

                _currentState?.OnExit();
                _currentState = newState;
                _currentState.OnEnter();
                if (_logger)
                    _logger.Log("> " + stateType.Name);

                foreach (var delayed in _delayedTransitions)
                {
                    delayed.Started = false;
                }
            }
            else
            {
                Debug.Log("State not registered: " + stateType);
            }
        }

        public void Enter<T>() where T : BaseState => Enter(typeof(T));

        public T GetState<T>() where T : BaseState
        {
            if (_states.TryGetValue(typeof(T), out var state))
            {
                return (T) state;
            }

            Debug.LogWarning("State not found: " + typeof(T).Name);
            return null;
        }

        public void AddAnyTransition<TTo>(Func<bool> condition) where TTo : BaseState
        {
            _transitions.Add(new TransitionRule(null, typeof(TTo), condition));
        }

        public void SetLogger(StateMachineLogger logger)
        {
            _logger = logger;
        }
    }
}