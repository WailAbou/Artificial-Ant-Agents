using UnityEngine;

[RequireComponent(typeof(Ant))]
public class AntPlayer : AntBase
{
    private void Start() => ant.RequestState(new IdleState(ant, transform));

    private void Update()
    {
        CaptureInput();
        ant.UpdateState();
    }

    private void CaptureInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && ant.GetState() is IdleState) ant.RequestState(new WalkState(ant, transform));
        else if (Input.GetKeyDown(KeyCode.Space) && ant.GetState() is WalkState) ant.RequestState(new IdleState(ant, transform));
    }
}
