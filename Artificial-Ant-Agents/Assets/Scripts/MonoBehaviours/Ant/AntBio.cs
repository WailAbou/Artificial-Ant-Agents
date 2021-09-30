using System.Collections;
using UnityEngine;
using DG.Tweening;

public class AntBio : AntBase
{
    public GameObject pheromonePrefab;
    public float pheromoneDistance = 0.5f;
    public float pheromoneLifetime = 5.0f;

    private Vector3 lastPheromonePosition;

    private void Start()
    {
        antStateHandler.RequestState(new WanderState(this, transform, Vector2.zero));
        lastPheromonePosition = transform.position;
        StartCoroutine(ReleasePheromnes());
    }

    private void Update() => antStateHandler.UpdateState(OnUpdate);

    private void OnUpdate()
    {
        BaseState baseState = antStateHandler.GetState();
        if (lastFoodPosition != Vector3.zero && baseState is WanderState)
        {
            ((WanderState)baseState).direction = (lastFoodPosition - transform.position).normalized;
            if (Vector3.Distance(transform.position, lastFoodPosition) < 0.1f) lastFoodPosition = Vector3.zero;
        }
    }

    private IEnumerator ReleasePheromnes()
    {
        while (true)
        {
            GameObject pheromoneObject = Instantiate(pheromonePrefab, transform.position, transform.rotation);
            pheromoneObject.GetComponent<SpriteRenderer>().DOFade(0.0f, pheromoneLifetime).OnComplete(() => Destroy(pheromoneObject));

            Pheromone pheromone = pheromoneObject.GetComponent<Pheromone>();
            BaseState baseState = antStateHandler.GetState();
            if (baseState is WanderState) pheromone.Init(Pheromone.PheromoneType.Search);
            else if (baseState is ReturnState) pheromone.Init(Pheromone.PheromoneType.Return);

            yield return new WaitUntil(() => Vector2.Distance(transform.position, lastPheromonePosition) > pheromoneDistance);
            lastPheromonePosition = transform.position;
        }
    }
}
