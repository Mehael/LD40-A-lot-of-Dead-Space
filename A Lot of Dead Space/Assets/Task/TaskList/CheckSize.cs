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

        float nowSize = GetCurrentSize();

        reallyRequestedSize = nowSize + RequiredSize;
    }

    private float GetCurrentSize()
    {
        var nowSprite = new List<CustomisableSprite>();
        if (Tag == "Offer")
            nowSprite = Board.sprites.Where(s => s.Tag.StartsWith(Tag)).ToList();
        else
            nowSprite = Board.sprites.Where(s => s.Tag == Tag).ToList();

        if (nowSprite.Count() > 0)
            InterestingSprite = nowSprite.First();

        var nowSize = 0f;
        foreach (var s in nowSprite)
            nowSize += s.size.x * s.size.y;
        return nowSize;
    }

    public override bool IsCompleted()
    {
        var size = GetCurrentSize();

        if (LessMode)
            return size < reallyRequestedSize;
        else
            return size >= reallyRequestedSize;
    }
}
