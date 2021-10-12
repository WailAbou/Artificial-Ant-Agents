using UnityEngine;

public static class Extensions
{
    public static bool Includes(this LayerMask layerMask, int layer)
    {
        return layerMask == (layerMask | (1 << layer));
    }
}
