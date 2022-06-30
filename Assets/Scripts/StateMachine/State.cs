using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    [SerializeField] private List<ConditionalTransition> _transitions = new List<ConditionalTransition>();
    [SerializeField] private EndStateTransition _endStateTransition;

    protected Player Target;
    
    private void Awake()
    {
        enabled = false;
    }

    private void Update()
    {
        if (_endStateTransition == null ||
            _endStateTransition != null && _endStateTransition.NeedTransit == false)
        {
            DoAction();
            
            if (_endStateTransition != null)
                _endStateTransition.OnEndState();
        }
    }

    public virtual void Enter(Player target)
    {
        enabled = true;
        Target = target;

        foreach (var transition in _transitions)
            transition.Init(target);
        
        if (_endStateTransition != null)
            _endStateTransition.Init(target);
    }

    public void Exit()
    {
        enabled = false;
        
        foreach (var transition in _transitions)
            transition.enabled = false;

        if (_endStateTransition != null)
            _endStateTransition.enabled = false;
    }

    public bool TryGetNextState(out State targetState)
    {
        targetState = null;

        if (_endStateTransition != null && _endStateTransition.NeedTransit)
        {
            targetState = _endStateTransition.TargetState;
            return true;
        }
        
        foreach (var transition in _transitions)
        {
            if (transition.NeedTransit)
            {
                targetState = transition.TargetState;
                return true;
            }
        }

        return false;
    }

    protected abstract void DoAction();
}
