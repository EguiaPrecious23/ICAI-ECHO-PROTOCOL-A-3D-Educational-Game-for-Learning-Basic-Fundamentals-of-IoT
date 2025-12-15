using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNameInput : MonoBehaviour
{
    [Header("Input Text")]
    public GameObject inputUI;
    public TMP_InputField usernameField;

    public static string PlayerName;
    public static bool hasEnteredName = false;

    [Header("Crosshair")]
    public GameObject crosshair;

    [Header("Move Disabled")]
    public GameObject moveGuide;

    [Header("Resume")]
    public ResumeESC esc;

    [Header("Running UI & Script")]
    public Running runscript;
    public GameObject runningUI;

    [Header("Move Script")]
    public Movement movescript;

    [Header("Flashlight")]
    public FlashlightToggle flashlight;

    [Header("Inventory")]
    public GameObject inventory;

    [Header("Subtitle Text")]
    public TMP_Text subtitleText;
    public GameObject TextBG;

    private void Start()
    {
        // Always show input UI (no save check)
        runscript.enabled = false;
        movescript.enabled = false;
        runningUI.SetActive(false);
        flashlight.enabled = false;
        inventory.SetActive(false);
        moveGuide.SetActive(false);
        esc.enabled = false;
        crosshair.SetActive(false); // Hide crosshair during input

        inputUI.SetActive(true);
        usernameField.ActivateInputField();
    }

    private void Update()
    {
        if (inputUI.activeSelf && Input.GetKeyDown(KeyCode.Return))
        {
            OnContinueClicked();
        }
    }

    public void OnContinueClicked()
    {
        if (!string.IsNullOrWhiteSpace(usernameField.text))
        {
            PlayerName = usernameField.text;
            hasEnteredName = true;
            inputUI.SetActive(false);
            // Enable game elements
            movescript.enabled = true;
            flashlight.enabled = true;
            crosshair.SetActive(true);
            moveGuide.SetActive(true);
            esc.enabled = false;
            runscript.enabled = false;
            runningUI.SetActive(false);
            inventory.SetActive(false); // Or based on your logic

            subtitleText.gameObject.SetActive(false);
            TextBG.SetActive(false);

            Debug.Log("Welcome " + PlayerName);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Debug.LogWarning("Username is empty!");
            subtitleText.gameObject.SetActive(true);
            TextBG.SetActive(true);
            subtitleText.text = "Enter a username";
            usernameField.ActivateInputField();
        }
    }
}