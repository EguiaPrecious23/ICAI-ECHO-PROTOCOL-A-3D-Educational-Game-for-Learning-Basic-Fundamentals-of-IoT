using System.Collections;
using TMPro;
using UnityEngine;

public class Led3rdTask : MonoBehaviour
{
    [Header("LED 1 Settings")]
    public Renderer ledRenderer1;
    public Color onColor1 = Color.red;
    public Color offColor1 = Color.black;
    public float emissionIntensity1 = 1000f;

    [Header("LED 2 Settings")]
    public Renderer ledRenderer2;
    public Color onColor2 = Color.green;
    public Color offColor2 = Color.black;
    public float emissionIntensity2 = 1000f;

    [Header("Blink Settings")]
    public float blinkSpeed = 0.75f;

    [Header("Raycast Settings")]
    public Camera playerCamera;
    public float interactDistance = 3f;
    public KeyCode interactKey = KeyCode.E;

    [Header("Hover Text")]
    public GameObject hoverCanvas;
    public TextMeshProUGUI hoverText;
    public string defaultHoverMessage = " to power the LED lights";

    private bool isPowerOn = false;
    private Coroutine blinkRoutine;

    void Update()
    {
        ShowHoverText();

        if (Input.GetKeyDown(interactKey))
        {
            if (IsLookingAtThisObject())
            {
                TogglePower();
            }
        }
    }

    void ShowHoverText()
    {
        hoverCanvas?.SetActive(false);

        if (IsLookingAtThisObject())
        {
            hoverCanvas?.SetActive(true);
            if (hoverText != null)
                hoverText.text = defaultHoverMessage;
        }
    }

    bool IsLookingAtThisObject()
    {
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
        {
            return hit.collider.gameObject == gameObject;
        }
        return false;
    }

    void TogglePower()
    {
        isPowerOn = !isPowerOn;

        if (isPowerOn)
        {
            blinkRoutine = StartCoroutine(AlternateBlink());
        }
        else
        {
            if (blinkRoutine != null) StopCoroutine(blinkRoutine);
            UpdateEmission(ledRenderer1, offColor1, false);
            UpdateEmission(ledRenderer2, offColor2, false);
        }
    }

    IEnumerator AlternateBlink()
    {
        bool toggle = false;

        while (isPowerOn)
        {
            toggle = !toggle;

            // LED 1 on, LED 2 off
            if (toggle)
            {
                UpdateEmission(ledRenderer1, onColor1, true, emissionIntensity1);
                UpdateEmission(ledRenderer2, offColor2, false);
            }
            // LED 1 off, LED 2 on
            else
            {
                UpdateEmission(ledRenderer1, offColor1, false);
                UpdateEmission(ledRenderer2, onColor2, true, emissionIntensity2);
            }

            yield return new WaitForSeconds(blinkSpeed);
        }
    }

    void UpdateEmission(Renderer renderer, Color color, bool enable, float intensity = 1000f)
    {
        if (renderer == null) return;

        Material mat = renderer.material;
        Color targetColor = enable ? color * Mathf.LinearToGammaSpace(intensity) : color;

        mat.SetColor("_EmissionColor", targetColor);

        if (enable)
        {
            mat.EnableKeyword("_EMISSION");
            DynamicGI.SetEmissive(renderer, targetColor);
        }
        else
        {
            mat.DisableKeyword("_EMISSION");
            DynamicGI.SetEmissive(renderer, color);
        }
    }
}