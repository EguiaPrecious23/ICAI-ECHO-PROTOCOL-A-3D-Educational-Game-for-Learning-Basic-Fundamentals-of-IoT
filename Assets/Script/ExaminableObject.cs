using UnityEngine;

public class ExaminableObject : MonoBehaviour
{
    public string objectName;
    public bool isBeingExamined = false;
    public Vector3 examineOffset = new Vector3(0, 0, 1f);
    public float desiredExamineSize = 0.1f;

    private Vector3 originalScale;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Transform originalParent;

    public void StartExamine(Transform cameraTransform)
    {
        if (isBeingExamined) return;

        originalPosition = transform.position;
        originalRotation = transform.rotation;
        originalScale = transform.localScale;
        originalParent = transform.parent;

        transform.SetParent(cameraTransform);
        transform.localPosition = examineOffset;
        isBeingExamined = true;

        // ✅ Auto-scale to consistent screen size
        float maxSize = GetLargestBoundsSize(transform);
        if (maxSize > 0)
        {
            float scaleFactor = desiredExamineSize / maxSize;
            transform.localScale = originalScale * scaleFactor;
        }
    }

    public void StopExamine()
    {
        if (!isBeingExamined) return;

        transform.SetParent(originalParent);
        transform.position = originalPosition;
        transform.rotation = originalRotation;
        transform.localScale = originalScale;

        isBeingExamined = false;
    }

    float GetLargestBoundsSize(Transform obj)
    {
        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
        if (renderers.Length == 0) return 1f;

        Bounds combinedBounds = renderers[0].bounds;
        for (int i = 1; i < renderers.Length; i++)
        {
            combinedBounds.Encapsulate(renderers[i].bounds);
        }

        return Mathf.Max(combinedBounds.size.x, combinedBounds.size.y, combinedBounds.size.z);
    }
}