using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScaleMarker : EventTrigger {
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
        if (!isDragged)
            image.color = highlightedState;
    }

    public override void OnPointerExit(PointerEventData data)
    {
        if (!isDragged)
            image.color = defaultState;
    }

    Vector2 mouseFromCenterShift;
    Vector2 maxScale;
    Vector2 startSize;
    Vector2 Coords;
    public override void OnPointerDown(PointerEventData eventData)
    {
        isDragged = true;
        image.color = draggedState;

        lastMouseCoords = GetMouseCoords();
        startingMousePos = lastMouseCoords;

        mouseFromCenterShift = new Vector2(startingMousePos.x - transform.parent.position.x,
            startingMousePos.y - transform.parent.position.y);

        //valid scale
        Coords = Board.instance.GetCoords(transform.parent.position);
        Debug.Log(Coords);
        maxScale = new Vector2(Board.instance.Width - Coords.x, Board.instance.Height - Coords.y);
        oldCenter = transform.parent.position;

        //startSize
        startSize = new Vector2(Mathf.Ceil(rect.sizeDelta.x / scaleFactor),
            Mathf.Ceil(rect.sizeDelta.y / scaleFactor));
    }

    float scaleFactor;
    public override void OnPointerUp(PointerEventData eventData)
    {
        isDragged = false;
        image.color = defaultState;
    }

    Vector2 lastMouseCoords;
    Vector2 startingMousePos;
    Vector2 oldCenter;
    public override void OnDrag(PointerEventData data)
    {
        var newMouse = GetMouseCoords();
        rect.sizeDelta = ValidateCoords(startSize + newMouse - startingMousePos) * scaleFactor;

        var newCenter = (newMouse + startingMousePos) / 2;
        newCenter = ValidateCenter(newCenter);

        transform.parent.position = newCenter - mouseFromCenterShift;
        oldCenter = new Vector2(transform.parent.position.x, transform.parent.position.y) + mouseFromCenterShift;
    }

    private Vector2 ValidateCenter(Vector2 newCenter)
    {
        if (name == "right" || name == "left")
        {
            if ((isMaxScaled && name == "right" && newCenter.x > oldCenter.x) ||
                (isMaxScaled && name == "left" && newCenter.x < oldCenter.x)) newCenter.x = oldCenter.x;
            if (rect.sizeDelta.x == scaleFactor)
                if ((name == "right" && newCenter.x < startingMousePos.x)
                || (name == "left" && newCenter.x > startingMousePos.x)) newCenter.x = startingMousePos.x;
            newCenter.y = startingMousePos.y;
        }
        else
        {
            if ((isMaxScaled && name == "top" && newCenter.y > oldCenter.y) ||
                (isMaxScaled && name == "bottom" && newCenter.y < oldCenter.y)) newCenter.y = oldCenter.y;
            if (rect.sizeDelta.y == scaleFactor)
                if ((name == "top" && newCenter.y < startingMousePos.y)
                || (name == "bottom" && newCenter.y > startingMousePos.y)) newCenter.y = startingMousePos.y;
            newCenter.x = startingMousePos.x;
        }

        return newCenter;
    }

    bool isMaxScaled = false;
    private Vector2 ValidateCoords(Vector2 vector2)
    {
        isMaxScaled = false;
        if (name == "right" || name == "left")
        {
            vector2.y = 1;
            if (vector2.x < 1) vector2.x = 1;
            if (vector2.x >= maxScale.x)
            {
                vector2.x = maxScale.x;
                isMaxScaled = true;
            }
        }
            else
        {
            vector2.x = 1;
            if (vector2.y < 1) vector2.y = 1;
            if (vector2.y >= maxScale.y)
            {
                vector2.y = maxScale.y;
                isMaxScaled = true;
            }
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
        if (name == "right") {
            var snappedSize = new Vector2(Mathf.Ceil(rect.sizeDelta.x / scaleFactor),
                Mathf.Ceil(rect.sizeDelta.y / scaleFactor)) * scaleFactor;
            var pDelta = rect.sizeDelta.x - snappedSize.x; 

            rect.sizeDelta = snappedSize;

            transform.parent.position -= new Vector3( pDelta / scaleFactor, 0,0);
        }
    }
}
