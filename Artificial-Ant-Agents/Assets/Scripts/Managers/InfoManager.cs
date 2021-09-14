using UnityEngine;
using TMPro;

public class InfoManager : Singleton<InfoManager>
{
    [Header("InfoManager Settings")]
    public GameObject canvas;
    public TMP_Text infoText;

    private Camera mainCam;

    public override void Awake()
    {
        base.Awake();
        mainCam = Camera.main;
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(mainCam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        IInfo info = hit.collider?.GetComponent<IInfo>();
        canvas.SetActive(info != null);

        if (info != null)
        {
            canvas.transform.position = hit.collider.transform.position + Vector3.up * 2;
            infoText.SetText(info.GetInfo());
        }
    }
}
