using System.Collections;
using UnityEngine;

public class ScreenFade : MonoBehaviour
{
    [Header("Blackscreen Fade")]
    public Canvas fadeCanvas;
    public CanvasGroup fadeCanvasGroup;

    [Header("Duration")]
    public float fadeDuration = 5f;
    public float fadeInDuration = 2f;

    void Start()
    {
        if (fadeCanvas != null)
        {
            fadeCanvas.gameObject.SetActive(false);
            StartCoroutine(FadeOutAtStart());
        }
    }

    public IEnumerator FadeOutAtStart()
    {
        fadeCanvas.gameObject.SetActive(true);
        fadeCanvasGroup.alpha = 1f;

        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            fadeCanvasGroup.alpha = Mathf.Lerp(1f, 0f, t / fadeDuration);
            yield return null;
        }

        fadeCanvasGroup.alpha = 0f;

        fadeCanvas.gameObject.SetActive(false);
    }
}