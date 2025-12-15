using UnityEngine;

public class RunningLights : MonoBehaviour
{
    [Header("Multi Lights")]
    public GameObject[] lightObjects;

    [Header("Ontime & Delay")]
    public float lightOnTime = 0.5f;
    public float delayBetween = 0.05f;

    private void Start()
    {
        StartCoroutine(RunLights());
    }

    private System.Collections.IEnumerator RunLights()
    {
        while (true)
        {
            for (int i = 0; i < lightObjects.Length; i++)
            {
                for (int j = 0; j < lightObjects.Length; j++)
                    SetLight(lightObjects[j], false);

                SetLight(lightObjects[i], true);
                yield return new WaitForSeconds(lightOnTime);
            }

            yield return new WaitForSeconds(delayBetween);
        }
    }

    private void SetLight(GameObject obj, bool state)
    {
        Light light = obj.GetComponent<Light>();
        if (light != null)
            light.enabled = state;

        Renderer rend = obj.GetComponent<Renderer>();
        if (rend != null && rend.material.HasProperty("_EmissionColor"))
        {
            if (state)
                rend.material.EnableKeyword("_EMISSION");
            else
                rend.material.DisableKeyword("_EMISSION");
        }
    }
}
