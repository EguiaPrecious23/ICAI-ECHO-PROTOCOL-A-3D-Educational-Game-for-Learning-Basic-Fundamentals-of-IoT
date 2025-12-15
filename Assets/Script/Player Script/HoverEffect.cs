using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Hover Color")]
    public Color hoverColor = Color.white;
    public Color normalColor = Color.white;

    private Image targetImage;

    private void Awake()
    {
        targetImage = GetComponent<Image>();

        if (targetImage != null)
            targetImage.color = normalColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (targetImage != null)
            targetImage.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (targetImage != null)
            targetImage.color = normalColor;
    }

    public void ResetColor()
    {
        if (targetImage != null)
            targetImage.color = normalColor;
    }
}