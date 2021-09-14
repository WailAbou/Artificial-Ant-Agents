using UnityEngine;

[RequireComponent(typeof(Nest))]
public class NestInfo : MonoBehaviour, IInfo
{
    private Nest nest;

    private void Awake() => nest = GetComponent<Nest>();

    public string GetInfo() => $"Ants: {nest.ants.Count.ToString()}\nFoods: {nest.foodCount}";
}
