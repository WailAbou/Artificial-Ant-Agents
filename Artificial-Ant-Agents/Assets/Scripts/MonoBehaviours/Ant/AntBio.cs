using System.Collections;
using UnityEngine;
using System;

public class AntBio : AntBase
{
    public GameObject pheromonePrefab;
    public float pheromoneSpacing = 1.0f;

    private Pheromone lastPheromone;
    private bool possibleCluster;

    private void Start()
    {
        antStateHandler.RequestState(new WanderState(this, transform, Vector2.zero));
        StartCoroutine(ReleasePheromnes());
    }

    private void Update() => antStateHandler.UpdateState(OnUpdate);

    private void OnUpdate()
    {
        BaseState baseState = antStateHandler.GetState();
        Func<Pheromone, bool> isReturnType = (pheromone => pheromone.type is Pheromone.Type.Return);
        Func<Pheromone, float> newestLifeTime = (pheromone => pheromone.lifePercentage);
        Pheromone pheromone = baseState?.ClosestItem<Pheromone, float>(3.0f, isReturnType, newestLifeTime);
        if (pheromone != null) possibleCluster = true;

        if (baseState is WanderState && possibleCluster)
        {
            if (pheromone?.parent != null)
            {
                Vector3 target = (pheromone.transform.position + pheromone.parent.transform.position) / 2;
                Vector3 desiredDirection = (target - transform.position).normalized;
                baseState.SetDirection(desiredDirection);
            }
            else if (pheromone != null && pheromone.parent == null)
            {
                possibleCluster = false;
            }
        }
    }

    private IEnumerator ReleasePheromnes()
    {
        while (true)
        {
            GameObject pheromoneObject = Instantiate(pheromonePrefab, transform.position, transform.rotation);
            Pheromone pheromone = pheromoneObject.GetComponent<Pheromone>();
            BaseState baseState = antStateHandler.GetState();

            if (baseState is WanderState) pheromone.Init(Pheromone.Type.Search, lastPheromone);
            else if (baseState is ReturnState) pheromone.Init(Pheromone.Type.Return, lastPheromone);
            lastPheromone = pheromone;

            yield return new WaitUntil(() => Vector2.Distance(transform.position, lastPheromone.transform.position) > pheromoneSpacing);
        }
    }
}
