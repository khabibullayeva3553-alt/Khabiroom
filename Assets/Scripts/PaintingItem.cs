using System.Collections;
using UnityEngine;

// Devordagi rasm: bosilsa yonga burilib, orqasidagi qog'ozni ko'rsatadi.
[RequireComponent(typeof(BoxCollider2D))]
public class PaintingItem : Interactable
{
    public float swingAngle = -70f;
    public float swingTime = 0.9f;
    bool moved;

    public override void OnTap()
    {
        GameController gc = GameController.Instance;

        if (!moved)
        {
            moved = true;
            gc.noteFound = true;
            StartCoroutine(Swing());
            gc.ShowMessage("Rasm orqasida qog'oz bor! Unda yozilgan: \"Soatga qara\".");
        }
        else
        {
            gc.ShowMessage("Rasm yonga burilgan. Orqasida: \"Soatga qara\".");
        }
    }

    // Rasm pastki chap burchagi atrofida buriladi.
    IEnumerator Swing()
    {
        Bounds b = GetComponent<BoxCollider2D>().bounds;
        Vector3 pivot = new Vector3(b.min.x, b.min.y, 0f);

        float done = 0f;
        for (float t = 0f; t < 1f; t += Time.deltaTime / swingTime)
        {
            float target = swingAngle * Mathf.SmoothStep(0f, 1f, t);
            transform.RotateAround(pivot, Vector3.forward, target - done);
            done = target;
            yield return null;
        }
        transform.RotateAround(pivot, Vector3.forward, swingAngle - done);
    }
}
