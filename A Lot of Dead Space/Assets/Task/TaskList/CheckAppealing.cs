using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CheckAppealing : Task {
    public string Tag = "PlayButton";
    public int RequiredAppealingPoints = 1;
    public bool FlexibleRequirement = false;
    public bool LessMode = false;

    float reallyRequestedAppeal;
    public override void OnEnabled()
    {
        if (!FlexibleRequirement)
        {
            reallyRequestedAppeal = RequiredAppealingPoints;
            return;
        }

        UpdateAppealing();

        reallyRequestedAppeal = appealingPoints + RequiredAppealingPoints;
        if (reallyRequestedAppeal < 0)
            reallyRequestedAppeal = 0;
    }

    bool isFailed = false;
    public void UpdateAppealing()
    {
        isFailed = false;
        appealingPoints = 0;
        List<CustomisableSprite> taggedItems;
        if (Tag == "Offer")
           taggedItems = Board.sprites.Where(s => s.Tag.StartsWith(Tag)).ToList();
        else
            taggedItems = Board.sprites.Where(s => s.Tag == Tag).ToList();

        if (taggedItems.Count == 0)
            isFailed = true;

        foreach (var item in taggedItems)
            foreach (var sprite in Board.sprites.Where(s => s.Tag == "Appealing"))
                appealingPoints += Board.Intersection(item, sprite);
    }

    float appealingPoints = 0;
    public override bool IsCompleted()
    {
        UpdateAppealing();
        if (isFailed)
            return false;

        if (LessMode)
            return appealingPoints <= reallyRequestedAppeal;
        else
            return appealingPoints >= reallyRequestedAppeal;
    }
}
