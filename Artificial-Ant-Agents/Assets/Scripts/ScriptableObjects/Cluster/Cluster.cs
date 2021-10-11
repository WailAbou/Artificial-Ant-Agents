using UnityEngine;

[CreateAssetMenu(fileName = "Cluster", menuName = "ScriptableObjects/Clusters", order = 1)]
public class Cluster : ScriptableObject
{
    [Header("Cluster Settings")]
    public GameObject prefab;
    public int minClusters = 4;
    public int maxClusters = 8;
    public int minClusterSize = 5;
    public int maxClusterSize = 10;

    [Header("Spawn Settings")]
    public float minSize = 1;
    public float maxSize = 1;
    public float innerSpread = 1.0f;
    public bool randomRotation = true;

    [Header("Respawn Settings")]
    public float minRespawnTime = 5.0f;
    public int minPercentage = 50;
    public bool respawnable = true;

    [HideInInspector] public float totalAmount;
}
