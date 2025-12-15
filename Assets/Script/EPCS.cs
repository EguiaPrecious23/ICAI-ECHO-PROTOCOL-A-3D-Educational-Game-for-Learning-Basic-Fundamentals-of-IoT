using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class EPCS : MonoBehaviour
{
    [Header("Cutscene Video")]
    public VideoPlayer videoPlayer;

    public int Gameplay;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += OnVideoFinished;
        }
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        SceneManager.LoadScene(Gameplay);
    }
}