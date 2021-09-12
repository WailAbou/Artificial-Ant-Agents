using UnityEngine;

public static class VectorExtensions
{
    public static void SetPos(this Transform trans, float? x = null, float? y = null, float? z = null)
    {
        Vector3 changed = new Vector3(x ?? trans.position.x, y ?? trans.position.y, z ?? trans.position.z);
        trans.position = changed;
    }

    public static Vector3 With(this Vector3 v3, float? x = null, float? y = null, float? z = null)
    {
        return new Vector3(x ?? v3.x, y ?? v3.y, z ?? v3.z);
    }
}
