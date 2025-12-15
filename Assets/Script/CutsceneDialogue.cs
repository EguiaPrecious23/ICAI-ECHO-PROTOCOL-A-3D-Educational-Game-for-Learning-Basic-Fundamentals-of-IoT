using System.Collections;
using TMPro;
using UnityEngine;

public class CutsceneDialogue : MonoBehaviour
{
    [Header("Player Subtitle")]
    public TMP_Text playerText;
    public GameObject playerBG;

    [Header("Narrator 1")]
    public TMP_Text narrator1Text;
    public GameObject narrator1BG;

    [Header("Narrator 2")]
    public TMP_Text narrator2Text;
    public GameObject narrator2BG;

    [Header("Mysterious Voice")]
    public TMP_Text mysteriousText;
    public GameObject mysteriousBG;

    private void Start()
    {
        StartCoroutine(MovementGuide());
    }

    private IEnumerator MovementGuide()
    {
        if (playerText)
            yield return StartCoroutine(PlayerRoutine());
    }

    private IEnumerator PlayerRoutine()
    {
        yield return new WaitForSeconds(5f);

        if (narrator1Text) narrator1Text.gameObject.SetActive(true);
        if (narrator1BG) narrator1BG.SetActive(true);

        yield return new WaitForSeconds(12f);

        if (narrator1Text) narrator1Text.gameObject.SetActive(false);
        if (narrator1BG) narrator1BG.SetActive(false);

        if (playerText) playerText.gameObject.SetActive(true);
        if (playerBG) playerBG.SetActive(true);

        playerText.text = "Where… am I?";
        yield return new WaitForSeconds(2.5f);

        playerText.text = "I don’t remember how I got here.";
        yield return new WaitForSeconds(4f);

        playerText.text = "The last thing I recall…";
        yield return new WaitForSeconds(2.5f);

        playerText.text = "was following that strange sound.";
        yield return new WaitForSeconds(4f);

        if (playerText) playerText.gameObject.SetActive(false);
        if (playerBG) playerBG.SetActive(false);

        if (mysteriousText) mysteriousText.gameObject.SetActive(true);
        if (mysteriousBG) mysteriousBG.SetActive(true);

        mysteriousText.text = "Mysterious Voice: You should not have come here…";
        yield return new WaitForSeconds(5f);

        mysteriousText.text = "Mysterious Voice: yet destiny has chosen your steps.";
        yield return new WaitForSeconds(5f);

        if (mysteriousText) mysteriousText.gameObject.SetActive(false);
        if (mysteriousBG) mysteriousBG.SetActive(false);

        if (playerText) playerText.gameObject.SetActive(true);
        if (playerBG) playerBG.SetActive(true);

        playerText.text = "Who’s there? Show yourself!";
        yield return new WaitForSeconds(5f);

        if (playerText) playerText.gameObject.SetActive(false);
        if (playerBG) playerBG.SetActive(false);

        if (narrator2Text) narrator2Text.gameObject.SetActive(true);
        if (narrator2BG) narrator2BG.SetActive(true);

        yield return new WaitForSeconds(6.5f);

        if (narrator2Text) narrator2Text.gameObject.SetActive(false);
        if (narrator2BG) narrator2BG.SetActive(false);
    }
}