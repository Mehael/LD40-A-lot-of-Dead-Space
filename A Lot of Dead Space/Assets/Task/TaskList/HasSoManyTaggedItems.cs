using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HasSoManyTaggedItems : Task
{
    public string Tag = "PlayButton";
    public int MaxAmount = 1;
    public bool LessMode = false;

    public override bool IsCompleted()
    {
        var items = Board.sprites.Where(s => s.Tag == Tag);
        if (items.Count()>0)
            InterestingSprite = items.First();

        if (LessMode)
            return items.Count() > MaxAmount;
        else
            return items.Count() <= MaxAmount;
    }
}
