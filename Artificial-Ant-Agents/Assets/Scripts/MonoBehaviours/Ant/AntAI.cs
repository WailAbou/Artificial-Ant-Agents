using UnityEngine;

[RequireComponent(typeof(Ant))]
public class AntAI : AntBase
{
    private void Start() => ant.RequestState(new WalkState(ant, transform));

    private void Update() => ant.UpdateState();
}
