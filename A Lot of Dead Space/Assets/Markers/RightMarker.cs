using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RightMarker : Marker {
    Vector2 maxScale;

    Vector2 lastMouseCoords;
    Vector2 startingMousePos;

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);

        lastMouseCoords = GetMouseCoords();
        startingMousePos = lastMouseCoords;

        //valid scale
        RecalculateGridRect();
        if (name == "right" || name == "top")
            maxScale = new Vector2(Board.instance.Width - leftbottom.x, 
                Board.instance.Height- leftbottom.y);
        else
            maxScale = leftbottom + size;

        //pivots
        var pivot = Vector2.zero;
        if (name == "right")
            pivot = new Vector2(0, 0.5f);
        if (name == "left")
            pivot = new Vector2(1f, 0.5f);
        if (name == "bottom")
            pivot = new Vector2(0.5f, 1f);
        if (name == "top")
            pivot = new Vector2(0.5f, 0);

        SetPivot(pivot);
    }

    public override void OnDrag(PointerEventData data)
    {
        var newMouse = GetMouseCoords();
        var delta = Vector2.zero;
        if (name == "right" || name == "top")
            delta =  newMouse - startingMousePos;
        else
            delta = startingMousePos - newMouse;

        rect.sizeDelta = ValidateCoords(size + delta) * CanvasScript.scaleFactor;
    }

    private Vector2 ValidateCoords(Vector2 vector2)
    {
        if (name == "right" || name == "left")
        {
            vector2.y = size.y;
            if (vector2.x < 1) vector2.x = 1;
            if (vector2.x >= maxScale.x)
                vector2.x = maxScale.x;
        }
            else
        {
            vector2.x = size.x;
            if (vector2.y < 1) vector2.y = 1;
            if (vector2.y >= maxScale.y)
                vector2.y = maxScale.y;
        }
        return vector2;
    }

    public override void OnEndDrag(PointerEventData data)
    {
        rect.sizeDelta = new Vector2(Mathf.Round(rect.sizeDelta.x / CanvasScript.scaleFactor),
                Mathf.Round(rect.sizeDelta.y / CanvasScript.scaleFactor)) * CanvasScript.scaleFactor;

        base.OnEndDrag(data);
    }
}
