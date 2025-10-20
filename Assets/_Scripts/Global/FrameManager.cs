using System.Collections;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class FrameRateManager : MonoBehaviour
{
    int MaxRate = 9999;
    public float TargetFrameRate = 60.0f;
    float currentFrameTime;
    [SerializeField] private Button frameSetButton;
    [SerializeField] private TextMeshProUGUI frameCount;

    void Awake()
    {
        SetFrameRate(60.0f);
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = MaxRate;
        currentFrameTime = Time.realtimeSinceStartup;
        StartCoroutine(WaitForNextFrame());
    }
    IEnumerator WaitForNextFrame()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            currentFrameTime += 1.0f / TargetFrameRate;
            var t = Time.realtimeSinceStartup;
            var sleepTime = currentFrameTime - t - 0.01f;
            if (sleepTime > 0)
                Thread.Sleep((int)(sleepTime * 1000));
            while (t < currentFrameTime)
                t = Time.realtimeSinceStartup;
        }
    }

    private void SetFrameRate(float newTargetedRate)
    {
        TargetFrameRate = newTargetedRate;
        frameCount.text = newTargetedRate + "fps";
    }

    public void ToggleFps()
    {
        if (TargetFrameRate == 60.0f) SetFrameRate(30.0f);
        else SetFrameRate(60.0f);
    }
}