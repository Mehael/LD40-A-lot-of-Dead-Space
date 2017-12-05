﻿using System.Collections;
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
        if (tag1items.Count() > 0)
            InterestingSprite = tag1items.First();

        var tag2items = Board.sprites.Where(s => s.Tag == ((Tag2 == "") ? s.Tag : Tag2));

        bool hasMatch = true;
        foreach (var sprite2 in tag2items)
            foreach (var sprite1 in tag1items)
                if (!sprite1.leftbottom.Equals(sprite2.leftbottom)
                    && !sprite1.size.Equals(sprite2.size)) hasMatch = false;

        return hasMatch;
    }
}
