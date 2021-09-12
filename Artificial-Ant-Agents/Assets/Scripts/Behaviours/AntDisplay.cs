using UnityEngine;

public class AntDisplay : MonoBehaviour
{
    public GameObject canvas;
    public AntInfo antInfo;

    private Camera mainCam;

    private void Awake() => mainCam = Camera.main;

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(mainCam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        AntController antController = hit.collider?.GetComponent<AntController>();
        canvas.SetActive(antController != null);

        if (antController != null)
        {
            canvas.transform.position = antController.transform.position + Vector3.up;
            antInfo.UpdateInfo(antController.GetState().ToString());
        }
    }
}
