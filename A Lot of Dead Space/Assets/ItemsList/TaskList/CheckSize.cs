using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CheckSize : Task {
    public string Tag = "none";
    public int RequiredSize = 1;

    public override bool IsCompleted()
    {
        var size = 0f;
        foreach (var sprite in Board.sprites.Where(s => s.Tag == Tag))
            size += sprite.size.x * sprite.size.y;

        return size >= RequiredSize;
    }
}
