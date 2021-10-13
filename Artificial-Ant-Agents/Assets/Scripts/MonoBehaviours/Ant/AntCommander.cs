using UnityEngine;
using System;

public class AntCommander : AntBase
{
    private void Start() => antStateHandler.RequestState(new IdleState(this, transform, Vector2.zero));

    private void Update()
    {
        BaseState baseState = antStateHandler.GetState();
        Func<AntAI, bool> isReturnState = (antAi => antAi.antStateHandler.GetState() is ReturnState);
        foreach (AntAI antAi in baseState.ClosestItems<AntAI, AntAI>(2, isReturnState))
        {
            // antAi.nextState = new ScoutState(this, transform, baseState.velocity);
        }
    }
}
