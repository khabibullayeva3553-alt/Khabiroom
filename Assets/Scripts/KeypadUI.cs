using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Seyf uchun raqamli klaviatura (3 xonali kod).
public class KeypadUI : MonoBehaviour
{
    public GameObject panel;
    public Text display;
    public Button[] digitButtons;   // indeks = raqam (0..9)
    public Button clearButton;
    public Button closeButton;

    string correctCode = "";
    string entered = "";
    Action onSuccess;
    bool locked;

    public bool IsOpen => panel != null && panel.activeSelf;

    void Start()
    {
        for (int i = 0; i < digitButtons.Length; i++)
        {
            int digit = i;
            digitButtons[i].onClick.AddListener(() => PressDigit(digit));
        }
        clearButton.onClick.AddListener(ClearEntry);
        closeButton.onClick.AddListener(Close);
        panel.SetActive(false);
    }

    public void Open(string code, Action success)
    {
        correctCode = code;
        onSuccess = success;
        entered = "";
        locked = false;
        display.color = Color.white;
        Refresh();
        panel.SetActive(true);
    }

    public void Close()
    {
        panel.SetActive(false);
    }

    void PressDigit(int digit)
    {
        if (locked || entered.Length >= correctCode.Length) return;

        entered += digit;
        Refresh();

        if (entered.Length == correctCode.Length)
        {
            if (entered == correctCode) StartCoroutine(Success());
            else StartCoroutine(Wrong());
        }
    }

    void ClearEntry()
    {
        if (locked) return;
        entered = "";
        Refresh();
    }

    void Refresh()
    {
        string text = "";
        for (int i = 0; i < correctCode.Length; i++)
        {
            text += (i < entered.Length) ? entered[i].ToString() : "_";
            if (i < correctCode.Length - 1) text += " ";
        }
        display.text = text;
    }

    IEnumerator Success()
    {
        locked = true;
        display.color = new Color(0.4f, 1f, 0.5f);
        yield return new WaitForSeconds(0.4f);
        panel.SetActive(false);
        onSuccess?.Invoke();
    }

    IEnumerator Wrong()
    {
        locked = true;
        display.color = new Color(1f, 0.4f, 0.4f);
        GameController.Instance.ShowMessage("Noto'g'ri kod.");
        yield return new WaitForSeconds(0.7f);
        entered = "";
        display.color = Color.white;
        Refresh();
        locked = false;
    }
}
