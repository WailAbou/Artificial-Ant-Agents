using UnityEngine;

public class Ant : MonoBehaviour
{
    private readonly StateMachine stateMachine = new StateMachine();

    public void UpdateState()
    {
        stateMachine.Update();
    }

    public void RequestState(BaseState requestedState)
    {
        if (requestedState is IdleState)
        {
            if (!(stateMachine.currentState is IdleState))
                stateMachine.ChangeState(requestedState);
        }

        if (requestedState is WanderState)
        {
            if (!(stateMachine.currentState is WanderState))
                stateMachine.ChangeState(requestedState);
        }

        if (requestedState is ReturnState)
        {
            if (!(stateMachine.currentState is ReturnState))
                stateMachine.ChangeState(requestedState);
        }
    }

    public BaseState GetState() => stateMachine.currentState;
}