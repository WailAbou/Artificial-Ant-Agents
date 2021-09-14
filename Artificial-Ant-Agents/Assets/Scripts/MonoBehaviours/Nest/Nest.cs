using System.Collections.Generic;
using UnityEngine;

public class Nest : MonoBehaviour
{
    public List<AntBase> ants;
    [HideInInspector] public int foodCount;

    private void Start() => ants.ForEach(ant => ant.nest = this);
}
