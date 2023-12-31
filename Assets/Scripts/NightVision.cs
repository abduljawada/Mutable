using UnityEngine;
using UnityEngine.Rendering.Universal;

public class NightVision : MonoBehaviour
{
    private PlayerReferences PlayerReferences => GetComponent<PlayerReferences>();
    private Light2D Light2D => PlayerReferences.light2D;
    private float _originalFalloffIntensity;
    private void Start()
    {
        _originalFalloffIntensity = Light2D.falloffIntensity;
        Light2D.falloffIntensity = 0.5f;
    }

    private void OnDestroy()
    {
        Light2D.falloffIntensity = _originalFalloffIntensity;
    }
}
