using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HasPerfectCover : Task
{
    public string Tag1 = "";
    public string Tag2 = "";

    public override bool IsCompleted()
    {
        var tag1items = Board.sprites.Where(s => s.Tag == Tag1);

        bool hasMatch = false;
        foreach (var sprite2 in Board.sprites.Where(s => s.Tag == Tag2))
            foreach (var sprite1 in tag1items)
                if (sprite1.leftbottom.Equals(sprite2.leftbottom)
                    && sprite1.size.Equals(sprite2.size)) hasMatch = true;

        return hasMatch;
    }
}
