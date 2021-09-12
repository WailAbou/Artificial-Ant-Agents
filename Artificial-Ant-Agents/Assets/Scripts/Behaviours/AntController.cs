using UnityEngine;

public class AntController : MonoBehaviour
{
    private readonly StateMachine stateMachine = new StateMachine();

    public void UpdateState()
    {
        stateMachine.Update();
    }

    public void FixedUpdateState()
    {
        stateMachine.FixedUpdate();
    }

    public void RequestState(IState requestedState)
    {
        if (requestedState is IdleState)
        {
            if (!(stateMachine.currentState is IdleState))
                stateMachine.ChangeState(requestedState);
        }

        if (requestedState is WalkState)
        {
            if (!(stateMachine.currentState is WalkState))
                stateMachine.ChangeState(requestedState);
        }
    }

    public IState GetState() => stateMachine.currentState;
}