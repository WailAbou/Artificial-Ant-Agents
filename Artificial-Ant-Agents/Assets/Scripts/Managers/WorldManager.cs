using UnityEngine;

public class WorldManager : Singleton<WorldManager>
{
    [Header("WorldManager Settings")]
    public Vector2Int worldSize;

    public bool OutBounds(Vector2 pos)
    {
        bool xOut = pos.x > worldSize.x || pos.x < -worldSize.x;
        bool yOut = pos.y > worldSize.y || pos.y < -worldSize.y;
        return xOut || yOut;
    }
}
