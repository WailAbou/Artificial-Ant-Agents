using UnityEngine;
using UnityEngine.UI;

public class TimeManager : Singleton<TimeManager>
{
    [Header("TimeManager Settings")]
    public Slider sliderTimeScale;
    public float minTimeScale = 0.1f;
    public float maxTimeScale = 5.0f;

    private void Start()
    {
        sliderTimeScale.minValue = minTimeScale;
        sliderTimeScale.maxValue = maxTimeScale;
        sliderTimeScale.value = Time.timeScale;
    }

    public void ChangeTimeStep(float timeScale)
    {
        float newTimeScale = Mathf.Clamp(timeScale, minTimeScale, maxTimeScale);
        Time.timeScale = newTimeScale;
    }
}
