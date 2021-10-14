using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class ScoutState : BaseState
{
    public int foodCount;
    public bool hasScouted;

    public ScoutState(AntBase antBase, Transform transform, Vector2 velocity) : base(antBase, transform, velocity) { }

    public override void Update(Action OnUpdate)
    {
        Movement();
    }

    private void Movement()
    {
        Vector3 targetDirection = ((hasScouted ? antBase.nest.transform.position : antBase.lastFoodPosition) - transform.position).normalized;
        if (antBase.lastFoodPosition == Vector3.zero) targetDirection = Random.insideUnitCircle * wanderStrength;

        if (!hasScouted && Vector3.Distance(antBase.lastFoodPosition, transform.position) < 1.0f)
        {
            foodCount = ClosestItems<Food, Food>(2.0f).Count;
            hasScouted = true;
        }

        SetDirection(targetDirection);
        Move();
    }
}