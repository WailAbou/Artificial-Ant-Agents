using UnityEngine;

[RequireComponent(typeof(Nest))]
public class NestInfo : MonoBehaviour, IInfo
{
    private Nest nest;

    private void Awake() => nest = GetComponent<Nest>();

    public string GetInfo() => $"This nest has: {nest.ants.Count.ToString()} ants";
}
