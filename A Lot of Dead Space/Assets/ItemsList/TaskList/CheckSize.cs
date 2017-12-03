﻿using System.Collections;
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
        var nowSprite = Board.sprites.Where(s => s.Tag == Tag).First();
        var nowSize = 0f;
        if (nowSprite != null)
            nowSize = nowSprite.size.x * nowSprite.size.y;

        reallyRequestedSize = nowSize + RequiredSize;
    }

    public override bool IsCompleted()
    {
        var size = 0f;
        foreach (var sprite in Board.sprites.Where(s => s.Tag == Tag))
            size += sprite.size.x * sprite.size.y;

        if (LessMode)
            return size < reallyRequestedSize;
        else
            return size >= reallyRequestedSize;
    }
}
