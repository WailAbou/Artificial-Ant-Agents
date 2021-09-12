using UnityEngine;
using TMPro;

public class AntInfo : MonoBehaviour
{
    public TMP_Text stateDisplay;

    public void UpdateInfo(string state)
    {
        stateDisplay.text = state;
    }
}
