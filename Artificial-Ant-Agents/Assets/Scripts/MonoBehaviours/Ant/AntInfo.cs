using UnityEngine;

[RequireComponent(typeof(Ant))]
public class AntInfo : MonoBehaviour, IInfo
{
    private Ant ant;

    private void Awake() => ant = GetComponent<Ant>();

    public string GetInfo() => $"State: {ant.GetState().ToString()}";
}
