using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

public class LR1stTask : MonoBehaviour
{
    [Header("Subtitle UI")]
    public TMP_Text subtitleText;

    [Header("Tools")]
    public Collider[] tools;

    [Header("Tool Placement Spots")]
    public ToolPlacement[] placements;

    [Header("Gray Tools")]
    public GameObject Arduino;
    public GameObject Board;
    public GameObject Resistor;
    public GameObject LED;
    public GameObject Wire;
    public GameObject Wire2;

    [Header("Disabled Tools")]
    public Collider[] disabledTools;

    [Header("LED Vid Tutorial")]
    public GameObject vidCanvas;
    public VideoPlayer vidTotur;

    [Header("Raycast Camera")]
    public Camera playerCamera;
    public float interactDistance = 3f;
    public KeyCode interactKey = KeyCode.E;

    [Header("Hover Text")]
    public GameObject hoverCanvas;
    public TextMeshProUGUI hoverText;
    public string defaultHoverMessage = "to start the 1st task";

    [Header("Build LED")]
    public GameObject buildLED;

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

        yield return StartCoroutine(PlayLEDTutorial());

        yield return StartCoroutine(SubtitleGuide());
    }

    IEnumerator PlayLEDTutorial()
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

        subtitleText.text = "For your first task, you're going to build a working LED light.";
        yield return new WaitForSeconds(7f);

        subtitleText.text = "First, gather the Arduino tools you'll need.";
        yield return new WaitForSeconds(5f);

        subtitleText.text = "Tools required:";
        yield return new WaitForSeconds(2f);

        subtitleText.text = "Tool need: \n1pc Arduino Uno";
        Arduino.SetActive(true);
        yield return new WaitForSeconds(1.5f);

        subtitleText.text = "Tool need: \n1pc Arduino Uno \n1pc Breadboard";
        Board.SetActive(true);
        yield return new WaitForSeconds(1.5f);

        subtitleText.text = "Tool need: \n1pc Arduino Uno \n1pc Breadboard \n1pc Resistor";
        Resistor.SetActive(true);
        yield return new WaitForSeconds(1.5f);

        subtitleText.text = "Tool need: \n1pc Arduino Uno \n1pc Breadboard \n1pc Resistor \n1pc LED Light";
        LED.SetActive(true);
        yield return new WaitForSeconds(1.5f);

        subtitleText.text = "Tool need: \n1pc Arduino Uno \n1pc Breadboard \n1pc Resistor \n1pc LED Light \n1pc Red Jumper Wire";
        yield return new WaitForSeconds(1.5f);

        subtitleText.text = "Tool need: \n1pc Arduino Uno \n1pc Breadboard \n1pc Resistor \n1pc LED Light \n1pc Red Jumper Wire\n1pc Black Jumper Wire";
        yield return new WaitForSeconds(1.5f);

        subtitleText.text = "Tool need: \n1pc Arduino Uno \n1pc Breadboard \n1pc Resistor \n1pc LED Light \n1pc Red Jumper Wire\n1pc Black Jumper Wire\n\nNow, collect each tool and place them on the work table in front of you.";
        yield return new WaitForSeconds(1.5f);

        Wire.SetActive(true);
        Wire2.SetActive(true);

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

        subtitleText.text = "The LED circuit is now ready for connection!";
        yield return new WaitForSeconds(3f);

        subtitleText.text = "";

        foreach (var p in tools)
            if (p) p.enabled = false;

        buildLED.SetActive(true);
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