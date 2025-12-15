using TMPro;
using UnityEngine;
using UnityEngine.LowLevel;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    [Header("Inventory Canvas")]
    public GameObject inventoryRoot;

    [Header("Coin Canvas")]
    public TMP_Text moneyText;

    [Header("Money")]
    public int money = 0;

    [Header("Keybind")]
    public KeyCode inventory = KeyCode.Tab;

    private Movement movement;
    private bool isOpen = false;

    void Awake()
    {
        // Singleton pattern
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            movement = player.GetComponent<Movement>();
        }

        if (inventoryRoot != null)
            inventoryRoot.SetActive(false);

        UpdateUI();
        LockCursor();
    }

    void Update()
    {
        if (Input.GetKeyDown(inventory))
        {
            ToggleInventory();
        }
    }

    private void ToggleInventory()
    {
        isOpen = !isOpen;

        if (inventoryRoot != null)
            inventoryRoot.SetActive(isOpen);

        if (isOpen)
        {
            UpdateUI();
            UnlockCursor();
            if (movement != null) movement.enabled = false;
        }
        else
        {
            LockCursor();
            if (movement != null) movement.enabled = true;
        }
    }

    public void AddMoney(int amount)
    {
        money += amount;
        UpdateUI();
    }

    public bool SpendMoney(int amount)
    {
        if (money >= amount)
        {
            money -= amount;
            UpdateUI();
            return true;
        }
        else
        {
            Debug.Log("Not enough money!");
            return false;
        }
    }

    private void UpdateUI()
    {
        if (moneyText != null)
        {
            moneyText.text = money.ToString() + " Ʌ";
        }
    }

    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
