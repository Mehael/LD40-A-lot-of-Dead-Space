using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CanvasScript{
    public static float scaleFactor = 37f;

    public static void SetPivot(this RectTransform rectTransform, Vector2 pivot)
    {
        Vector2 size = rectTransform.sizeDelta / scaleFactor;
        Vector2 deltaPivot = rectTransform.pivot - pivot;
        Vector3 deltaPosition = new Vector3(deltaPivot.x * size.x, deltaPivot.y * size.y);
        rectTransform.pivot = pivot;
        rectTransform.localPosition -= deltaPosition;
    }
}
