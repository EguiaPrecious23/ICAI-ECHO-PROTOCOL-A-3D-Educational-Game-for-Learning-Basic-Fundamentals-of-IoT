using UnityEngine;
using TMPro;

public class PickTool : MonoBehaviour, IInteractable
{
    [Header("Tool ID")]
    public string toolID;

    [Header("Raycast Camera")]
    public Camera playerCamera;
    public float interactDistance = 3f;
    public KeyCode interactKey = KeyCode.E;
    public KeyCode dropKey = KeyCode.Q;

    [Header("Hidden Tool To Reveal")]
    public GameObject hiddenTool;

    [Header("Layer Mask")]
    public LayerMask layer;

    [Header("Drop Zone (Where Item Returns When Dropped)")]
    public Transform dropZone;

    public GameObject textQ;

    [Header("Hover Text")]
    public GameObject hoverCanvas;
    public TextMeshProUGUI hoverText;
    public string defaultHoverMessage = "";

    private bool picked = false;
    private bool isHovering = false;

    public static bool isHoldingItem = false;
    public static string currentHeldID = "";

    private static GameObject currentlyHeldObject;
    private static GameObject currentlyHiddenTool;
    private static Transform currentDropZone;

    void Start()
    {
        // Disable drop zone initially (if exists)
        if (dropZone != null)
            dropZone.gameObject.SetActive(false);
    }

    void Update()
    {
        HandleInteraction();

        // Handle dropping when pressing Q
        if (isHoldingItem && Input.GetKeyDown(dropKey))
            DropToZone();
    }

    private void HandleInteraction()
    {
        // Don't allow interaction while holding another item
        if (isHoldingItem)
        {
            HideHover();
            return;
        }

        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance, layer))
        {
            if (hit.collider.gameObject == gameObject)
            {
                // Show hover text once when aiming at this tool
                if (!isHovering)
                {
                    isHovering = true;
                    hoverCanvas?.SetActive(true);
                    if (hoverText) hoverText.text = defaultHoverMessage;
                }

                // Interaction
                if (Input.GetKeyDown(interactKey) && !isHoldingItem)
                    Interact();
            }
            else if (isHovering)
            {
                HideHover();
            }
        }
        else if (isHovering)
        {
            HideHover();
        }
    }

    public void Interact()
    {
        picked = !picked;
        isHoldingItem = true;
        currentHeldID = toolID;

        if (hiddenTool) hiddenTool.SetActive(true);
        if (dropZone != null) dropZone.gameObject.SetActive(true);

        currentlyHeldObject = gameObject;
        currentlyHiddenTool = hiddenTool;
        currentDropZone = dropZone;

        HideHover();
        gameObject.SetActive(false);

        if (textQ != null)
            textQ.SetActive(true);
    }

    private void HideHover()
    {
        isHovering = false;
        if (hoverCanvas && hoverCanvas.activeSelf)
            hoverCanvas.SetActive(false);
    }

    public static void ReleaseHeldItem()
    {
        isHoldingItem = false;
        currentHeldID = "";
    }

    private void DropToZone()
    {
        if (!isHoldingItem || currentlyHeldObject == null)
            return;

        PickTool tool = currentlyHeldObject.GetComponent<PickTool>();
        if (tool == null || currentDropZone == null)
        {
            Debug.LogWarning("No Drop Zone assigned for this tool!");
            return;
        }

        // Reposition the original tool back to its assigned drop zone
        currentlyHeldObject.transform.position = currentDropZone.position;
        currentlyHeldObject.transform.rotation = currentDropZone.rotation;
        currentlyHeldObject.SetActive(true);

        // Hide the held (revealed) version
        if (currentlyHiddenTool != null)
            currentlyHiddenTool.SetActive(false);

        // Disable drop zone again after dropping
        currentDropZone.gameObject.SetActive(false); // ✅ Disable after drop

        // Reset states
        ReleaseHeldItem();
        currentlyHeldObject = null;
        currentlyHiddenTool = null;
        currentDropZone = null;

        if (textQ != null)
            textQ.SetActive(false);
    }
}