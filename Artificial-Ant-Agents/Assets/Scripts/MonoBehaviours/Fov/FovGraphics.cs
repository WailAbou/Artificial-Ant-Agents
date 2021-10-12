using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class FovGraphics : MonoBehaviour
{
    private FovMechanics fov;
    private Transform parent;
    private Mesh mesh;
    private Vector3[] vertices;
    private int[] triangles;
    private int stepCount;

    private void Start()
    {
        fov = GetComponentInParent<FovMechanics>();
        parent = fov.transform;
        mesh = GetComponent<MeshFilter>().mesh;
    }

    private void LateUpdate()
    {
        fov.positions = CastRays();
        if (fov.debugMode)
        {
            DrawMesh(fov.positions);
            DrawLines(fov.positions);
        }
    }

    private List<Vector3> CastRays()
    {
        List<Vector3> positions = new List<Vector3>();

        stepCount = Mathf.RoundToInt(fov.viewAngle * fov.meshResolution);
        float stepAngle = fov.viewAngle / stepCount;
        for (int i = 0; i < stepCount; i++)
        {
            float angle = parent.eulerAngles.y - fov.viewAngle / 2 + stepAngle * i;
            Vector3 dir = parent.rotation * Quaternion.Euler(fov.offsetRotation) * fov.DirFromAngle(angle);
            RaycastHit2D hit = Physics2D.Raycast(parent.position, dir, fov.viewRadius, fov.obstacleMask);
            positions.Add(parent.position + dir.normalized * (hit.collider ? hit.distance : fov.viewRadius));
        }

        return positions;
    }

    private void DrawMesh(List<Vector3> positions)
    {
        int vertexCount = positions.Count + 1;
        vertices = new Vector3[vertexCount];
        triangles = new int[(vertexCount - 2) * 3];
        vertices[0] = Vector3.zero;
        for (int i = 0; i < vertexCount - 1; i++)
        {
            vertices[i + 1] = transform.InverseTransformPoint(positions[i]);

            if (i < vertexCount - 2)
            {
                triangles[i * 3 + 2] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3] = i + 2;
            }
        }

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }

    private void DrawLines(List<Vector3> positions)
    {
        positions.ForEach(position => Debug.DrawLine(transform.position, position, Color.blue));
    }
}
