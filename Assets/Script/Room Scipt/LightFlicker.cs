using System.Collections;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    [Header("Lights")]
    public GameObject Lights;

    [Header("Ceiling Lights")]
    public GameObject CeilingLights;

    [Header("Ceiling Bulb")]
    public GameObject CeilingBulb;

    [Header("Min & Max Delay")]
    public float minFlickerDelay = 0.5f;
    public float maxFlickerDelay = 2f;

    private bool hasTriggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            hasTriggered = true;
            StartCoroutine(FlickerForever());
        }
    }

    IEnumerator FlickerForever()
    {
        while (true)
        {
            bool newState = !Lights.activeSelf;

            Lights.SetActive(newState);
            CeilingBulb.SetActive(newState);
            CeilingLights.SetActive(newState);


            float waitTime = Random.Range(minFlickerDelay, maxFlickerDelay);
            yield return new WaitForSeconds(waitTime);
        }
    }
}