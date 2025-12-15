using TMPro;
using UnityEngine;
using UnityEngine.LowLevel;

public class MarketInteraction : MonoBehaviour
{
    [Header("Market UI + Cameras")]
    public GameObject marketUI;
    public GameObject marketCam;
    public GameObject playerCam;

    [Header("Waypoint")]
    public GameObject waypoint5;

    [Header("Raycasy Camera")]
    public Camera playerCamera;
    public float interactDistance = 3f;
    public KeyCode interactKey = KeyCode.E;

    [Header("Hover Text")]
    public GameObject hoverCanvas;
    public TextMeshProUGUI hoverText;
    public string defaultHoverMessage = "";

    [Header("Objective")]
    public GameObject objCanvas;
    public TMP_Text fObjText;
    public TMP_Text objText;

    [Header("Subtitle Text")]
    public TMP_Text subtitleText;
    public GameObject TextBG;

    [Header("LR Unlock")]
    public GameObject marketGuide;

    [Header("LR Unlock")]
    public GameObject Door;

    [Header("Market Scripts")]
    public MarketBuyBack marketBuyBack;

    private Movement playerLookMoveScript;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerLookMoveScript = player.GetComponent<Movement>();
        }

        objCanvas.SetActive(false);

        if (marketBuyBack != null)
            marketBuyBack.ClearUI();
    }

    void Update()
    {
        ShowHoverText();

        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
        {
            if (hit.collider.gameObject == gameObject)
            {
                if (Input.GetKeyDown(interactKey))
                {
                    Interact();
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

    public void Interact()
    {
        if (marketUI != null) marketUI.SetActive(true);
        if (marketCam != null) marketCam.SetActive(true);
        if (playerCam != null) playerCam.SetActive(false);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (playerLookMoveScript != null && playerLookMoveScript.walkingSource != null)
            playerLookMoveScript.walkingSource.enabled = false;

        if (objCanvas != null) objCanvas.SetActive(true);
        if (fObjText != null) fObjText.text = "Fourth Objective";
        if (objText != null) objText.text = "Proceed to the Lab room for practical challenges";
        waypoint5.SetActive(true);

        TextBG.SetActive(false);
        subtitleText.enabled = false;
        
        marketGuide.SetActive(false);
        Door.SetActive(true);
    }

    public void CloseMarket()
    {
        if (marketUI != null) marketUI.SetActive(false);
        if (marketCam != null) marketCam.SetActive(false);
        if (playerCam != null) playerCam.SetActive(true);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (playerLookMoveScript != null && playerLookMoveScript.walkingSource != null)
            playerLookMoveScript.walkingSource.enabled = true;

        if (marketBuyBack != null)
            marketBuyBack.ClearUI();

        TextBG.SetActive(false);
        subtitleText.enabled = false;
    }
}
