using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] private State _startState;
    
    private Player _target;

    public State CurrentState { get; private set; }

    private void Start()
    {
        _target = FindObjectOfType<Player>();
        Reset();
    }

    private void Update()
    {
        if (CurrentState.TryGetNextState(out State nextState))
        {
            Transit(nextState);
        }
    }

    private void Reset()
    {
        if (_target == null)
            return;
        
        var allStates = GetComponents<State>();

        foreach (var state in allStates)
            state.Exit();

        CurrentState = _startState;
        CurrentState.Enter(_target);
    }

    private void Transit(State nextState)
    {
        if (CurrentState == null || _target == null)
            return;
        
        CurrentState.Exit();
        nextState.Enter(_target);
        CurrentState = nextState;
    }
}
