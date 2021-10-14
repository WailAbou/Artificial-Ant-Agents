using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class AntCommander : AntBase
{
    private void Start() => antStateHandler.RequestState(new IdleState(this, transform, Vector2.zero));

    private void Update()
    {
        BaseState baseState = antStateHandler.GetState();
        HandleReturnAnts(baseState);
        HandleScoutAnts(baseState);
    }

    private void HandleReturnAnts(BaseState baseState)
    {
        Func<AntAI, bool> isReturnState = (antAi => antAi.antStateHandler.GetState() is ReturnState);
        foreach (AntAI antAi in baseState.ClosestItems<AntAI, AntAI>(2, isReturnState))
        {
            ScoutState scoutState = new ScoutState(antAi, antAi.transform, antAi.antStateHandler.GetState().velocity);
            antAi.antStateHandler.NextState(scoutState);
        }
    }

    private void HandleScoutAnts(BaseState baseState)
    {
        Func<AntAI, bool> isDoneScoutState = (antAi => (antAi.antStateHandler.GetState() is ScoutState) && ((ScoutState)antAi.antStateHandler.GetState()).hasScouted);
        Func<AntAI, int> biggestCluster = (antAi => ((ScoutState)antAi.antStateHandler.GetState()).foodCount);
        List<AntAI> scouterAnts = baseState.ClosestItems<AntAI, int>(2, isDoneScoutState, biggestCluster);
        Vector3 biggestClusterPosition = scouterAnts.Select(antAi => antAi.lastFoodPosition).FirstOrDefault();

        foreach (AntAI antAi in scouterAnts)
        {
            antAi.lastFoodPosition = biggestClusterPosition;
            WanderState wanderState = new WanderState(antAi, antAi.transform, antAi.antStateHandler.GetState().velocity);
            antAi.antStateHandler.RequestState(wanderState);
        }
    }
}
