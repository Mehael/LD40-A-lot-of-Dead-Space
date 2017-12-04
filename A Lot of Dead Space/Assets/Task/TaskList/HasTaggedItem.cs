using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HasTaggedItem : Task {
    public string Tag = "PlayButton";
    public bool DontHasMode = false;

    public override bool IsCompleted()
    {
        var amount = Board.sprites.Count(s => s.Tag == Tag);

        if (DontHasMode)
            return amount == 0;
        else
            return amount > 0;
    }
}
