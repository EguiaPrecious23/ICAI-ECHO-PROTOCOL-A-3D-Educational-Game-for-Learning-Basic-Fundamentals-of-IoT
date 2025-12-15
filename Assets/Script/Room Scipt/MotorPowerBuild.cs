using TMPro;
using UnityEngine;

public class MotorPowerBuild : MonoBehaviour
{
    [Header("Motor Propeller")]
    public PropellerSpin propellerSpin;

    [Header("Raycast Camera")]
    public Camera playerCamera;
    public float interactDistance = 3f;
    public KeyCode interactKey = KeyCode.E;

    [Header("Hover Text")]
    public GameObject hoverCanvas;
    public TextMeshProUGUI hoverText;
    public string defaultHoverMessage = "Press [E] to power the servo motor";

    private bool isPowered = false;

    void Update()
    {
        ShowHoverText();

        // Center ray from camera
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
        {
            // Detect if we're looking at THIS object
            if (hit.collider.gameObject == gameObject)
            {
                // Press E to power on
                if (Input.GetKeyDown(interactKey) && !isPowered)
                {
                    PowerMotor();
                }
            }
        }
    }

    void ShowHoverText()
    {
        hoverCanvas.SetActive(false);

        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
        {
            if (hit.collider.gameObject == gameObject && !isPowered)
            {
                hoverCanvas.SetActive(true);
                hoverText.text = defaultHoverMessage;
            }
        }
    }

    void PowerMotor()
    {
        isPowered = true;
        hoverCanvas.SetActive(false);

        Debug.Log("⚡ Servo motor powered!");

        propellerSpin.enabled = true;
    }
}