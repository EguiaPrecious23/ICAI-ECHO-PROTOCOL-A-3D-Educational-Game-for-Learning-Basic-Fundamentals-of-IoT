using TMPro;
using UnityEngine;

public class SRImgItemHover : MonoBehaviour
{
    [Header("Raycast Camera")]
    public Camera playerCam;
    public float hoverDistance = 1f;

    [Header("Hover UI")]
    public GameObject hoverUI;
    public TMP_Text nameText;
    public TMP_Text desText;

    private SRItemHover currentItem;

    private void Update()
    {
        Ray ray = new Ray(playerCam.transform.position, playerCam.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, hoverDistance)) 
        {
            SRItemHover item = hit.collider.GetComponent<SRItemHover>();
            if (item != null)
            {
                if (currentItem != item)
                {
                    HidePreviousItemImage();
                    currentItem = item;
                    ShowItemInfo(item);
                }
            }
            else
            {
                HideUI();
            }
        }
        else
        {
            HideUI();
        }
    }

    void ShowItemInfo(SRItemHover item)
    {
        hoverUI.SetActive(true);

        nameText.text = item.itemName;
        desText.text = item.itemDescription;

        if (item.itemImage != null)
            item.itemImage.SetActive(true);
    }

    void HideUI()
    {
        if (hoverUI.activeSelf)
            hoverUI.SetActive(false);

        HidePreviousItemImage();
        currentItem = null;
    }

    void HidePreviousItemImage()
    {
        if (currentItem != null && currentItem.itemImage != null)
        {
            currentItem.itemImage.SetActive(false);
        }
    }
}
