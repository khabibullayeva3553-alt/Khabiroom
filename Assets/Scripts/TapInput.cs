using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

// Point and Click: ekranga bosish (sichqoncha yoki touch) va bosilgan obyektni topish.
public class TapInput : MonoBehaviour
{
    Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        GameController gc = GameController.Instance;
        if (gc == null || gc.IsBusy) return;

        Vector2 screenPos = Vector2.zero;
        int touchId = -1;
        bool pressed = false;

        Touchscreen ts = Touchscreen.current;
        if (ts != null && ts.primaryTouch.press.wasPressedThisFrame)
        {
            pressed = true;
            screenPos = ts.primaryTouch.position.ReadValue();
            touchId = ts.primaryTouch.touchId.ReadValue();
        }
        else if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            pressed = true;
            screenPos = Mouse.current.position.ReadValue();
        }

        if (!pressed) return;

        // UI (inventar tugmalari) ustiga bosilgan bo'lsa, xonaga bosilmaydi.
        if (EventSystem.current != null)
        {
            bool overUI = touchId >= 0
                ? EventSystem.current.IsPointerOverGameObject(touchId)
                : EventSystem.current.IsPointerOverGameObject();
            if (overUI) return;
        }

        Vector2 world = cam.ScreenToWorldPoint(screenPos);
        Collider2D[] hits = Physics2D.OverlapPointAll(world);

        Interactable best = null;
        foreach (Collider2D h in hits)
        {
            Interactable it = h.GetComponent<Interactable>();
            if (it != null && (best == null || it.priority > best.priority))
                best = it;
        }

        if (best != null) best.OnTap();
    }
}
