using System.Collections;
using UnityEngine;

// Xonadagi bosiladigan har bir narsaning asosiy (bazaviy) sinfi.
public abstract class Interactable : MonoBehaviour
{
    // Bir nechta obyekt ustma-ust tushsa, priority kattasi tanlanadi.
    public int priority = 0;

    public abstract void OnTap();

    protected void Bump()
    {
        StartCoroutine(BumpRoutine());
    }

    protected void Shake()
    {
        StartCoroutine(ShakeRoutine());
    }

    IEnumerator BumpRoutine()
    {
        Vector3 baseScale = transform.localScale;
        for (float t = 0f; t < 1f; t += Time.deltaTime / 0.2f)
        {
            transform.localScale = baseScale * (1f + 0.08f * Mathf.Sin(t * Mathf.PI));
            yield return null;
        }
        transform.localScale = baseScale;
    }

    IEnumerator ShakeRoutine()
    {
        Vector3 basePos = transform.position;
        for (float t = 0f; t < 1f; t += Time.deltaTime / 0.35f)
        {
            float offset = Mathf.Sin(t * 40f) * 0.1f * (1f - t);
            transform.position = basePos + new Vector3(offset, 0f, 0f);
            yield return null;
        }
        transform.position = basePos;
    }
}
