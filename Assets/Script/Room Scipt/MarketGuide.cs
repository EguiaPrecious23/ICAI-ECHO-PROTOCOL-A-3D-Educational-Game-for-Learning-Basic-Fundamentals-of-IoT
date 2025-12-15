using TMPro;
using UnityEngine;

public class MarketGuide : MonoBehaviour
{
    [Header("Objective")]
    public GameObject objCanvas;
    public TMP_Text fObjText;
    public TMP_Text objText;

    [Header("Waypoint")]
    public GameObject waypoint4;

    [Header("Obj Outline")]
    public GameObject foutline;
    public GameObject foutline2;
    public GameObject foutline3;
    public GameObject foutline4;
    public GameObject foutline5;

    [Header("Ojb Outline")]
    public GameObject soutline;
    public GameObject soutline2;
    public GameObject soutline3;
    public GameObject soutline4;
    public GameObject soutline5;

    private bool rewardGiven = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            objCanvas.SetActive(true);
            fObjText.text = "New Objective:";
            objText.text = "Buy your first Arduino tools";

            waypoint4.SetActive(false);

            foutline.SetActive(false);
            foutline2.SetActive(false);
            foutline3.SetActive(false);
            foutline4.SetActive(false);
            foutline5.SetActive(false);

            soutline.SetActive(true);
            soutline2.SetActive(true);
            soutline3.SetActive(true);
            soutline4.SetActive(true);
            soutline5.SetActive(true);

            if (!rewardGiven)
            {
                rewardGiven = true;
                Inventory.instance.AddMoney(500);
            }
        }
    }
}
