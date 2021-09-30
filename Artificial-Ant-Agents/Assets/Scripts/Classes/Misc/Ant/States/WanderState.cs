using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class WanderState : BaseState
{
    public Vector2 direction;

    public WanderState(AntBase antBase, Transform transform, Vector2 velocity) : base(antBase, transform, velocity) { }

    public override void Update(Action OnUpdate)
    {
        Movement(OnUpdate);
        CheckFood();
    }

    private void Movement(Action OnUpdate)
    {
        direction = Random.insideUnitCircle * wanderStrength;
        OnUpdate?.Invoke();
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
            antStateHandler.RequestState(new ReturnState(antBase, transform, velocity));
        }
    }
}