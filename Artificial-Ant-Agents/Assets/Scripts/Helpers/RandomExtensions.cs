using System.Collections.Generic;
using Random = UnityEngine.Random;

public static class RandomExtensions
{
    public static float Remap(this float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    public static T RandomItem<T>(this IList<T> list)
    {
        return list[Random.Range(0, list.Count)];
    }

    public static int RandomIndex<T>(this IList<T> list)
    {
        return Random.Range(0, list.Count);
    }
}
