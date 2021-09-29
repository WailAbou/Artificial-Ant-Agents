using System.Collections;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Ant))]
public class AntBio : AntBase
{
    public GameObject pheromonePrefab;
    public float pheromoneDistance = 0.5f;
    public float pheromoneLifetime = 5.0f;

    private Vector3 lastPheromonePosition;

    private void Start()
    {
        ant.RequestState(new WanderState(ant, this, transform, Vector2.zero));
        lastPheromonePosition = transform.position;
        StartCoroutine(ReleasePheromnes());
    }

    private void Update() => ant.UpdateState();

    private IEnumerator ReleasePheromnes()
    {
        while (true)
        {
            GameObject pheromoneObject = Instantiate(pheromonePrefab, transform.position, transform.rotation);
            pheromoneObject.GetComponent<SpriteRenderer>().DOFade(0.0f, pheromoneLifetime).OnComplete(() => Destroy(pheromoneObject));

            Pheromone pheromone = pheromoneObject.GetComponent<Pheromone>();
            if (ant.GetState() is WanderState) pheromone.Init(Pheromone.PheromoneType.Search);
            else if (ant.GetState() is ReturnState) pheromone.Init(Pheromone.PheromoneType.Return);

            yield return new WaitUntil(() => Vector2.Distance(transform.position, lastPheromonePosition) > pheromoneDistance);
            lastPheromonePosition = pheromoneObject.transform.position;
        }
    }
}
