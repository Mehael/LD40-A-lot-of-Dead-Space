using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class IsHeavyOverlapped : Task {
    public string Tag = "";

    public override bool IsCompleted()
    {
        var tag1items = Board.sprites.Where(s => s.Tag == Tag);
        if (tag1items.Count() > 0)
            InterestingSprite = tag1items.First();

        foreach (var sprite2 in Board.sprites.Where(s => s.isHeavy))
            foreach (var sprite1 in tag1items)
                if (sprite1 != sprite2
                    && Board.Intersection(sprite1, sprite2) > 0) return false;

        return true;
    }
}
