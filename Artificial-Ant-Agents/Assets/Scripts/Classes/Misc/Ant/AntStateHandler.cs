using System;

public class AntStateHandler
{
    private readonly StateMachine stateMachine = new StateMachine();
    private BaseState nextState;

    public void UpdateState(Action OnUpdate = null)
    {
        stateMachine.Update(OnUpdate);
    }

    public void NextState(BaseState nextState)
    {
        this.nextState = nextState;
    }

    public void RequestState(BaseState requestedState, Action OnDisable = null, Action OnEnable = null)
    {
        requestedState = nextState ?? requestedState;
        nextState = null;

        if (requestedState is IdleState)
        {
            if (!(stateMachine.currentState is IdleState))
                stateMachine.ChangeState(requestedState, OnDisable, OnEnable);
        }

        if (requestedState is WanderState)
        {
            if (!(stateMachine.currentState is WanderState))
                stateMachine.ChangeState(requestedState, OnDisable, OnEnable);
        }

        if (requestedState is ReturnState)
        {
            if (!(stateMachine.currentState is ReturnState))
                stateMachine.ChangeState(requestedState, OnDisable, OnEnable);
        }

        if (requestedState is ScoutState)
        {
            if (!(stateMachine.currentState is ScoutState))
                stateMachine.ChangeState(requestedState, OnDisable, OnEnable);
        }
    }

    public BaseState GetState() => stateMachine.currentState;
}