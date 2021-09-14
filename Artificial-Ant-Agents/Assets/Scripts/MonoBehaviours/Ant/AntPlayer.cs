using UnityEngine;

[RequireComponent(typeof(Ant))]
public class AntPlayer : AntBase
{
    private void Start() => ant.RequestState(new IdleState(ant, this, transform, Vector2.zero));

    private void Update()
    {
        CaptureInput();
        ant.UpdateState();
    }

    private void CaptureInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && ant.GetState() is IdleState) ant.RequestState(new WanderState(ant, this, transform, Vector2.zero));
        else if (Input.GetKeyDown(KeyCode.Space) && ant.GetState() is WanderState) ant.RequestState(new IdleState(ant, this, transform, Vector2.zero));
    }
}
