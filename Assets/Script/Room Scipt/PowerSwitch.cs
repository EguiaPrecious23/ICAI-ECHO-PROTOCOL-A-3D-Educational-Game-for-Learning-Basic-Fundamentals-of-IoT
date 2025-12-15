using System.Collections;
using TMPro;
using UnityEngine;

public class PowerSwitch : MonoBehaviour
{
    [Header("Central Area Lights & Bulb")]
    public GameObject CALights;
    public GameObject[] CACeilingBulb;

    [Header("Room's Light")]
    public GameObject SRHWLights;
    public GameObject SRLights;
    public GameObject LRHWLights;
    public GameObject LRLights;
    public GameObject TDHWLight;
    public GameObject[] TDCeilingBulb;
    public GameObject RRHWLights;
    public GameObject ReactorLights;

    [Header("Switch")]
    public Transform switchHandle;
    public Vector3 onRotationEuler = new Vector3(0, 0f, 0);
    public Vector3 offRotationEuler = new Vector3(0, 0f, 0);
    public float rotationDuration = 0.4f;

    [Header("Raycasy Camera")]
    public Camera playerCamera;
    public float interactDistance = 3f;
    public KeyCode interactKey = KeyCode.E;

    [Header("Hover Text")]
    public GameObject hoverCanvas;
    public TextMeshProUGUI hoverText;
    public string defaultHoverMessage = "";

    [Header("Waypoint")]
    public GameObject waypoint;
    public GameObject waypoint2;

    [Header("Objective")]
    public GameObject objCanvas;
    public TMP_Text fObjText;
    public TMP_Text objText;

    [Header("Central Area Guide")]
    public GameObject caguide;

    [Header("1st Objective")]
    public GameObject firstObj;

    [Header("First Obj Outline")]
    public GameObject foutline;
    public GameObject foutline2;
    public GameObject foutline3;
    public GameObject foutline4;

    [Header("Second Ojb Outline")]
    public GameObject soutline;
    public GameObject soutline2;
    public GameObject soutline3;

    private bool isPowerOn = false;
    private bool isAnimating = false;
    private bool rewardGiven = false;

    void Update()
    {

        ShowHoverText();

        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
        {
            if (hit.collider.gameObject == gameObject)
            {
                if (Input.GetKeyDown(interactKey) && !isAnimating)
                {
                    StartCoroutine(TogglePower());
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

    IEnumerator TogglePower()
    {
        isAnimating = true;
        isPowerOn = !isPowerOn;
        foutline.SetActive(false);
        foutline2.SetActive(false);
        foutline3.SetActive(false);
        foutline4.SetActive(false);

        soutline.SetActive(true);
        soutline2.SetActive(true);
        soutline3.SetActive(true);

        Quaternion startRotation = switchHandle.localRotation;
        Quaternion endRotation = Quaternion.Euler(isPowerOn ? onRotationEuler : offRotationEuler);

        float t = 0f;
        while (t < rotationDuration)
        {
            t += Time.deltaTime;
            switchHandle.localRotation = Quaternion.Slerp(startRotation, endRotation, t / rotationDuration);
            yield return null;
        }
        switchHandle.localRotation = endRotation;

        CALights.SetActive(isPowerOn);
        foreach (GameObject bulb in CACeilingBulb)
        {
            if (bulb != null)
                bulb.SetActive(isPowerOn);
        }

        SRHWLights.SetActive(isPowerOn);

        SRLights.SetActive(isPowerOn);
        LRHWLights.SetActive(isPowerOn);
        TDHWLight.SetActive(isPowerOn);
        foreach (GameObject bulb in TDCeilingBulb)
        {
            if (bulb != null)
                bulb.SetActive(isPowerOn);
        }
        LRLights.SetActive(isPowerOn);
        RRHWLights.SetActive(isPowerOn);
        ReactorLights.SetActive(isPowerOn);

        isAnimating = false;

        firstObj.SetActive(false);
        waypoint.SetActive(false);
        objCanvas.SetActive(true);
        fObjText.text = "New Objective:";
        objText.text = "Find the Projector Table at Central Area";
        caguide.SetActive(true);
        waypoint2.SetActive(true);

        if (isPowerOn && !rewardGiven)
        {
            rewardGiven = true;
            Inventory.instance.AddMoney(500);
        }
    }
}