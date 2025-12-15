using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Video;

public class LR3rdTask : MonoBehaviour
{
    [Header("Subtitle UI")]
    public TMP_Text subtitleText;

    [Header("Tools")]
    public Collider[] tools;

    [Header("Tool Placement Spots")]
    public ToolPlacement[] placements;

    [Header("Gray Tools")]
    public GameObject Arduino;
    public GameObject Breadboard;
    public GameObject led;
    public GameObject led2;
    public GameObject Resistor;
    public GameObject Resistor2;
    public GameObject Wire;
    public GameObject Wire2;
    public GameObject Wire3;
    public GameObject Wire4;
    public GameObject Wire5;

    [Header("Disabled Tools")]
    public Collider[] disabledTools;

    [Header("Motor Vid Tutorial")]
    public GameObject vidCanvas;
    public VideoPlayer vidTotur;

    [Header("Raycast Camera")]
    public Camera playerCamera;
    public float interactDistance = 3f;
    public KeyCode interactKey = KeyCode.E;

    [Header("Hover Text")]
    public GameObject hoverCanvas;
    public TextMeshProUGUI hoverText;
    public string defaultHoverMessage = "to start the 3rd and last task";

    [Header("Build LED")]
    public GameObject twoLEDBuild;

    public AudioSource taskBG;

    [Header("Video Replay")]
    public GameObject textPressR;
    public KeyCode replayKey = KeyCode.R;
    private bool tutorialWatched = false;
    private bool allowReplay = true;

    private bool taskStarted = false;
    private bool taskCompleted = false;

    void Start()
    {
        foreach (var p in placements)
            if (p) p.enabled = false;

        // Auto-detect ToolPlacement if none are manually assigned
        if (placements == null || placements.Length == 0)
            placements = FindObjectsOfType<ToolPlacement>();

        if (hoverCanvas) hoverCanvas.SetActive(false);
        if (vidCanvas) vidCanvas.SetActive(false);
    }

    void Update()
    {
        if (!taskStarted)
            ShowHoverText();
        else if (hoverCanvas.activeSelf)
            hoverCanvas.SetActive(false);

        // Detect [E] to start
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
        {
            if (hit.collider.gameObject == gameObject && Input.GetKeyDown(interactKey))
            {
                if (!taskStarted)
                    StartCoroutine(TaskSequence());

                taskBG.Play();
            }
        }

        // Check completion status
        if (taskStarted && !taskCompleted)
        {
            bool allPlaced = true;

            foreach (var p in placements)
            {
                if (p != null && !p.IsPlaced())
                {
                    allPlaced = false;
                    break;
                }
            }

            if (allPlaced)
            {
                taskCompleted = true;
                StartCoroutine(OnTaskCompleted());
            }
        }

        // Replay video when R is pressed
        if (tutorialWatched && allowReplay && Input.GetKeyDown(replayKey))
        {
            ReplayTutorialVideo();
        }
    }

    IEnumerator TaskSequence()
    {
        taskStarted = true;
        if (hoverCanvas) hoverCanvas.SetActive(false);

        yield return StartCoroutine(PlayMotorTutorial());

        yield return StartCoroutine(SubtitleGuide());
    }

    IEnumerator PlayMotorTutorial()
    {
        if (vidCanvas == null || vidTotur == null)
            yield break;

        vidCanvas.SetActive(true);
        vidTotur.Play();

        // Wait until video finishes
        while (vidTotur.isPlaying || vidTotur.frame < (long)vidTotur.frameCount - 1)
        {
            yield return null;
        }

        vidCanvas.SetActive(false);

        tutorialWatched = true;
    }

    IEnumerator SubtitleGuide()
    {
        textPressR.SetActive(true);

        subtitleText.text = "For your last task, you're going to build a working 2 LED Light.";
        yield return new WaitForSeconds(7f);

        subtitleText.text = "First, gather the Arduino tools you'll need.";
        yield return new WaitForSeconds(5f);

        subtitleText.text = "Tools required:";
        yield return new WaitForSeconds(2f);

        subtitleText.text = "Tool need: \n1pc Arduino Uno";
        Arduino.SetActive(true);
        yield return new WaitForSeconds(1.5f);

        subtitleText.text = "Tool need: \n1pc Arduino Uno\n1pc Breadboard";
        Breadboard.SetActive(true);
        yield return new WaitForSeconds(1.5f);

        subtitleText.text = "Tool need: \n1pc Arduino Uno\n1pc Breadboard\n2pc LED";
        led.SetActive(true);
        led2.SetActive(true);
        yield return new WaitForSeconds(1.5f);

        subtitleText.text = "Tool need: \n1pc Arduino Uno\n1pc Breadboard\n2pc LED\n2pc Resistor";
        Resistor.SetActive(true);
        Resistor2.SetActive(true);
        yield return new WaitForSeconds(1.5f);

        subtitleText.text = "Tool need: \n1pc Arduino Uno\n1pc Breadboard\n2pc LED\n2pc Resistor\n3pc Red Wire";
        Wire3.SetActive(true);
        Wire4.SetActive(true);
        Wire5.SetActive(true);
        yield return new WaitForSeconds(1.5f);

        subtitleText.text = "Tool need: \n1pc Arduino Uno\n1pc Breadboard\n2pc LED\n2pc Resistor\n3pc Red Wire\n2pc Black Wire";
        Wire.SetActive(true);
        Wire2.SetActive(true);
        yield return new WaitForSeconds(1.5f);

        subtitleText.text = "Tool need:  \n1pc Arduino Uno\n1pc Breadboard\n2pc LED\n2pc Resistor\n3pc Red Wire\n2pc Black Wire\n\nNow, collect each tool and place them on the work table in front of you.";
        yield return new WaitForSeconds(1.5f);

        foreach (var p in placements)
            if (p) p.enabled = true;

        foreach (var p in tools)
            if (p) p.enabled = true;

        foreach (var p in disabledTools)
            if (p) p.enabled = false;
    }

    IEnumerator OnTaskCompleted()
    {
        allowReplay = false;
        textPressR.SetActive(false);

        subtitleText.text = "";
        yield return new WaitForSeconds(1f);

        subtitleText.text = "Good job! You’ve successfully placed all the components.";
        yield return new WaitForSeconds(3f);

        subtitleText.text = "The 2 LED build is now ready for connection!";
        yield return new WaitForSeconds(3f);

        subtitleText.text = "";

        foreach (var p in tools)
            if (p) p.enabled = false;

        twoLEDBuild.SetActive(true);
    }

    void ShowHoverText()
    {
        if (hoverCanvas) hoverCanvas.SetActive(false);

        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
        {
            if (hit.collider.gameObject == gameObject)
            {
                if (hoverCanvas) hoverCanvas.SetActive(true);
                if (hoverText) hoverText.text = defaultHoverMessage;
            }
        }
    }

    void ReplayTutorialVideo()
    {
        if (vidTotur == null || vidCanvas == null)
            return;

        vidCanvas.SetActive(true);
        vidTotur.Stop();
        vidTotur.Play();
    }
}