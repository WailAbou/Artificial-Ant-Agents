using UnityEngine;

public class ReturnState : BaseState
{
    public ReturnState(Ant ant, AntBase antBase, Transform transform, Vector2 velocity) : base(ant, antBase, transform, velocity) { }

    public override void Update()
    {
        Vector2 direction = (antBase.nest.transform.position - transform.position).normalized;
        Move(direction);
        if (Vector2.Distance(transform.position, antBase.nest.transform.position) < 0.1f)
        {
            ant.RequestState(new WanderState(ant, antBase, transform, velocity));
        }
    }

    public override void OnDisable()
    {
        Food food = transform.GetChild(0).GetComponent<Food>();
        food.Destroy();
    }
    // Return home and make momvement in base state
}
