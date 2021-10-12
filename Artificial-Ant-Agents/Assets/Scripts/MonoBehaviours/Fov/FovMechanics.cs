using System.Collections.Generic;
using UnityEngine;

public class FovMechanics : MonoBehaviour
{
    [Header("FovMechanics Settings")]
    public LayerMask obstacleMask;
    public float viewRadius = 3.0f;
    public float viewAngle = 115.0f;
    [Range(0.1f, 1.0f)] public float meshResolution = 0.5f;
    public Vector3 offsetRotation;
    public bool debugMode = false;

    [HideInInspector] public List<Vector3> positions = new List<Vector3>();

    private void Start()
    {
        GameObject FOV = new GameObject("FOV", typeof(FovGraphics));
        FOV.transform.SetParent(gameObject.transform);
        FOV.transform.position = transform.position;

        MeshRenderer meshRenderer = FOV.GetComponent<MeshRenderer>();
        meshRenderer.material.SetColor("_UnlitColor", new Color(0.0f, 0.0f, 0.0f, 150.0f));
    }

    public Vector2 DirFromAngle(float angleDeg)
    {
        float radians = angleDeg * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(radians), Mathf.Sin(radians), 0.0f);
    }

    public Vector3 ClosestFree()
    {
        return Vector3.zero;
    }
}
