using UnityEngine;
using TMPro;

public class ToolPlacement : MonoBehaviour
{
    [Header("Tool ID Match")]
    public string[] requiredToolID;

    [Header("Linked Objects")]
    public GameObject tableTool;
    public GameObject whiteTableTool;
    public GameObject heldTool;

    [Header("Interaction")]
    public Camera playerCamera;
    public float interactDistance = 3f;
    public KeyCode interactKey = KeyCode.E;

    [Header("Hover UI")]
    public GameObject hoverCanvas;
    public TextMeshProUGUI hoverText;
    public string hoverMessage = "";

    private bool isPlaced = false;

    void Update()
    {
        if (isPlaced || !PickTool.isHoldingItem)
        {
            if (hoverCanvas) hoverCanvas.SetActive(false);
            return;
        }

        if (hoverCanvas) hoverCanvas.SetActive(false);

        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
        {
            if (hit.collider.gameObject == gameObject)
            {
                if (IsValidTool(PickTool.currentHeldID))
                {
                    if (hoverCanvas) hoverCanvas.SetActive(true);
                    if (hoverText) hoverText.text = hoverMessage;

                    if (Input.GetKeyDown(interactKey))
                        PlaceTool();
                }
            }
        }
    }

    bool IsValidTool(string heldID)
    {
        foreach (string id in requiredToolID)
        {
            if (id == heldID)
                return true;
        }
        return false;
    }

    void PlaceTool()
    {
        if (isPlaced) return;
        if (tableTool) tableTool.SetActive(true);
        if (whiteTableTool) whiteTableTool.SetActive(false);
        if (heldTool) heldTool.SetActive(false);

        PickTool.ReleaseHeldItem();
        isPlaced = true;

        if (hoverCanvas) hoverCanvas.SetActive(false);
    }

    public bool IsPlaced()
    {
        return isPlaced;
    }
}