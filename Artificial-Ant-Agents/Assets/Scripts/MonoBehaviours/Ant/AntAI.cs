using UnityEngine;

public class AntAI : AntBase
{
    public BaseState nextState;

    private void Start() => antStateHandler.RequestState(new WanderState(this, transform, Vector2.zero));

    private void Update() => antStateHandler.UpdateState(OnUpdate);

    private void OnUpdate()
    {
        BaseState baseState = antStateHandler.GetState();
        if (lastFoodPosition != Vector3.zero && baseState is WanderState)
        {
            Vector3 desiredDirection = (lastFoodPosition - transform.position).normalized;
            baseState.SetDirection(desiredDirection);
            if (Vector3.Distance(transform.position, lastFoodPosition) < 0.1f) lastFoodPosition = Vector3.zero;
        }
    }
}
