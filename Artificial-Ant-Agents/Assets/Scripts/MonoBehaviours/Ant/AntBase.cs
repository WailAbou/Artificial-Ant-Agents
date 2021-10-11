using UnityEngine;

public class AntBase : MonoBehaviour
{
    [HideInInspector] public Nest nest;
    [HideInInspector] public Food lastFood;
    [HideInInspector] public readonly AntStateHandler antStateHandler = new AntStateHandler();
}
