using UnityEngine;

[RequireComponent(typeof(AntController))]
public class AntAI : MonoBehaviour
{
    private AntController antController;

    private void Awake() => antController = GetComponent<AntController>();

    private void Start() => antController.RequestState(new IdleState(antController));

    private void Update() => antController.UpdateState();

    private void FixedUpdate() => antController.FixedUpdateState();
}
