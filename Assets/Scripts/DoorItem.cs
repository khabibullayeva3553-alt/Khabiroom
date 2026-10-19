using System.Collections;
using UnityEngine;

// Chiqish eshigi. Inventardan kalitni tanlab, eshikka bosilsa ochiladi va o'yin tugaydi.
public class DoorItem : Interactable
{
    public Transform doorHinge;          // eshik panjarasi (chap qirrasi atrofida ochiladi)
    public SpriteRenderer behind;        // eshik ortidagi joy
    public Color openedColor = new Color(1f, 0.95f, 0.7f);
    public string requiredItem = "Kalit";

    bool open;

    public override void OnTap()
    {
        if (open) return;

        GameController gc = GameController.Instance;

        if (gc.HasSelected(requiredItem))
        {
            open = true;
            gc.RemoveItem(requiredItem);
            StartCoroutine(OpenRoutine());
        }
        else if (gc.HasItem(requiredItem))
        {
            gc.ShowMessage("Eshik qulflangan. Inventardan kalitni tanlab, keyin eshikka bosing.");
            Shake();
        }
        else
        {
            gc.ShowMessage("Eshik qulflangan. Kalit kerak.");
            Shake();
        }
    }

    IEnumerator OpenRoutine()
    {
        GameController gc = GameController.Instance;
        gc.ShowMessage("Kalit mos tushdi! Eshik ochilmoqda...");
        behind.color = openedColor;

        for (float t = 0f; t < 1f; t += Time.deltaTime / 1f)
        {
            float x = Mathf.Lerp(1f, 0.08f, Mathf.SmoothStep(0f, 1f, t));
            doorHinge.localScale = new Vector3(x, 1f, 1f);
            yield return null;
        }
        doorHinge.localScale = new Vector3(0.08f, 1f, 1f);

        yield return new WaitForSeconds(0.6f);
        gc.Win();
    }
}
