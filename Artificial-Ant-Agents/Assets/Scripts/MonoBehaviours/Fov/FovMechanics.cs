using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FovMechanics : MonoBehaviour
{
    [Header("FovMechanics Settings")]
    public LayerMask obstacleMask;
    public float viewRadius = 3.0f;
    public float viewAngle = 115.0f;
    [Range(0.1f, 1.0f)] public float meshResolution = 0.5f;
    public Vector3 offsetRotation;
    public bool debugMesh = true;
    public bool debugLines = false;

    private List<HitPosition> hitPositions = new List<HitPosition>();
    private FovGraphics fovGraphics;

    private void Start()
    {
        CreateFovGraphics();
    }

    private void Update() => CastRays();

    private void LateUpdate() => fovGraphics?.UpdateGraphics(hitPositions.Select(hp => hp.position).ToList());

    private void CreateFovGraphics()
    {
        GameObject fovObj = new GameObject("FOV", typeof(FovGraphics));
        fovGraphics = fovObj.GetComponent<FovGraphics>();
        fovGraphics.Init(transform, this);
    }

    private void CastRays()
    {
        hitPositions.Clear();
        float stepCount = Mathf.RoundToInt(viewAngle * meshResolution);
        float stepAngle = viewAngle / stepCount;
        for (int i = 0; i < stepCount; i++)
        {
            float angle = transform.eulerAngles.y - viewAngle / 2 + stepAngle * i;
            Vector3 direction = transform.rotation * Quaternion.Euler(offsetRotation) * DirFromAngle(angle);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, viewRadius, obstacleMask);
            Vector3 position = transform.position + direction.normalized * (hit.collider ? hit.distance : viewRadius);
            hitPositions.Add(new HitPosition(hit, position));
        }
    }

    public Vector2 DirFromAngle(float angleDeg)
    {
        float radians = angleDeg * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(radians), Mathf.Sin(radians), 0.0f);
    }

    public Vector3 ClosestFree(Vector3 direction)
    {
        if (!Physics2D.Raycast(transform.position, direction, viewRadius, obstacleMask)) return direction;

        List<Vector3> openPositions = hitPositions.Where(hp => hp.hit.collider == null).Select(hp => hp.position).ToList();
        if (openPositions?.Count > 0) return (openPositions.Closest(direction) - transform.position).normalized;

        return direction;
    }
}
