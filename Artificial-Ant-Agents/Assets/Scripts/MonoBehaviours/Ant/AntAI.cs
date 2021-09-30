using UnityEngine;

public class AntAI : AntBase
{
    private void Start() => antStateHandler.RequestState(new WanderState(this, transform, Vector2.zero));

    private void Update() => antStateHandler.UpdateState(OnUpdate);

    private void OnUpdate()
    {
        BaseState baseState = antStateHandler.GetState();
        if (lastFoodPosition != Vector3.zero && baseState is WanderState)
        {
            ((WanderState)baseState).direction = (lastFoodPosition - transform.position).normalized;
            if (Vector3.Distance(transform.position, lastFoodPosition) < 0.1f) lastFoodPosition = Vector3.zero;
        }
    }
}
