using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class WanderState : BaseState
{
    public WanderState(Ant ant, AntBase antBase, Transform transform, Vector2 velocity) : base(ant, antBase, transform, velocity) { }

    public override void Update()
    {
        Movement();
        CheckFood();
    }

    private void Movement()
    {
        Vector2 direction = Random.insideUnitCircle * wanderStrength;
        if (antBase.lastFoodPosition != Vector3.zero)
        {
            direction = (antBase.lastFoodPosition - transform.position).normalized;
            if (Vector3.Distance(transform.position, antBase.lastFoodPosition) < 0.1f) antBase.lastFoodPosition = Vector3.zero;
        }
        Move(direction);
    }

    private void CheckFood()
    {
        Func<Food, bool> isNotPickedUp = (food => food.isPickedUp == false);
        Food food = CheckItem<Food>(1.0f, isNotPickedUp);
        if (food != null)
        {
            food.Pickup(transform);
            antBase.lastFoodPosition = food.transform.position;
            ant.RequestState(new ReturnState(ant, antBase, transform, velocity));
        }
    }
}