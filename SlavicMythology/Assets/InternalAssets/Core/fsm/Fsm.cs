using System;
using System.Collections.Generic;
using FSM;

public class Fsm
{
    private FsmState _currentState;
    private Dictionary<Type, FsmState> _states = new();

    public void AddState(FsmState state)
    {
        _states.Add(state.GetType(), state);
    }

    public void SetState<T>() where T : FsmState
    {
        var type = typeof(T);

        if (_currentState.GetType() == type)
            return;

        if (_states.TryGetValue(type, out var newState))
        {
            _currentState?.Exit();

            _currentState = newState;
            _currentState.Enter();
        }
    }

    public void Update()
    {
        _currentState?.Update();
    }
}

public class FsmEnemy : Fsm
{
    private FsmStateEnemy _currentState;
    private Dictionary<Type, FsmStateEnemy> _states = new();

    public void AddState(FsmStateEnemy state)
    {
        _states.Add(state.GetType(), state);
    }
    
    public new void SetState<T>() where T : FsmStateEnemy
    {
        // получаем название класса
        var baseType = typeof(T);

        // Если текущее состояние уже является потомком данного типа, ничего не делаем
        if (_currentState?.GetType().BaseType == baseType)
            return;
        
        // Ищем в словаре состояние, у которого базовый класс равен baseType
        foreach (var kvp in _states)
        {
            var stateType = kvp.Key;
            if (stateType.BaseType == baseType)
            {
                var newState = kvp.Value;
                _currentState?.Exit();
                _currentState = newState;
                _currentState.Enter();
                return;
            }
        }
    }
    
    public new void Update()
    {
        _currentState?.Update();
    }

    public void Heal(float hp)
    {
        _currentState?.Heal(hp);
    }

    public void Hit(float hp)
    {
        _currentState?.Hit(hp);
    }
}