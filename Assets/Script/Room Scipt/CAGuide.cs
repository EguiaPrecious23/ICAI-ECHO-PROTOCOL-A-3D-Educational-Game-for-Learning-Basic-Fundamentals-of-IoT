using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CAGuide : MonoBehaviour
{
    [Header ("Central Area Guide")]
    public AudioSource caGuide;

    [Header ("Subtitle Text")]
    public TMP_Text subtitleText;
    public GameObject TextBG;

    [Header("Alien Hologram")]
    public GameObject Holo;

    [Header("Waypoint")]
    public GameObject waypoint2;
    public GameObject waypoint3;

    [Header("Objective")]
    public GameObject objCanvas;
    public TMP_Text fObjText;
    public TMP_Text objText;

    [Header("Stamina")]
    public GameObject Stamina;
    public Running running;

    [Header("Inventory")]
    public GameObject inventory;
    public Canvas inventoryUI;

    [Header("Resume")]
    public ResumeESC esc;

    [Header("Controls Keybinds")]
    public GameObject controls;
    public GameObject wasd;
    public GameObject mouse;
    public GameObject leftShift;
    public GameObject e;
    public GameObject f;
    public GameObject escBut;

    [Header("Door Locked")]
    public GameObject MRDoor;
    public GameObject SRDoor;

    [Header("Replay")]
    public GameObject replayTrigger;

    [Header("Obj Outline")]
    public GameObject foutline;
    public GameObject foutline2;
    public GameObject foutline3;

    [Header("Ojb Outline")]
    public GameObject soutline;
    public GameObject soutline2;

    private bool hasPlayed = false;
    private bool isOpen = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasPlayed && other.CompareTag("Player"))
        {
            hasPlayed = true;
            StartCoroutine(CAguide());

            foutline.SetActive(false);
            foutline2.SetActive(false);
            foutline3.SetActive(false);
        }
    }

    IEnumerator CAguide()
    {

        TextBG.SetActive(true);
        Holo.SetActive(true);
        objCanvas.SetActive(false);
        waypoint2.SetActive(false);
        MRDoor.SetActive(false);
        SRDoor.SetActive(false);
        esc.enabled = false;

        yield return new WaitForSeconds(2f);

        caGuide.Play();

        if (subtitleText != null)
        {
            subtitleText.gameObject.SetActive(true);
            StartCoroutine(SubtitleCAG());
        }

        yield return new WaitWhile(() => caGuide.isPlaying);

        subtitleText.gameObject.SetActive(false);
        TextBG.SetActive(false);

        MRDoor.SetActive(true);
        SRDoor.SetActive(true);

        objCanvas.SetActive(true);
        fObjText.text = "Second Objective:";
        objText.text = "Proceed to Smartroom to learn IoT on Arduino tools";

        waypoint3.SetActive(true);
        soutline.SetActive(true);
        soutline2.SetActive(true);

        replayTrigger.SetActive(true);
        this.enabled = false;

    }

    IEnumerator SubtitleCAG()
    {
        isOpen = !isOpen;

        subtitleText.text = "";
        yield return new WaitForSeconds(0.5f);

        subtitleText.text = "Every task requires energy. ";
        yield return new WaitForSeconds(3f);

        subtitleText.text = "Every successful mission earns you credits — used to unlock tools, parts, and access.";
        yield return new WaitForSeconds(7.5f);

        subtitleText.text = "Now... before you proceed, learn how to move within this system.";
        yield return new WaitForSeconds(5.5f);

        controls.SetActive(true);

        subtitleText.text = "Use W,A,S & D key to walk.";

        wasd.SetActive(true);

        yield return new WaitForSeconds(6f);

        subtitleText.text = "Look around using your mouse.";

        mouse.SetActive(true);

        yield return new WaitForSeconds(3f);

        subtitleText.text = "Hold Left-Shift to run. Take note that your stamina will drain, and generate when you stop.";

        if (Stamina != null)
            Stamina.SetActive(true);

        if (running != null)
            running.enabled = true;

        leftShift.SetActive(true);

        yield return new WaitForSeconds(7.5f);

        subtitleText.text = "Press E to interact with terminals, switches, and objects.";

        e.SetActive(true);

        yield return new WaitForSeconds(5.5f);

        subtitleText.text = "Use F key to toggle your flashlight in darker zones.";

        f.SetActive(true);

        yield return new WaitForSeconds(5f);

        subtitleText.text = "Press Escape to show Resume, Options, Quit.";

        escBut.SetActive(true);

        yield return new WaitForSeconds(3.5f);

        wasd.SetActive(false);
        mouse.SetActive(false);
        leftShift.SetActive(false);
        e.SetActive(false);
        f.SetActive(false);
        escBut.SetActive(false);
        inventory.SetActive(true);

        subtitleText.text = "Press Tab to open your digital inventory. All your collected tools will be stored here.";
        yield return new WaitForSeconds(8.2f);

        subtitleText.text = "Your Credits are earned through performance, use it to purchase items.";
        yield return new WaitForSeconds(5.6f);

        subtitleText.text = "That concludes your initial orientation.";
        yield return new WaitForSeconds(4f);

        subtitleText.text = "Now... Let's begin.";
        yield return new WaitForSeconds(4f);

        subtitleText.text = "";

        esc.enabled = true;
    }

    public void ReplayGuide()
    {
        StopAllCoroutines();
        this.enabled = true;
        StartCoroutine(CAguide());
    }
}