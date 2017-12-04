using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BoardMarker : MovingObject
{
    public override void OnEndDrag(PointerEventData data)
    {
        base.OnEndDrag(data);
        Destroy(rect.gameObject);
        
        Board.instance.Background.GetComponent<Mask>().showMaskGraphic = true;
    }
}
