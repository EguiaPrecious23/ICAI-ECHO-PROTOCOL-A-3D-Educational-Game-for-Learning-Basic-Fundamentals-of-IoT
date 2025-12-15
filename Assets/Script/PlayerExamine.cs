using TMPro;
using UnityEngine;

public class PlayerExamine : MonoBehaviour
{
    public Camera playerCamera;
    public float examineDistance = 3f;
    public KeyCode examineKey = KeyCode.E;
    public float rotationSpeed = 5f;

    public GameObject hoverCanvas;
    public TextMeshProUGUI hoverText;
    public string defaultHoverMessage = "to";

    private ExaminableObject currentExaminedObject;
    private Movement playerMovement;

    void Start()
    {
        playerMovement = GetComponent<Movement>();
    }

    void Update()
    {
        ShowHoverText(); // check raycast every frame

        if (Input.GetKeyDown(examineKey))
        {
            if (currentExaminedObject == null)
            {
                TryExamine();
            }
            else
            {
                currentExaminedObject.StopExamine();
                currentExaminedObject = null;

                if (playerMovement != null)
                    playerMovement.allowLook = true;

                if (hoverCanvas != null)
                    hoverCanvas.SetActive(false);
            }
        }

        if (currentExaminedObject != null)
            RotateExaminedObject();
    }

    void TryExamine()
    {
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (Physics.Raycast(ray, out RaycastHit hit, examineDistance))
        {
            ExaminableObject obj = hit.collider.GetComponent<ExaminableObject>();
            if (obj != null)
            {
                obj.StartExamine(playerCamera.transform);
                currentExaminedObject = obj;

                if (playerMovement != null)
                    playerMovement.allowLook = false;
            }
        }
    }

    void RotateExaminedObject()
    {
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

        currentExaminedObject.transform.Rotate(Vector3.up, -mouseX, Space.World);
        currentExaminedObject.transform.Rotate(playerCamera.transform.right, mouseY, Space.World);
    }

    void ShowHoverText()
    {
        if (currentExaminedObject != null)
        {
            if (hoverCanvas != null)
                hoverCanvas.SetActive(false);

            return;
        }

        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (Physics.Raycast(ray, out RaycastHit hit, examineDistance))
        {
            ExaminableObject obj = hit.collider.GetComponent<ExaminableObject>();
            if (obj != null)
            {
                if (hoverCanvas != null && !hoverCanvas.activeSelf)
                    hoverCanvas.SetActive(true);

                if (hoverText != null)
                    hoverText.text = defaultHoverMessage;
                return;
            }
        }

        if (hoverCanvas != null && hoverCanvas.activeSelf)
            hoverCanvas.SetActive(false);
    }
}