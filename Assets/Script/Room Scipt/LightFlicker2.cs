using UnityEngine;
using System.Collections;

public class LightFlicker2 : MonoBehaviour
{
    [Header("")]
    public Light[] targetLights;

    [Header("Min & Max Time")]
    public float minTime = 0.05f;
    public float maxTime = 0.2f;

    [Header("Min & Max Intensity")]
    public float minIntensity = 0.3f;
    public float maxIntensity = 1f;

    private void Start()
    {
        if (targetLights == null || targetLights.Length == 0)
            targetLights = GetComponentsInChildren<Light>();

        StartCoroutine(FlickerRoutine());
    }

    private IEnumerator FlickerRoutine()
    {
        while (true)
        {
            foreach (Light light in targetLights)
            {
                if (light != null)
                    light.intensity = Random.Range(minIntensity, maxIntensity);
            }

            yield return new WaitForSeconds(Random.Range(minTime, maxTime));
        }
    }
}