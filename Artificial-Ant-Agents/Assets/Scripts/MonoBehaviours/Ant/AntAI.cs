using UnityEngine;

public class AntAI : AntBase
{
    private void Start() => antStateHandler.RequestState(new WanderState(this, transform, Vector2.zero));

    private void Update() => antStateHandler.UpdateState(OnUpdate);

    private void OnUpdate()
    {
        BaseState baseState = antStateHandler.GetState();
        if (lastFood != null && baseState is WanderState)
        {
            ((WanderState)baseState).direction = (lastFood.transform.position - transform.position).normalized;
            if (Vector3.Distance(transform.position, lastFood.transform.position) < 0.1f) lastFood.transform.position = Vector3.zero;
        }
    }
}
