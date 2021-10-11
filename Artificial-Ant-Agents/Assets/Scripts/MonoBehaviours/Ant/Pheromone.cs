using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(SpriteRenderer))]
public class Pheromone : MonoBehaviour
{
    public float lifetime = 25.0f;

    public enum Type { Search, Return };
    [HideInInspector] public Type type;
    [HideInInspector] public Pheromone parent;
    [HideInInspector] public float lifePercentage = 1.0f;

    private SpriteRenderer sr;

    private Dictionary<Type, Color> colors = new Dictionary<Type, Color>() {
        { Type.Search, Color.blue }, { Type.Return, Color.red }
    };

    private void Awake() => sr = GetComponent<SpriteRenderer>();

    public void Init(Type type, Pheromone parent)
    {
        this.type = type;
        this.parent = parent?.type == type ? parent : null;
        sr.color = colors[type];
        StartDecay();
    }

    private void StartDecay()
    {
        DOTween.To(() => lifePercentage, x => lifePercentage = x, 0, lifetime);
        sr.DOFade(0, lifetime).OnComplete(() => Destroy(gameObject));
    }
}
