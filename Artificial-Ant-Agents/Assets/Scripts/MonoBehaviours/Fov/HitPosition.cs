using UnityEngine;

public class HitPosition
{
    public RaycastHit2D hit;
    public Vector3 position;

    public HitPosition(RaycastHit2D hit, Vector3 position)
    {
        this.hit = hit;
        this.position = position;
    }
}
