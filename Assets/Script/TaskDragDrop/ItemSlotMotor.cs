using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlotMotor : MonoBehaviour, IDropHandler
{
    [Header("Accepted Item IDs")]
    public string[] acceptedIDs;

    [Header("Reference to Correct Motor Form")]
    public GameObject ledCorrectForm;  // the final correct LED form

    [Header("Reference to Build Manager")]
    public MotorBuild motoBuild; // 👈 link to LEDBuild

    [Header("Feedback Text")]
    public TMP_Text subtitleText;
    public GameObject TextBG;
    public float feedbackDuration = 2f;

    private bool isFilled = false;

    private void ShowFeedback(string message, Color color)
    {
        if (subtitleText == null || TextBG == null) return;

        StopAllCoroutines();
        TextBG.SetActive(true);
        subtitleText.gameObject.SetActive(true);
        subtitleText.text = message;
        subtitleText.color = color;

        StartCoroutine(HideFeedbackAfterDelay());
    }

    private IEnumerator HideFeedbackAfterDelay()
    {
        yield return new WaitForSeconds(feedbackDuration);
        TextBG.SetActive(false);
        subtitleText.gameObject.SetActive(false);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (isFilled) return;

        DragDropMotor draggedItem = eventData.pointerDrag?.GetComponent<DragDropMotor>();
        if (draggedItem == null) return;

        // ✅ Check if this item is valid for the slot
        if (!IsAcceptedID(draggedItem.itemID))
        {
            Debug.LogWarning($"❌ {draggedItem.itemID} not accepted in {gameObject.name}");
            return;
        }

        // ✅ Enable correct LED form
        ledCorrectForm.SetActive(true);

        // 🚫 Hide dragged item
        draggedItem.gameObject.SetActive(false);

        isFilled = true;
        Debug.Log($"✅ {gameObject.name} filled with {draggedItem.itemID}");
        ShowFeedback("Correct Placement", Color.green);

        // 🧠 Notify build progress
        if (motoBuild != null)
            motoBuild.NotifySlotFilled();
    }

    private bool IsAcceptedID(string id)
    {
        foreach (var valid in acceptedIDs)
        {
            if (valid == id)
                return true;
        }
        return false;
    }

    public bool IsFilled()
    {
        return isFilled;
    }
}
