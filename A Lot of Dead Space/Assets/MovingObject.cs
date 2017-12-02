using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovingObject : EventTrigger {
    Vector3 startPosition;

    public override void OnBeginDrag(PointerEventData data)
    {
        startPosition = transform.position;
    }

    public override void OnDrag(PointerEventData data)
    {
        transform.position = GetMouseCoords();
    }

    private static Vector3 GetMouseCoords()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return new Vector3(ray.origin.x, ray.origin.y, 0);
    }

    public override void OnEndDrag(PointerEventData data)
    {
        transform.position = Board.instance.GetClosestSnap(GetMouseCoords());
    }
}
