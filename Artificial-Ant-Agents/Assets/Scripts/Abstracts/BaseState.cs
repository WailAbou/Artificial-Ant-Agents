using UnityEngine;
using System.Linq;

public abstract class BaseState
{
    protected Ant ant;
    protected Transform transform;

    public BaseState(Ant ant, Transform transform)
    {
        this.ant = ant;
        this.transform = transform;
    }

    public virtual void OnEnable() { }
    public virtual void Update() { }
    public virtual void OnDisable() { }

    protected T Check<T>(float radius = 1.0f)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
        Transform[] transforms = colliders.Where(c => c.GetComponent<T>() != null).Select(c => c.transform).ToArray();
        Transform closest = transforms.OrderBy(t => Vector3.Distance(transform.position, t.position)).FirstOrDefault();
        return closest != null ? closest.GetComponent<T>() : default(T);
    }
}