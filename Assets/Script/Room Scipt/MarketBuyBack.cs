using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MarketBuyBack : MonoBehaviour
{
    [Header("Item Info Display (UI)")]
    public TMP_Text nameText;
    public TMP_Text priceText;
    public TMP_Text descriptionText;
    public Image itemDisplayImage;

    [Header("Subtitle Text")]
    public TMP_Text subtitleText;
    public GameObject TextBG;

    [Header("Money UI in Market")]
    public TMP_Text marketMoneyText;

    [Header("Inventory")]
    public List<GameObject> inventoryItems;

    private string selectedItemName;
    private int selectedItemPrice;
    private GameObject currentMarketObject;

    void Start()
    {
        ClearUI();
    }

    void OnEnable()
    {
        UpdateMarketMoneyUI();
    }

    public void SetSelectedItem(MRItemDesc item)
    {
        selectedItemName = item.itemName;
        selectedItemPrice = item.itemPrice;
        currentMarketObject = item.gameObject;

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

    public void BuySelectedItem()
    {
        if (string.IsNullOrEmpty(selectedItemName)) return;

        if (Inventory.instance.SpendMoney(selectedItemPrice))
        {
            Debug.Log("Purchased: " + selectedItemName);

            foreach (GameObject slot in inventoryItems)
            {
                if (slot.name == selectedItemName)
                {
                    slot.SetActive(true);
                    break;
                }
            }

            if (currentMarketObject != null)
                currentMarketObject.SetActive(false);

            StartCoroutine(ShowFeedback("Item Purchased!", Color.green));
            UpdateMarketMoneyUI();
            ClearUI();
        }
        else
        {
            StartCoroutine(ShowFeedback("Not enough coins!", Color.red));
            UpdateMarketMoneyUI();
        }
    }

    void UpdateMarketMoneyUI()
    {
        if (marketMoneyText != null)
            marketMoneyText.text = Inventory.instance.money.ToString() + " Ʌ";
    }

    public void ClearUI()
    {
        selectedItemName = "";
        selectedItemPrice = 0;

        if (nameText) nameText.text = "";
        if (priceText) priceText.text = "";
        if (descriptionText) descriptionText.text = "";

        if (itemDisplayImage)
        {
            itemDisplayImage.sprite = null;
            itemDisplayImage.enabled = false;
        }
    }

    IEnumerator ShowFeedback(string message, Color color)
    {
        if (!subtitleText) yield break;

        TextBG.SetActive(true);
        subtitleText.enabled = true;
        subtitleText.text = message;
        subtitleText.color = color;

        yield return new WaitForSeconds(2f);

        subtitleText.enabled = false;
        TextBG.SetActive(false);
    }
}