using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static bool Includes(this LayerMask layerMask, int layer)
    {
        return layerMask == (layerMask | (1 << layer));
    }

    public static Vector3 Closest(this IEnumerable<Vector3> points, Vector3 target)
    {
        Vector3 closestPoint = Vector3.zero;
        float closestDistance = Mathf.Infinity;
        foreach (Vector3 point in points)
        {
            float targetDistance = Vector3.Distance(target, point);
            if (targetDistance < closestDistance)
            {
                closestDistance = targetDistance;
                closestPoint = point;
            }
        }
        return closestPoint;
    }
}
