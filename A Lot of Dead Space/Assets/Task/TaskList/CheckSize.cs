using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CheckSize : Task {
    public string Tag = "none";
    public int RequiredSize = 1;
    public bool FlexibleRequirement = false;
    public bool LessMode = false;

    float reallyRequestedSize;
    public override void OnEnabled()
    {
        if (!FlexibleRequirement)
        {
            reallyRequestedSize = RequiredSize;
            return;
        }

        var nowSprite = new List<CustomisableSprite>();
        if (Tag == "Offer")
            nowSprite = Board.sprites.Where(s => s.Tag.StartsWith(Tag)).ToList();
        else
            nowSprite = Board.sprites.Where(s => s.Tag == Tag).ToList();

        var nowSize = 0f;
        foreach (var s in nowSprite)
            nowSize += s.size.x * s.size.y;

        reallyRequestedSize = nowSize + RequiredSize;
    }

    public override bool IsCompleted()
    {
        var size = 0f;
        
        if (Tag == "Offer")
            foreach (var sprite in Board.sprites.Where(s => s.Tag.StartsWith(Tag)))
                size += sprite.size.x * sprite.size.y;
        else
            foreach (var sprite in Board.sprites.Where(s => s.Tag == Tag))
                size += sprite.size.x * sprite.size.y;

        if (LessMode)
            return size < reallyRequestedSize;
        else
            return size >= reallyRequestedSize;
    }
}
