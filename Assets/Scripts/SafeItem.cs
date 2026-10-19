using System.Collections;
using UnityEngine;

// Kod bilan ochiladigan seyf. To'g'ri kod kiritilsa, eshigi ochilib, ichidagi kalit ko'rinadi.
public class SafeItem : Interactable
{
    public KeypadUI keypad;
    public Transform doorHinge;     // seyf eshigi (chap qirrasi atrofida ochiladi)
    public GameObject keyObject;    // ichidagi kalit (boshida yashirin)
    public string code = "417";

    bool open;

    public override void OnTap()
    {
        GameController gc = GameController.Instance;

        if (open)
        {
            gc.ShowMessage("Seyf ochiq.");
            return;
        }

        if (!gc.noteFound)
            gc.ShowMessage("Seyf 3 xonali kod bilan ochiladi. Ko'rsatma qidiring.");

        keypad.Open(code, OnSolved);
    }

    void OnSolved()
    {
        open = true;
        GameController.Instance.safeOpened = true;
        keyObject.SetActive(true);
        StartCoroutine(OpenDoor());
        GameController.Instance.ShowMessage("Seyf ochildi! Ichida kalit yotibdi.");
    }

    IEnumerator OpenDoor()
    {
        for (float t = 0f; t < 1f; t += Time.deltaTime / 0.6f)
        {
            float x = Mathf.Lerp(1f, 0.12f, Mathf.SmoothStep(0f, 1f, t));
            doorHinge.localScale = new Vector3(x, 1f, 1f);
            yield return null;
        }
        doorHinge.localScale = new Vector3(0.12f, 1f, 1f);
    }
}
