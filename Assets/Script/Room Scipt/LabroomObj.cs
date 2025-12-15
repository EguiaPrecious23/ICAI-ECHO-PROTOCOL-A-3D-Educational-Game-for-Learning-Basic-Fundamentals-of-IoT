using System.Collections;
using TMPro;
using UnityEngine;

public class LabroomObj : MonoBehaviour
{
    [Header("Objective")]
    public GameObject objCanvas;
    public TMP_Text fObjText;
    public TMP_Text objText;

    [Header("Waypoint")]
    public GameObject waypoint5;

    [Header("Tools")]
    public Collider[] tools;

    [Header("Obj Outline")]
    public GameObject foutline;
    public GameObject foutline2;
    public GameObject foutline3;
    public GameObject foutline4;
    public GameObject foutline5;

    private bool hasPassed = false;
    private bool rewardGiven = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!hasPassed)
            {
                foreach (var p in tools)
                    if (p) p.enabled = false;

                hasPassed = true;
                StartCoroutine(HandleObjective());

                foutline.SetActive(false);
                foutline2.SetActive(false);
                foutline3.SetActive(false);
                foutline4.SetActive(false);
                foutline5.SetActive(false);

                waypoint5.SetActive(false);
            }
        }
    }

    private IEnumerator HandleObjective()
    {
        objCanvas.SetActive(true);
        fObjText.text = "Last Objective:";
        objText.text = "Finish all the practical activity";

        if (!rewardGiven)
        {
            rewardGiven = true;
            Inventory.instance.AddMoney(500);
        }

        yield return new WaitForSeconds(5f);

        objCanvas.SetActive(false);
        fObjText.text = "";
        objText.text = "";
    }
}