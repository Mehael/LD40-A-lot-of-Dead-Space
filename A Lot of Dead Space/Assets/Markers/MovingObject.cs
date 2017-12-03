using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MovingObject : Marker {
    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        SetPivot(new Vector2(0.5f, 0.5f));
    }

    public override void OnDrag(PointerEventData data)
    {
        transform.parent.position = GetMouseCoords();
    }

    public override void OnEndDrag(PointerEventData data)
    {
        var finalMousePosition = GetMouseCoords();
        if (finalMousePosition.x < 0 || finalMousePosition.x > Board.instance.Width
            || finalMousePosition.y < 0 || finalMousePosition.y > Board.instance.Height)
        {
            Destroy(rect.gameObject);
        }
        else
        {
            base.OnEndDrag(data);

            SetPivot(new Vector2(0, 0));

            if (leftbottom.x < 0) leftbottom.x = 0;
            if (leftbottom.y < 0) leftbottom.y = 0;

            if (leftbottom.x > Board.instance.Width - size.x)
                leftbottom.x = Board.instance.Width - size.x;
            if (leftbottom.y > Board.instance.Height - size.y)
                leftbottom.y = Board.instance.Height - size.y;

            transform.parent.position = leftbottom;
        }
    }
}
