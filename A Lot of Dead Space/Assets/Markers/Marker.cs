using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Marker : EventTrigger {
    protected Vector3 startPosition;
    Image image;
    public Vector2 size;
    public Vector2 leftbottom;

    public Color32 defaultState = Color.green;
    public Color32 highlightedState = Color.yellow;
    public Color32 draggedState = Color.red;

    public RectTransform rect;
    bool isDragged = false;

    private void Awake()
    {
        image = GetComponent<Image>();
        image.color = defaultState;
        rect = transform.parent.GetComponent<RectTransform>();
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        isDragged = true;
        image.color = draggedState;

        startPosition = transform.parent.position;
        base.OnPointerDown(eventData);       
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

    public override void OnPointerUp(PointerEventData eventData)
    {
        isDragged = false;
        image.color = defaultState;
    }

    public void SetPivot(Vector2 pivot)
    {
        var beforePivotChangePos = transform.position;
        rect.anchorMin = pivot;
        rect.anchorMax = pivot;
        rect.pivot = pivot;
        rect.position += beforePivotChangePos - transform.position;
    }

    public static Vector2 GetMouseCoords()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return new Vector3(ray.origin.x, ray.origin.y, 0);
    }

    public void RecalculateGridRect()
    {
        size = new Vector2(Mathf.Round(rect.sizeDelta.x / CanvasScript.scaleFactor),
                Mathf.Round(rect.sizeDelta.y / CanvasScript.scaleFactor));

        SetPivot(new Vector2(0.5f, 0.5f));
        leftbottom = new Vector2(Mathf.Round(transform.parent.position.x - (size.x) / 2),
            Mathf.Round(transform.parent.position.y - (size.y) / 2));
    }

    public override void OnEndDrag(PointerEventData data)
    {
        image.color = defaultState;
        RecalculateGridRect();
    }

    public void SetSize(Vector2 size)
    {
        rect.sizeDelta = size * CanvasScript.scaleFactor;
    }
}
