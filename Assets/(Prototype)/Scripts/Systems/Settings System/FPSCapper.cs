using UnityEngine;

public class FPSCapper : MonoBehaviour
{
    [SerializeField] int vSyncEverFrames = 1;
    [SerializeField] int maxFPS = 60;

    private void Awake()
    {
        ApplySettings();
    }

    private void OnEnable()
    {
        ApplySettings();
    }

    private void ApplySettings()
    {
        QualitySettings.vSyncCount = vSyncEverFrames;
        Application.targetFrameRate = maxFPS;
    }
}
