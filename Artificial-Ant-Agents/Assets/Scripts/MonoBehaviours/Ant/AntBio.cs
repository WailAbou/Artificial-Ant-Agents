using UnityEngine;

[RequireComponent(typeof(Ant))]
public class AntBio : AntBase
{
    private void Start() => ant.RequestState(new WanderState(ant, this, transform, Vector2.zero));

    private void Update() => ant.UpdateState();
}
