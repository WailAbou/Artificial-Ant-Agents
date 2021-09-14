using UnityEngine;

public class AntBase : MonoBehaviour
{
    [HideInInspector] public Nest nest;
    [HideInInspector] public Vector3 lastFoodPosition;

    protected Ant ant;

    protected virtual void Awake() => ant = GetComponent<Ant>();
}
