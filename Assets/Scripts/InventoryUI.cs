using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Ekran pastidagi inventar paneli (4 ta uyacha).
public class InventoryUI : MonoBehaviour
{
    public Button[] slotButtons;
    public Image[] slotFrames;
    public Image[] slotIcons;
    public Color normalColor = new Color(0.2f, 0.2f, 0.22f, 0.95f);
    public Color selectedColor = new Color(0.95f, 0.75f, 0.2f, 1f);

    public int SlotCount => slotButtons.Length;

    void Start()
    {
        for (int i = 0; i < slotButtons.Length; i++)
        {
            int index = i;
            slotButtons[i].onClick.AddListener(() => GameController.Instance.SelectSlot(index));
        }
    }

    public void Refresh(List<ItemData> items, int selectedIndex)
    {
        for (int i = 0; i < slotButtons.Length; i++)
        {
            bool filled = i < items.Count;
            slotIcons[i].enabled = filled;
            if (filled)
            {
                slotIcons[i].sprite = items[i].icon;
                slotIcons[i].color = items[i].color;
            }
            slotFrames[i].color = (i == selectedIndex) ? selectedColor : normalColor;
        }
    }
}
