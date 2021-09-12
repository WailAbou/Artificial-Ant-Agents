using UnityEngine;

[RequireComponent(typeof(AntController))]
public class AntPlayer : MonoBehaviour
{
    private AntController antController;

    private void Awake() => antController = GetComponent<AntController>();

    private void Start() => antController.RequestState(new WalkState(antController));

    private void Update()
    {
        CaptureInput();
        antController.UpdateState();
    }

    private void FixedUpdate() => antController.FixedUpdateState();

    private void CaptureInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && antController.GetState() is IdleState) antController.RequestState(new WalkState(antController));
        else if (Input.GetKeyDown(KeyCode.Space) && antController.GetState() is WalkState) antController.RequestState(new IdleState(antController));
    }
}
