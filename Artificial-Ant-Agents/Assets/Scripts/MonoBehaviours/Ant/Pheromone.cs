using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Pheromone : MonoBehaviour
{
    public enum PheromoneType { Search, Return };
    public PheromoneType pheromoneType;

    private SpriteRenderer spriteRenderer;

    private Dictionary<PheromoneType, Color> colors = new Dictionary<PheromoneType, Color>() {
        { PheromoneType.Search, Color.blue }, { PheromoneType.Return, Color.red }
    };

    private void Awake() => spriteRenderer = GetComponent<SpriteRenderer>();

    public void Init(PheromoneType pheromoneType)
    {
        this.pheromoneType = pheromoneType;
        spriteRenderer.color = colors[pheromoneType];
    }
}
