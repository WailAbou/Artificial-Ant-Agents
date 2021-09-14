using UnityEngine;

public class WanderState : BaseState
{
    public WanderState(Ant ant, AntBase antBase, Transform transform, Vector2 velocity) : base(ant, antBase, transform, velocity) { }

    public override void Update()
    {
        Movement();
        CheckingFood();
    }

    private void Movement()
    {
        Vector2 direction = Random.insideUnitCircle * wanderStrength;
        Move(direction);
    }

    private void CheckingFood()
    {
        Food food = Check<Food>();
        if (food != null)
        {
            food.transform.position = transform.position + transform.up;
            food.transform.SetParent(transform);
            ant.RequestState(new ReturnState(ant, antBase, transform, velocity));
        }
    }
}