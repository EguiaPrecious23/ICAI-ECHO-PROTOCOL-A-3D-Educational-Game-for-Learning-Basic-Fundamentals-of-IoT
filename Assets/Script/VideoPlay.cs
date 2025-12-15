using TMPro;
using UnityEngine;
using UnityEngine.Video;

public class VideoPlay : MonoBehaviour
{
    [Header("LED Video")]
    public VideoPlayer videoPlayer;

    [Header("Raycasy Camera")]
    public Camera playerCamera;
    public float interactDistance = 3f;
    public KeyCode interactKey = KeyCode.E;

    [Header("Hover Text")]
    public GameObject hoverCanvas;
    public TextMeshProUGUI hoverText;
    public string defaultHoverMessage = "";

    void Update()
    {
        if (videoPlayer != null && videoPlayer.isPlaying)
        {
            if (hoverCanvas != null) hoverCanvas.SetActive(false);
            return;
        }

        ShowHoverText();

        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
        {
            if (hit.collider.gameObject == gameObject)
            {
                if (Input.GetKeyDown(interactKey))
                {
                    if (videoPlayer != null)
                    {
                        if (!videoPlayer.isPlaying)
                        {
                            videoPlayer.Play();
                        }
                    }
                }
            }
        }
    }

    void ShowHoverText()
    {
        if (hoverCanvas != null) hoverCanvas.SetActive(false);

        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
        {
            if (hit.collider.gameObject == gameObject)
            {
                if (hoverCanvas != null) hoverCanvas.SetActive(true);
                if (hoverText != null) hoverText.text = defaultHoverMessage;
            }
        }
    }
}