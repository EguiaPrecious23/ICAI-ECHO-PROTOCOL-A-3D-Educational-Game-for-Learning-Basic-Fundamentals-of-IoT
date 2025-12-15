using UnityEngine;

public class MRItemDesc : MonoBehaviour
{
    [Header("Name & Description")]
    public string itemName;
    [TextArea] public string itemDescription;
    public int itemPrice;

    [Header("Item Image")]
    public GameObject itemImage;
}