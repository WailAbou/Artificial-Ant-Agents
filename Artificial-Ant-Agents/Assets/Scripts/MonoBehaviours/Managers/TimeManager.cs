using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeManager : Singleton<TimeManager>
{
    [Header("TimeManager Settings")]
    public Slider sliderTimeScale;
    public TMP_Text timeScale;
    public TMP_Text timer;
    public float minTimeScale = 0.1f;
    public float maxTimeScale = 5.0f;

    private void Start()
    {
        sliderTimeScale.minValue = minTimeScale;
        sliderTimeScale.maxValue = maxTimeScale;
        sliderTimeScale.value = Time.timeScale;
    }

    public void ChangeTimeStep(float newTimeScale)
    {
        float clampedTimeScale = Mathf.Clamp(newTimeScale, minTimeScale, maxTimeScale);
        Time.timeScale = clampedTimeScale;
        timeScale.SetText($"TimeScale: {clampedTimeScale.ToString("F2")}");
    }

    private void Update() => timer.SetText($"Time: {Time.time.ToString("F2")} seconden");
}
