using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class IsLightlyOverlapped : Task
{
    public string Tag = "";
    public float LegalPercent = 50f;

    public override bool IsCompleted()
    {
        var tag1items = Board.sprites.Where(s => s.Tag == Tag);
        var allowedSize = tag1items.Select(s => s.size.x * s.size.y).Sum() * LegalPercent / 100;

        if (tag1items.Count() > 0)
            InterestingSprite = tag1items.First();

        var coveredSize = 0f;
        foreach (var sprite2 in Board.sprites.Where(s => !s.isHeavy && !s.isAppealing))
            foreach (var sprite1 in tag1items)
                coveredSize += Board.Intersection(sprite1, sprite2);

        return coveredSize < allowedSize;
    }
}

