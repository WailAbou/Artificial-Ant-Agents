using UnityEngine;
using System.Linq;

public abstract class BaseState
{
    protected Ant ant;
    protected AntBase antBase;
    protected Transform transform;

    protected float wanderStrength = 0.1f;
    private float maxSpeed = 2;
    private float steerStrenth = 2;

    protected Vector2 velocity;
    private Vector2 position;
    private Vector2 desiredDirection;

    public BaseState(Ant ant, AntBase antBase, Transform transform, Vector2 velocity)
    {
        this.ant = ant;
        this.antBase = antBase;
        this.transform = transform;
        this.velocity = velocity;
        position = transform.position;
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

    protected void Move(Vector2 direction)
    {
        if (transform.position.x > 17 || transform.position.x < -17 ||
            transform.position.y > 9 || transform.position.y < -9) direction = (Vector2.zero - (Vector2)transform.position);

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