using UnityEngine;

[RequireComponent(typeof(FovMechanics))]
public class AntBase : MonoBehaviour
{
    [HideInInspector] public Nest nest;
    [HideInInspector] public FovMechanics fov;
    [HideInInspector] public Vector3 lastFoodPosition;
    [HideInInspector] public readonly AntStateHandler antStateHandler = new AntStateHandler();

    protected virtual void Awake() => fov = GetComponent<FovMechanics>();
}
