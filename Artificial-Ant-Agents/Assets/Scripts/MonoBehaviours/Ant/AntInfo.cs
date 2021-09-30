using UnityEngine;

[RequireComponent(typeof(AntBase))]
public class AntInfo : MonoBehaviour, IInfo
{
    private AntStateHandler antStateHandler;

    private void Awake() => antStateHandler = GetComponent<AntBase>().antStateHandler;

    public string GetInfo() => $"State: {antStateHandler?.GetState()?.ToString()}";
}
