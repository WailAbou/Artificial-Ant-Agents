using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    [Header("SpawnManager Settings")]
    public GameObject antPrefab;
    public GameObject foodPrefab;
    public Transform antHolder;
    public Transform foodHolder;

    private Camera mainCam;

    public override void Awake()
    {
        base.Awake();
        mainCam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject ant = Instantiate(antPrefab, (Vector2)mainCam.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
            ant.transform.SetParent(antHolder);
            AntBase antBase = ant.GetComponent<AntBase>();
            antBase.nest = FindObjectOfType<Nest>();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            GameObject food = Instantiate(foodPrefab, (Vector2)mainCam.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
            food.transform.SetParent(foodHolder);
        }
    }
}
