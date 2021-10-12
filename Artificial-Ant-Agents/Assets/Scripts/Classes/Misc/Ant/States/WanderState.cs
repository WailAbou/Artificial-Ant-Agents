using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class WanderState : BaseState
{
    public WanderState(AntBase antBase, Transform transform, Vector2 velocity) : base(antBase, transform, velocity) { }

    public override void Update(Action OnUpdate)
    {
        Movement(OnUpdate);
        ClosestFood();
    }

    private void Movement(Action OnUpdate)
    {
        Vector3 desiredDirection = Random.insideUnitCircle * wanderStrength;
        SetDirection(desiredDirection);
        OnUpdate?.Invoke();
        Move();
    }

    private void ClosestFood()
    {
        Func<Food, bool> isNotPickedUp = (food => food.isPickedUp == false);
        Food food = ClosestItem<Food, Food>(1.0f, isNotPickedUp);
        if (food != null)
        {
            food.Pickup(transform);
            antBase.lastFoodPosition = food.transform.position;
            antStateHandler.RequestState(new ReturnState(antBase, transform, velocity));
        }
    }
}