using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Inventardagi bitta narsa haqida ma'lumot.
[System.Serializable]
public class ItemData
{
    public string name;
    public Sprite icon;
    public Color color;

    public ItemData(string name, Sprite icon, Color color)
    {
        this.name = name;
        this.icon = icon;
        this.color = color;
    }
}

// O'yinning markaziy boshqaruvchisi: xabarlar, inventar, g'alaba va qayta boshlash.
public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    [Header("UI")]
    public Text messageText;
    public GameObject winPanel;
    public Button restartButton;
    public InventoryUI inventoryUI;
    public KeypadUI keypad;

    [HideInInspector] public bool noteFound;
    [HideInInspector] public bool safeOpened;

    readonly List<ItemData> items = new List<ItemData>();
    int selectedIndex = -1;
    Coroutine messageRoutine;

    // Panel ochiq bo'lsa, xonadagi narsalarga bosish bloklanadi.
    public bool IsBusy =>
        (keypad != null && keypad.IsOpen) || (winPanel != null && winPanel.activeSelf);

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        winPanel.SetActive(false);
        restartButton.onClick.AddListener(Restart);
        inventoryUI.Refresh(items, selectedIndex);
        ShowMessage("Siz qulflangan xonadasiz. Atrofni ko'zdan kechiring.");
    }

    // ---------- Xabarlar ----------
    public void ShowMessage(string text)
    {
        if (messageRoutine != null) StopCoroutine(messageRoutine);
        messageRoutine = StartCoroutine(MessageRoutine(text));
    }

    IEnumerator MessageRoutine(string text)
    {
        messageText.text = text;
        SetAlpha(1f);
        yield return new WaitForSeconds(3.5f);
        for (float t = 0f; t < 1f; t += Time.deltaTime * 2f)
        {
            SetAlpha(1f - t);
            yield return null;
        }
        messageText.text = "";
    }

    void SetAlpha(float a)
    {
        Color c = messageText.color;
        c.a = a;
        messageText.color = c;
    }

    // ---------- Inventar ----------
    public bool AddItem(ItemData item)
    {
        if (items.Count >= inventoryUI.SlotCount) return false;
        items.Add(item);
        inventoryUI.Refresh(items, selectedIndex);
        return true;
    }

    public bool HasItem(string itemName)
    {
        return items.Exists(i => i.name == itemName);
    }

    public bool HasSelected(string itemName)
    {
        return selectedIndex >= 0 && selectedIndex < items.Count && items[selectedIndex].name == itemName;
    }

    public void RemoveItem(string itemName)
    {
        int index = items.FindIndex(i => i.name == itemName);
        if (index < 0) return;
        items.RemoveAt(index);
        selectedIndex = -1;
        inventoryUI.Refresh(items, selectedIndex);
    }

    public void SelectSlot(int index)
    {
        if (index >= items.Count)
        {
            ShowMessage("Bu uyacha bo'sh.");
            return;
        }

        selectedIndex = (selectedIndex == index) ? -1 : index;
        inventoryUI.Refresh(items, selectedIndex);

        if (selectedIndex >= 0)
            ShowMessage("Tanlandi: " + items[selectedIndex].name + ". Endi biror narsaga bosing.");
        else
            ShowMessage("Tanlov bekor qilindi.");
    }

    // ---------- G'alaba ----------
    public void Win()
    {
        winPanel.SetActive(true);
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
