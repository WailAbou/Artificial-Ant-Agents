using UnityEngine;
using System.Linq;
using System;

public abstract class BaseState
{
    protected AntStateHandler antStateHandler;
    protected AntBase antBase;
    protected Transform transform;
    protected Vector2 velocity;
    protected float wanderStrength = 0.1f;

    private WorldManager worldManager;
    private Vector2 position;
    private Vector2 desiredDirection;
    private float steerStrenth = 2;

    public BaseState(AntBase antBase, Transform transform, Vector2 velocity)
    {
        this.antStateHandler = antBase.antStateHandler;
        this.antBase = antBase;
        this.transform = transform;
        this.velocity = velocity;
        worldManager = WorldManager.Instance;
        position = transform.position;
    }

    public virtual void Enable(Action OnEnable) { OnEnable?.Invoke(); }
    public virtual void Update(Action OnUpdate) { OnUpdate?.Invoke(); }
    public virtual void Disable(Action OnDisable) { OnDisable?.Invoke(); }

    protected T CheckItem<T>(float radius = 1.0f, Func<T, bool> filter = null)
    {
        filter = filter ?? (T => true);

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);

        Transform[] transforms = colliders
            .Where(collider => collider.GetComponent<T>() != null)
            .Where(collider => filter(collider.GetComponent<T>()))
            .Select(collider => collider.transform).ToArray();

        Transform closest = transforms.OrderBy(t => Vector3.Distance(transform.position, t.position)).FirstOrDefault();

        return closest != null ? closest.GetComponent<T>() : default(T);
    }

    protected void Move(Vector2 direction, float maxSpeed = 2.0f)
    {
        if (worldManager.OutBounds(transform.position)) direction = (Vector2.zero - (Vector2)transform.position);

        desiredDirection = (desiredDirection + direction).normalized;

        Vector2 desiredVelocity = desiredDirection * maxSpeed;
        Vector2 desiredSteeringForce = (desiredVelocity - velocity) * steerStrenth;
        Vector2 acceleration = Vector2.ClampMagnitude(desiredSteeringForce, steerStrenth);

        velocity = Vector2.ClampMagnitude(velocity + acceleration * Time.deltaTime, maxSpeed);
        position += velocity * Time.deltaTime;

        float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg - 90;
        transform.SetPositionAndRotation(position, Quaternion.Euler(0, 0, angle));
    }
}