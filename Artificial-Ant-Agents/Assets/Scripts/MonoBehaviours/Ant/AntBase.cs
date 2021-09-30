using UnityEngine;

public class AntBase : MonoBehaviour
{
    [HideInInspector] public Nest nest;
    [HideInInspector] public Vector3 lastFoodPosition;
    [HideInInspector] public readonly AntStateHandler antStateHandler = new AntStateHandler();
}
