using System.Collections;
using TMPro;
using UnityEngine;

public class WelcomePlayer : MonoBehaviour
{
    [Header("Welcome Audio")]
    public AudioSource welcomeAudio;

    [Header("Subtitle Text")]
    public TMP_Text subtitleText;
    public GameObject TextBG;

    [Header("Player Input")]
    public PlayerNameInput Username;

    [Header("Sliding Door")]
    public GameObject SlidingDoor;

    [Header("Resume")]
    public ResumeESC esc;

    private bool hasPlayed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasPlayed && other.CompareTag("Player"))
        {
            hasPlayed = true;
            StartCoroutine(PlayWelcomeAndEnableDoor());
        }
    }

    private IEnumerator PlayWelcomeAndEnableDoor()
    {
        TextBG.SetActive(true);
        SlidingDoor.SetActive(false);

        yield return new WaitForSeconds(2f);

        welcomeAudio.Play();

        if (subtitleText != null)
        {
            subtitleText.gameObject.SetActive(true);
            StartCoroutine(SubtitleRoutine());
        }

        yield return new WaitWhile(() => welcomeAudio.isPlaying);

        subtitleText.gameObject.SetActive(false);
        TextBG.SetActive(false);
        SlidingDoor.SetActive(true);
    }

    IEnumerator SubtitleRoutine()
    {
        esc.enabled = false;

        subtitleText.text = "";
        yield return new WaitForSeconds(1f);

        subtitleText.text = "Welcome " + PlayerNameInput.PlayerName;
        yield return new WaitForSeconds(2.5f);

        subtitleText.text = "You have entered ICAI: Echo Protocol.";
        yield return new WaitForSeconds(4.2f);

        subtitleText.text = "This is not a workplace.";
        yield return new WaitForSeconds(2f);

        subtitleText.text = "This is not a school.";
        yield return new WaitForSeconds(2.75f);

        subtitleText.text = "It is a proving ground.";
        yield return new WaitForSeconds(2f);

        subtitleText.text = "Where only the curious adapt.";
        yield return new WaitForSeconds(2.5f);

        subtitleText.text = "Your journey begins now.";
        yield return new WaitForSeconds(2.5f);

        subtitleText.text = "Prepare for protocol initiation.";
        yield return new WaitForSeconds(3f);

        subtitleText.text = "Walk straight to the door.";
        yield return new WaitForSeconds(2.1f);

        subtitleText.text = "Once you entered you can never gonna get out.";
        yield return new WaitForSeconds(4.2f);

        subtitleText.text = "Unless...";
        yield return new WaitForSeconds(1.6f);

        subtitleText.text = "Goodluck!.";
        yield return new WaitForSeconds(1.6f);

        subtitleText.text = "";
    }
}
