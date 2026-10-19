using UnityEngine;

// Olib inventarga solinadigan narsa (masalan, kalit).
public class PickupItem : Interactable
{
    public string itemName = "Kalit";
    public Sprite icon;
    public Color iconColor = Color.white;
    [TextArea] public string pickupMessage = "Kalitni oldingiz!";

    public override void OnTap()
    {
        GameController gc = GameController.Instance;

        if (gc.AddItem(new ItemData(itemName, icon, iconColor)))
        {
            gc.ShowMessage(pickupMessage);
            gameObject.SetActive(false);
        }
        else
        {
            gc.ShowMessage("Inventar to'lgan.");
        }
    }
}
