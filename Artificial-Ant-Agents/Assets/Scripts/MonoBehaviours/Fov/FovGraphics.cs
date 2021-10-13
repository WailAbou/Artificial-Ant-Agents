using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class FovGraphics : MonoBehaviour
{
    private FovMechanics fov;
    private Mesh mesh;

    public void Init(Transform parent, FovMechanics fov)
    {
        transform.SetParent(parent);
        transform.position = parent.position;
        mesh = GetComponent<MeshFilter>().mesh;
        GetComponent<MeshRenderer>().material.SetColor("_UnlitColor", new Color(0.0f, 0.0f, 0.0f, 150.0f));
        this.fov = fov;
    }

    public void UpdateGraphics(List<Vector3> positions)
    {
        if (positions.Count > 0)
        {
            if (fov.debugMesh) DrawMesh(positions);
            if (fov.debugLines) DrawLines(positions);
        }
    }

    private void DrawMesh(List<Vector3> positions)
    {
        int vertexCount = positions.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3];
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
