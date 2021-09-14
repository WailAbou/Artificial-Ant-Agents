using UnityEngine;

[RequireComponent(typeof(Ant))]
public class AntAI : AntBase
{
    private void Start() => ant.RequestState(new WanderState(ant, this, transform, Vector2.zero));

    private void Update() => ant.UpdateState();
}
