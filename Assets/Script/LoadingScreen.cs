using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadingScreen : MonoBehaviour
{
    [Header("Loading Text")]
    public TMP_Text progressText;

    [Header("Loading Dots")]
    public TMP_Text loadingDots;

    [Header("Scene")]
    public int sceneToLoad;

    [Header("Dot Animation")]
    public float dotInterval = 0.3f;

    [Header("Speed Loading")]
    public float fillSpeed = 0.1f;

    private void Start()
    {
        StartCoroutine(LoadSceneAsync());
        StartCoroutine(AnimateDots());
    }

    IEnumerator LoadSceneAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);
        operation.allowSceneActivation = false;

        float visualProgress = 0f;

        while (!operation.isDone)
        {
            float targetProgress = Mathf.Clamp01(operation.progress / 0.9f);

            visualProgress = Mathf.MoveTowards(visualProgress, targetProgress, Time.deltaTime * fillSpeed);

            if (progressText != null)
                progressText.text = $"{Mathf.RoundToInt(visualProgress * 100f)}%";

            if (visualProgress >= 1f && targetProgress >= 1f)
            {
                yield return new WaitForSeconds(0.5f);
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }

    IEnumerator AnimateDots()
    {
        string baseText = "";
        int dotCount = 0;

        while (true)
        {
            dotCount = (dotCount + 1) % 4;
            if (loadingDots != null)
                loadingDots.text = baseText + new string('.', dotCount);

            yield return new WaitForSeconds(dotInterval);
        }
    }
}