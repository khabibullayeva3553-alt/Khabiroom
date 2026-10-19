using UnityEngine;

// Xonani telefon ekraniga (portret) moslab, kameraning o'lchamini sozlaydi.
public class CameraFit : MonoBehaviour
{
    public float roomWidth = 9f;
    public float roomHeight = 16f;
    Camera cam;

    void Awake()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        float sizeByHeight = roomHeight / 2f;
        float sizeByWidth = roomWidth / 2f / cam.aspect;
        cam.orthographicSize = Mathf.Max(sizeByHeight, sizeByWidth);
    }
}
