using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HasTaggedItem : Task {
    public string Tag = "PlayButton";

    public override bool IsCompleted()
    {
        return Board.sprites.Count(s => s.Tag == Tag) > 0;
    }
}
