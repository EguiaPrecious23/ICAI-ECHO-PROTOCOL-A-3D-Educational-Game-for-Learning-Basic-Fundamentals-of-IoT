using System.Collections;
using TMPro;
using UnityEngine;

public class TwoLEDBuild : MonoBehaviour
{
    [Header("Disabled Esc")]
    public GameObject pauseESC;

    [Header ("Congrats")]
    public GameObject congratsText;

    [Header("Build 2D")]
    public GameObject build2D;
    public GameObject cameraBuild;

    [Header("Build Tools")]
    public GameObject Arduino;
    public GameObject Board;
    public GameObject LED;
    public GameObject LED2;
    public GameObject Resistor;
    public GameObject Resistor2;
    public GameObject RedWire;
    public GameObject RedWire2;
    public GameObject RedWire3;
    public GameObject BlackWire;
    public GameObject BlackWire2;

    [Header("Door")]
    public GameObject Cube;
    public GameObject Door;
    public GameObject exit;
    public GameObject exit2;

    [Header("LED Complete Build")]
    public GameObject LEDComBuild;

    [Header("Raycast Camera")]
    public Camera playerCamera;
    public float interactDistance = 3f;
    public KeyCode interactKey = KeyCode.E;

    [Header("Hover Text")]
    public GameObject hoverCanvas;
    public TextMeshProUGUI hoverText;
    public string defaultHoverMessage = " to build 2 LED Light";

    [Header("All Build Slots")]
    public ItemSlotTwoLED[] itemSlot;

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
        if (filledCount >= itemSlot.Length)
        {
            Debug.Log("✅ 2 LED Build Complete!");
            StartCoroutine(HandleTaskCompletion());
        }
    }

    private IEnumerator HandleTaskCompletion()
    {
        yield return new WaitForSeconds(2f);
        StartCoroutine(OnTaskComplete());
    }

    private IEnumerator OnTaskComplete()
    {
        Debug.Log("✅ 2 LED Light Build Task Complete!");
        feedback.text = " 2 LED Light Build Task Complete\n You can now power the 2 LED you made";

        // Turn off build mode
        build2D.SetActive(false);
        yield return new WaitForSeconds(1f);

        congratsText.SetActive(true);
        yield return new WaitForSeconds(5f);
        congratsText.SetActive(false);

        pauseESC.SetActive(true);

        taskBG.Stop();

        Collider col = GetComponent<Collider>();
        if (col != null)
            col.enabled = false;

        Cube.SetActive(true);
        Door.SetActive(true);
        exit.SetActive(false);
        exit2.SetActive(false);

        Arduino.SetActive(false);
        Board.SetActive(false);
        LED.SetActive(false);
        LED2.SetActive(false);
        Resistor.SetActive(false);
        Resistor2.SetActive(false);
        RedWire.SetActive(false);
        RedWire2.SetActive(false);
        RedWire3.SetActive(false);
        BlackWire.SetActive(false);
        BlackWire2.SetActive(false);

        LEDComBuild.SetActive(true);

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
