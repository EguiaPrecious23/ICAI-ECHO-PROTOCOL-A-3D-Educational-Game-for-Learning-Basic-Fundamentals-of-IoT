using System.Collections;
using TMPro;
using UnityEngine;

public class LEDBuild : MonoBehaviour
{
    [Header("Disabled Esc")]
    public GameObject pauseESC;

    [Header("Build 2D")]
    public GameObject build2D;
    public GameObject cameraBuild;

    [Header("Build Tools")]
    public GameObject Arduino;
    public GameObject Board;
    public GameObject Resistor;
    public GameObject LED;
    public GameObject Wire;
    public GameObject Wire2;

    [Header ("LED Complete Build")]
    public GameObject LEDComBuild;

    public GameObject secondTask;

    [Header("Raycast Camera")]
    public Camera playerCamera;
    public float interactDistance = 3f;
    public KeyCode interactKey = KeyCode.E;

    [Header("Hover Text")]
    public GameObject hoverCanvas;
    public TextMeshProUGUI hoverText;
    public string defaultHoverMessage = " to build LED light";

    [Header("All Build Slots")]
    public ItemSlot[] itemSlots;

    public AudioSource taskBG;

    public TMP_Text feedback;

    private int filledCount = 0;
    private Movement playerMovement;
    private bool isBuilding = false;

    void Start()
    {
        playerMovement = FindObjectOfType<Movement>();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Update()
    {
        ShowHoverText();

        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
        {
            if (hit.collider.gameObject == gameObject && Input.GetKeyDown(interactKey))
            {
                if (!isBuilding)
                    StartBuildMode();
            }
        }
    }

    void StartBuildMode()
    {
        pauseESC.SetActive(false);

        cameraBuild.SetActive(true);
        interactDistance = 0f;
        if (hoverCanvas) hoverCanvas.SetActive(false);

        isBuilding = true;
        build2D.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        playerCamera.enabled = false;
        if (playerMovement != null) playerMovement.enabled = false;
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

    public void NotifySlotFilled()
    {
        filledCount++;

        //// Check if all slots are filled
        if (filledCount >= itemSlots.Length)
        {
            Debug.Log("✅ LED Build Complete!");
            StartCoroutine(HandleTaskCompletion());
        }
    }

    private IEnumerator HandleTaskCompletion()
    {
        yield return new WaitForSeconds(2f);
        OnTaskComplete();
    }

    public void OnTaskComplete()
    {
        Debug.Log("✅ LED Build Task Complete!");
        feedback.text = "LED Build Task Complete\n You can now power the LED you made \n\nGo to your 2nd Activity";

        pauseESC.SetActive(true);

        taskBG.Stop();

        secondTask.SetActive(true);

        Collider col = GetComponent<Collider>();
        if (col != null)
            col.enabled = false;

        Arduino.SetActive(false);
        Board.SetActive(false);
        Resistor.SetActive(false);
        LED.SetActive(false);
        Wire.SetActive(false);
        Wire2.SetActive(false);

        LEDComBuild.SetActive(true);

        // Turn off build mode
        build2D.SetActive(false);

        cameraBuild.SetActive(false);
        playerCamera.enabled = true;

        // Restore camera and controls
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (playerMovement != null)
            playerMovement.enabled = true;

        // Optional: show a message, sound, or visual feedback
        // Example: UI popup or mission update
    }
}