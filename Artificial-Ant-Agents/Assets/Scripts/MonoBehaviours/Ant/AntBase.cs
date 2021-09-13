using UnityEngine;

public class AntBase : MonoBehaviour
{
    [HideInInspector] public Nest nest;

    protected Ant ant;

    protected virtual void Awake() => ant = GetComponent<Ant>();
}
