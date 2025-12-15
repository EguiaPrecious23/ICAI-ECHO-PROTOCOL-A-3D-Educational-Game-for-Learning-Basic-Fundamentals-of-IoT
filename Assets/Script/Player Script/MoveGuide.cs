using System.Collections;
using TMPro;
using UnityEngine;

public class MoveGuide : MonoBehaviour
{
    [Header("Subtitle Text")]
    public TMP_Text subtitleText;
    public GameObject TextBG;

    [Header("Resume")]
    public ResumeESC esc;

    [Header("Welcome Audio")]
    public GameObject welcomePlayer;

    private bool hasPlayed = false;

    private void Start()
    {
        welcomePlayer.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasPlayed && other.CompareTag("Player"))
        {
            hasPlayed = true;
            StartCoroutine(MovementGuide());
        }
    }

    private IEnumerator MovementGuide()
    {
        TextBG.SetActive(true);

        if (subtitleText != null)
        {
            subtitleText.gameObject.SetActive(true);
            yield return StartCoroutine(SubtitleRoutine());
        }

        subtitleText.gameObject.SetActive(false);
        TextBG.SetActive(false);

        welcomePlayer.SetActive(true);
    }

    private IEnumerator SubtitleRoutine()
    {
        esc.enabled = false;

        subtitleText.text = "W, A, S, D for Movement";
        yield return new WaitForSeconds(2f);

        subtitleText.text = "Press F to toggle Flashlight";
        yield return new WaitForSeconds(2f);

        subtitleText.text = "";
    }
}