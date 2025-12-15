using TMPro;
using UnityEngine;

public class CAGuideReplay : MonoBehaviour
{
    [Header("Subtitle Text")]
    public TMP_Text subtitleText;
    public GameObject textBG;

    [Header("Keybind")]
    public KeyCode replayKey = KeyCode.R;

    [Header("Central Area Guide")]
    public CAGuide caGuide;

    private bool playerInside = false;

    private void OnEnable()
    {
        playerInside = false;
        subtitleText.text = "";
        subtitleText.gameObject.SetActive(false);
        textBG.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
            subtitleText.text = "Press 'R' to replay the guide.";
            subtitleText.gameObject.SetActive(true);
            textBG.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            subtitleText.text = "";
            subtitleText.gameObject.SetActive(false);
            textBG.SetActive(false);
        }
    }

    private void Update()
    {
        if (playerInside && Input.GetKeyDown(replayKey))
        {
            subtitleText.text = "";
            subtitleText.gameObject.SetActive(false);
            textBG.SetActive(false);

            this.gameObject.SetActive(false);
            caGuide.ReplayGuide();
        }
    }
}