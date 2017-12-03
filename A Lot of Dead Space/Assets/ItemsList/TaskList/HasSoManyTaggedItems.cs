using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HasSoManyTaggedItems : Task
{
    public string Tag = "PlayButton";
    public int MaxAmount = 1;

    public override bool IsCompleted()
    {
        return Board.sprites.Count(s => s.Tag == Tag) <= MaxAmount;
    }
}
