using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HasTaggedItem : Task {
    public string Tag = "PlayButton";
    public bool DontHasMode = false;

    public override bool IsCompleted()
    {
        var items = Board.sprites.Where(s => s.Tag == Tag);
        var amount = items.Count();
        if (amount > 0)
            InterestingSprite = items.First();

        if (DontHasMode)
            return amount == 0;
        else
            return amount > 0;
    }
}
