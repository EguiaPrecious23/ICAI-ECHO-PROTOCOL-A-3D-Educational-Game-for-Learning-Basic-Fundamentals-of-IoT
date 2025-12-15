using TMPro;
using UnityEngine;

public class FirstObjectivePowerSwitch : MonoBehaviour
{
    [Header("Waypoint")]
    public GameObject waypoint;

    [Header("Objective")]
    public GameObject objCanvas;
    public TMP_Text fObjText;
    public TMP_Text objText;

    [Header("Door Locked")]
    public GameObject Door1;
    public GameObject Door2;
    public GameObject Door3;

    [Header("Outline")]
    public GameObject outline;
    public GameObject outline2;
    public GameObject outline3;
    public GameObject outline4;

    private bool objectiveShown = false; // Simple flag to prevent re-showing

    private void Start()
    {
        // Initial setup: Hide waypoint and objective UI, set doors inactive
        if (waypoint != null) waypoint.SetActive(false);
        if (objCanvas != null) objCanvas.SetActive(false);

        if (Door1 != null) Door1.SetActive(false);
        if (Door2 != null) Door2.SetActive(false);
        if (Door3 != null) Door3.SetActive(false);

        // Hide outlines initially
        if (outline != null) outline.SetActive(false);
        if (outline2 != null) outline2.SetActive(false);
        if (outline3 != null) outline3.SetActive(false);
        if (outline4 != null) outline4.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !objectiveShown)
        {
            objectiveShown = true;

            // Show objective UI
            if (waypoint != null) waypoint.SetActive(true);
            if (objCanvas != null) objCanvas.SetActive(true);

            if (fObjText != null) fObjText.text = "First Objective:";
            if (objText != null) objText.text = "Turn on the Facility's power on the Reactor room";

            // Show outlines
            if (outline != null) outline.SetActive(true);
            if (outline2 != null) outline2.SetActive(true);
            if (outline3 != null) outline3.SetActive(true);
            if (outline4 != null) outline4.SetActive(true);

            Debug.Log("First objective UI shown.");
        }
    }
}