using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RightMarker : EventTrigger {
    Image image;
    public Color32 defaultState = Color.green;
    public Color32 highlightedState = Color.yellow;
    public Color32 draggedState = Color.red;

    bool isDragged = false;
    RectTransform rect;
    private void Start()
    {
        image = GetComponent<Image>();
        rect = transform.parent.GetComponent<RectTransform>();
        image.color = defaultState;

        scaleFactor = CanvasScript.scaleFactor;
    }

    public override void OnPointerEnter(PointerEventData data)
    {
        if (isDragged) return;

        image.color = highlightedState;
    }

    public override void OnPointerExit(PointerEventData data)
    {
        if (!isDragged)
            image.color = defaultState;
    }

    Vector2 maxScale;
    Vector2 size;
    public override void OnPointerDown(PointerEventData eventData)
    {
        isDragged = true;
        image.color = draggedState;

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
        var parentRect = transform.parent.GetComponent<RectTransform>();
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

    private void SetPivot(Vector2 pivot)
    {
        var beforePivotChangePos = transform.position;
        rect.anchorMin = pivot;
        rect.anchorMax = pivot;
        rect.pivot = pivot;
        rect.position += beforePivotChangePos - transform.position;
    }

    float scaleFactor;
    public override void OnPointerUp(PointerEventData eventData)
    {
        isDragged = false;
        image.color = defaultState;
    }

    Vector2 lastMouseCoords;
    Vector2 startingMousePos;
    private Vector2 leftbottom;

    public override void OnDrag(PointerEventData data)
    {
        var newMouse = GetMouseCoords();
        var delta = Vector2.zero;
        if (name == "right" || name == "top")
            delta =  newMouse - startingMousePos;
        else
            delta = startingMousePos - newMouse;

        rect.sizeDelta = ValidateCoords(size + delta ) * scaleFactor;
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

    private Vector2 GetMouseCoords()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return new Vector2(ray.origin.x, ray.origin.y);
    }

    public override void OnEndDrag(PointerEventData data)
    {
        rect.sizeDelta = new Vector2(Mathf.Round(rect.sizeDelta.x / scaleFactor),
                Mathf.Round(rect.sizeDelta.y / scaleFactor)) * scaleFactor;

        RecalculateGridRect();
    }

    private void RecalculateGridRect()
    {
        size = new Vector2(Mathf.Round(rect.sizeDelta.x / scaleFactor),
                Mathf.Round(rect.sizeDelta.y / scaleFactor));

        SetPivot(new Vector2(0.5f, 0.5f));
        leftbottom = new Vector2(Mathf.Round(transform.parent.position.x - (size.x) / 2),
            Mathf.Round(transform.parent.position.y - (size.y) / 2));
    }
}
