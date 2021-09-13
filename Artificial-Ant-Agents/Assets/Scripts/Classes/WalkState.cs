using UnityEngine;

public class WalkState : BaseState
{
    private float maxSpeed = 2;
    private float steerStrenth = 2;
    private float wanderStrength = 0.1f;

    private Vector2 position;
    private Vector2 velocity;
    private Vector2 desiredDirection;

    public WalkState(Ant ant, Transform transform) : base(ant, transform) { }

    public override void Update()
    {
        Movement();
        CheckingFood();
    }

    private void Movement()
    {
        desiredDirection = (desiredDirection + Random.insideUnitCircle * wanderStrength).normalized;

        Vector2 desiredVelocity = desiredDirection * maxSpeed;
        Vector2 desiredSteeringForce = (desiredVelocity - velocity) * steerStrenth;
        Vector2 acceleration = Vector2.ClampMagnitude(desiredSteeringForce, steerStrenth);

        velocity = Vector2.ClampMagnitude(velocity + acceleration * Time.deltaTime, maxSpeed);
        position += velocity * Time.deltaTime;

        float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg - 90;
        transform.SetPositionAndRotation(position, Quaternion.Euler(0, 0, angle));
    }

    private void CheckingFood()
    {
        Food food = Check<Food>();
        if (food != null) ant.RequestState(new ReturnState(ant, transform));
    }
}