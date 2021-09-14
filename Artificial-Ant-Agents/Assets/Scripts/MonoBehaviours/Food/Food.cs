using System.Collections;
using UnityEngine;

public class Food : MonoBehaviour
{
    public bool isPickedUp;

    public void Pickup(Transform parent)
    {
        transform.position = parent.position + parent.up;
        transform.SetParent(parent);
        isPickedUp = true;
    }

    public void Drop() => Destroy(gameObject);
}
