using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Symmetry : Task {
    public string Tag = "";
    public float aspectRatio = 1f;

    public override bool IsCompleted()
    {
        var tag1items = Board.sprites.Where(s => s.Tag == Tag);
        if (tag1items.Count() > 0)
            InterestingSprite = tag1items.First();

        foreach (var t in tag1items)
            if (t.size.x / t.size.y != aspectRatio)
                return false;

        return true;
    }
}
