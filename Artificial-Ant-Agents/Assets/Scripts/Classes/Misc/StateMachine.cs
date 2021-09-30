using System;

public class StateMachine
{
    public BaseState currentState;

    public void ChangeState(BaseState newState, Action OnDisable, Action OnEnable)
    {
        currentState?.Disable(OnDisable);
        currentState = newState;
        currentState.Enable(OnEnable);
    }

    public void Update(Action OnUpdate) => currentState?.Update(OnUpdate);
}