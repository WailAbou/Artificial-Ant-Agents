using System.Collections.Generic;
using UnityEngine;

public class ManualSpawnManager : Singleton<ManualSpawnManager>
{
    [Header("ManualSpawnManager Settings")]
    public List<SpawnItem> spawnItems = new List<SpawnItem>();

    private Camera mainCam;
    private Nest selectedNest;
    private KeyCode[] keys = new KeyCode[5] { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5 };

    public override void Awake()
    {
        base.Awake();
        mainCam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(mainCam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            selectedNest = hit.collider?.GetComponent<Nest>() ?? selectedNest;
        }

        for (int i = 0; i < keys.Length; i++)
        {
            if (Input.GetKeyDown(keys[i]))
            {
                SpawnItem spawnItem = spawnItems[i];
                GameObject spawnPrefab = Instantiate(spawnItem.prefab, (Vector2)mainCam.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
                spawnPrefab.transform.SetParent(spawnItem.holder);

                switch (i)
                {
                    case int n when (n < 2):
                        AntBase antBase = spawnPrefab.GetComponent<AntBase>();
                        antBase.nest = selectedNest;
                        break;
                }
            }
        }
    }
}
