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
        if (LessMode)
            return Board.sprites.Count(s => s.Tag == Tag) > MaxAmount;
        else
            return Board.sprites.Count(s => s.Tag == Tag) <= MaxAmount;
    }
}
