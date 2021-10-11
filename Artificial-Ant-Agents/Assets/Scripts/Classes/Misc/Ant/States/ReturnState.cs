using System;
using UnityEngine;

public class ReturnState : BaseState
{
    public ReturnState(AntBase antBase, Transform transform, Vector2 velocity) : base(antBase, transform, velocity) { }

    public override void Update(Action OnUpdate)
    {
        Movement();
        CheckNest();
        CheckSpoiled();
    }

    public override void Disable(Action OnDisable)
    {
        Food food = transform.GetComponentInChildren<Food>();
        if (food)
        {
            food.Drop();
            antBase.nest.foodCount++;
        }
    }

    private void Movement()
    {
        Vector2 direction = (antBase.nest.transform.position - transform.position).normalized;
        Move(direction, 1.5f);
    }

    private void CheckNest()
    {
        if (Vector2.Distance(transform.position, antBase.nest.transform.position) < 0.1f)
            antStateHandler.RequestState(new WanderState(antBase, transform, velocity));
    }

    private void CheckSpoiled()
    {
        if (!transform.GetComponentInChildren<Food>())
            antStateHandler.RequestState(new WanderState(antBase, transform, velocity));
    }
}
