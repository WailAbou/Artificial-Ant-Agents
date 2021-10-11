using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSpawnManager : Singleton<AutoSpawnManager>
{
    [Header("AutoSpawnManager Settings")]
    public bool randomSeed = true;
    public string seed = "";
    public List<ClusterGroup> clusterGroups;

    private WorldManager worldManager;

    private void Start()
    {
        worldManager = WorldManager.Instance;
        InitRandom();
        clusterGroups.ForEach(clusterGroup => StartCoroutine(SpawnClusters(clusterGroup.cluster, clusterGroup.holder)));
    }

    private void InitRandom()
    {
        if (randomSeed == true)
        {
            int value = Random.Range(-10000, 10000);
            seed = value.ToString();
        }
        Random.InitState(seed.GetHashCode());
    }

    private IEnumerator SpawnClusters(Cluster cluster, Transform holder)
    {
        do
        {
            cluster.totalAmount = 0;
            int numClusters = Random.Range(cluster.minClusters, cluster.maxClusters);
            for (int i = 0; i < numClusters; i++)
            {
                int x = Random.Range(-worldManager.worldSize.x, worldManager.worldSize.x);
                int y = Random.Range(-worldManager.worldSize.y, worldManager.worldSize.y);
                int clusterSize = Random.Range(cluster.minClusterSize, cluster.maxClusterSize);
                cluster.totalAmount += clusterSize;
                for (int j = 0; j < clusterSize; j++) { SpawnObject(cluster, holder, new Vector3(x, y, 0)); }
            }
            float startAmount = cluster.totalAmount;

            yield return new WaitForSeconds(cluster.minRespawnTime);
            yield return new WaitUntil(() => (cluster.totalAmount / startAmount * 100) < cluster.minPercentage);
        }
        while (cluster.respawnable);
    }

    private void SpawnObject(Cluster cluster, Transform holder, Vector3 pos)
    {
        Vector2 offset = Random.insideUnitCircle * cluster.innerSpread;
        Vector3 spawnPos = pos + new Vector3(offset.x, offset.y, 0);
        Quaternion spawnRotation = cluster.randomRotation ? Quaternion.Euler(0, 0, Random.Range(0.0f, 360.0f)) : Quaternion.identity;

        GameObject spawnObject = Instantiate(cluster.prefab, spawnPos, spawnRotation);
        spawnObject.GetComponent<BaseSpawnable>().Init(cluster);

        float size = Random.Range(cluster.minSize, cluster.maxSize);
        spawnObject.transform.localScale = new Vector3(size, size, 1);
        spawnObject.transform.SetParent(holder);
    }
}
