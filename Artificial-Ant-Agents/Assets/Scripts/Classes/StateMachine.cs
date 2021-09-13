public class StateMachine
{
    public BaseState currentState;

    public void ChangeState(BaseState newState)
    {
        currentState?.OnDisable();
        currentState = newState;
        currentState.OnEnable();
    }

    public void Update() => currentState?.Update();
}