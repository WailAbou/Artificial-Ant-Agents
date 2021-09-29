using UnityEngine;

[CreateAssetMenu(fileName = "Cluster", menuName = "ScriptableObjects/Clusters", order = 1)]
public class Cluster : ScriptableObject
{
    public GameObject prefab;
    public int minClusters = 4;
    public int maxClusters = 8;
    public int minClusterSize = 5;
    public int maxClusterSize = 10;
    public float innerSpread = 1.0f;
    public float minSize = 1;
    public float maxSize = 1;
    public bool randomRotation = true;
}
