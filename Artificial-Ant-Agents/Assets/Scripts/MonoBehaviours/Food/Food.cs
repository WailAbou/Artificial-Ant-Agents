using UnityEngine;

public class Food : BaseSpawnable
{
    [HideInInspector] public bool isPickedUp;

    public void Pickup(Transform parent)
    {
        transform.position = parent.position + parent.up;
        transform.SetParent(parent);
        isPickedUp = true;
    }
}
