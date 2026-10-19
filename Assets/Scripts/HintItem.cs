using UnityEngine;

// Bosilganda faqat ma'lumot (ko'rsatma) beradigan narsa: soat, qog'oz va h.k.
public class HintItem : Interactable
{
    [TextArea] public string message;

    public override void OnTap()
    {
        GameController.Instance.ShowMessage(message);
        Bump();
    }
}
