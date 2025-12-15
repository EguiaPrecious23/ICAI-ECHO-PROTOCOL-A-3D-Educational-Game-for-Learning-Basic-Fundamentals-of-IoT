using System.Collections;
using TMPro;
using UnityEngine;

public class SRArduinoLED : MonoBehaviour
{
    [Header("LED Light")]
    public Renderer ledRenderer;
    public Color onColor;
    public Color offColor;
    public float emissionIntensity = 1000f;
    public float blinkSpeed = 0.75f;

    [Header("Raycast Camera")]
    public Camera playerCamera;
    public float interactDistance = 3f;
    public KeyCode interactKey = KeyCode.E;

    [Header("Hover Text")]
    public GameObject hoverCanvas;
    public TextMeshProUGUI hoverText;
    public string defaultHoverMessage = "to power the LED Light";

    private bool isPowerOn = false;
    private Coroutine blinkRoutine;

    void Update()
    {
        ShowHoverText();

        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
        {
            SRArduinoLED led = hit.collider.GetComponentInParent<SRArduinoLED>();
            if (led == this && Input.GetKeyDown(interactKey))
            {
                isPowerOn = !isPowerOn;

                if (isPowerOn)
                {
                    blinkRoutine = StartCoroutine(BlinkLED());
                }
                else
                {
                    if (blinkRoutine != null) StopCoroutine(blinkRoutine);
                    UpdateEmission(false);
                }
            }
        }
    }

    void ShowHoverText()
    {
        hoverCanvas?.SetActive(false);

        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
        {
            if (hit.collider.gameObject == gameObject)
            {
                hoverCanvas?.SetActive(true);
                if (hoverText != null) hoverText.text = defaultHoverMessage;
            }
        }
    }

    IEnumerator BlinkLED()
    {
        while (isPowerOn)
        {
            UpdateEmission(true);
            yield return new WaitForSeconds(blinkSpeed);
            UpdateEmission(false);
            yield return new WaitForSeconds(blinkSpeed);
        }
    }

    void UpdateEmission(bool powerOn)
    {
        if (ledRenderer != null)
        {
            Material mat = ledRenderer.material;

            if (powerOn)
            {
                Color targetColor = onColor * Mathf.LinearToGammaSpace(emissionIntensity);
                mat.SetColor("_EmissionColor", targetColor);

                mat.EnableKeyword("_EMISSION");
                DynamicGI.SetEmissive(ledRenderer, targetColor);
            }
            else
            {
                mat.SetColor("_EmissionColor", offColor);
                mat.DisableKeyword("_EMISSION");
                DynamicGI.SetEmissive(ledRenderer, offColor);
            }
        }
    }
}