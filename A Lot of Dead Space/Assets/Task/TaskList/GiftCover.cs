using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GiftCover : Task {
    public string ObjTag = "";
    public string GiftTag = "";

    public override bool IsCompleted()
    {
        var objItems = Board.sprites.Where(s => s.Tag == ObjTag);
        var giftItems = Board.sprites.Where(s => s.Tag == GiftTag);

        var isMatched = false;
        foreach (var obj in objItems)
            foreach (var gift in giftItems)
                if (Board.Intersection(obj, gift) == gift.size.x * gift.size.y)
                    isMatched = true;

        return isMatched;
    }
}
