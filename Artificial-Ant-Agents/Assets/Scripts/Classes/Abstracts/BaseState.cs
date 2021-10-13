using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public abstract class BaseState
{
    public Vector2 velocity;

    protected AntStateHandler antStateHandler;
    protected AntBase antBase;
    protected Transform transform;
    protected float wanderStrength = 0.1f;

    private WorldManager worldManager;
    private Vector2 position;
    private Vector2 direction;
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

    public virtual void Enable(Action OnEnable) => OnEnable?.Invoke();
    public virtual void Update(Action OnUpdate) => OnUpdate?.Invoke();
    public virtual void Disable(Action OnDisable) => OnDisable?.Invoke();

    public List<T> ClosestItems<T, TO>(float radius = 1.0f, Func<T, bool> filter = null, Func<T, TO> order = null) where T : Component
    {
        filter = filter ?? (T => true);

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);

        colliders = colliders.Where(collider => collider.GetComponent<T>() != null)
                             .Where(collider => filter(collider.GetComponent<T>())).ToArray();

        if (order != null) colliders = colliders.OrderBy(collider => order(collider.GetComponent<T>())).ToArray();

        List<Transform> all = colliders.Select(collider => collider.transform)
                                    .OrderBy(t => Vector3.Distance(transform.position, t.position)).ToList();

        return all.Select(t => t.GetComponent<T>()).ToList();
    }

    public T ClosestItem<T, TO>(float radius = 1.0f, Func<T, bool> filter = null, Func<T, TO> order = null) where T : Component
    {
        T closest = ClosestItems<T, TO>(radius, filter, order)
                                .OrderBy(c => Vector3.Distance(transform.position, c.transform.position)).FirstOrDefault();

        return closest;
    }

    protected void Move(float maxSpeed = 2.0f)
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

    public void SetDirection(Vector3 newDirection)
    {
        direction = antBase.fov.ClosestFree(newDirection);
    }
}