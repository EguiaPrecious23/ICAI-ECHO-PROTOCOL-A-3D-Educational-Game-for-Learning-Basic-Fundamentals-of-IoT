using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MarketInventoryEqual : MonoBehaviour
{
    [Header("Market Item")]
    public GameObject firstMItem;
    public GameObject secondMItem;
    public GameObject thirdMItem;
    public GameObject fourthMItem;
    public GameObject fifthMItem;
    public GameObject sixMItem;
    public GameObject sevenMItem;

    [Header("Inventory Item")]
    public GameObject firstIItem;
    public GameObject secondIItem;
    public GameObject thirdIItem;
    public GameObject fourthIItem;
    public GameObject fifthIItem;
    public GameObject sixIItem;
    public GameObject sevenIItem;

    [Header("MRTable Item")]
    public GameObject firstTItem;
    public GameObject secondTItem;
    public GameObject thirdTItem;
    public GameObject fourthTItem;
    public GameObject fifthTItem;
    public GameObject sixTItem;
    public GameObject sevenTItem;

    [Header("Item Info Display (UI)")]
    public TMP_Text nameText;
    public TMP_Text priceText;
    public TMP_Text descriptionText;
    public Image itemDisplayImage;

    private string selectedItemName;
    private int selectedItemPrice;

    private bool firstDone, secondDone, thirdDone, fourthDone, fifthDone, sixDone, sevenDone;

    void Update()
    {
        if (!firstDone && firstMItem && !firstMItem.activeSelf)
        {
            firstIItem?.SetActive(true);
            firstTItem?.SetActive(true);
            firstDone = true;
        }

        if (!secondDone && secondMItem && !secondMItem.activeSelf)
        {
            secondIItem?.SetActive(true);
            secondTItem?.SetActive(true);
            secondDone = true;
        }

        if (!thirdDone && thirdMItem && !thirdMItem.activeSelf)
        {
            thirdIItem?.SetActive(true);
            thirdTItem?.SetActive(true);
            thirdDone = true;
        }

        if (!fourthDone && fourthMItem && !fourthMItem.activeSelf)
        {
            fourthIItem?.SetActive(true);
            fourthTItem?.SetActive(true);
            fourthDone = true;
        }

        if (!fifthDone && fifthMItem && !fifthMItem.activeSelf)
        {
            fifthIItem?.SetActive(true);
            fifthTItem?.SetActive(true);
            fifthDone = true;
        }

        if (!sixDone && sixMItem && !sixMItem.activeSelf)
        {
            sixIItem?.SetActive(true);
            sixTItem?.SetActive(true);
            sixDone = true;
        }

        if (!sevenDone && sevenMItem && !sevenMItem.activeSelf)
        {
            sevenIItem?.SetActive(true);
            sevenTItem?.SetActive(true);
            sevenDone = true;
        }
    }

    public void SetSelectedItem(MRItemDesc item)
    {
        selectedItemName = item.itemName;
        selectedItemPrice = item.itemPrice;

        if (nameText) nameText.text = item.itemName;
        if (priceText) priceText.text = item.itemPrice + " Ʌ";
        if (descriptionText) descriptionText.text = item.itemDescription;

        if (itemDisplayImage && item.itemImage)
        {
            Image src = item.itemImage.GetComponent<Image>();
            itemDisplayImage.sprite = src ? src.sprite : null;
            itemDisplayImage.enabled = src != null;
        }
    }
}