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
    }

    public void UpdateAppealing()
    {
        appealingPoints = 0;
        var taggedItems = Board.sprites.Where(s => s.Tag == Tag).ToList();
        if (taggedItems.Count == 0)
            appealingPoints = RequiredAppealingPoints;

        foreach (var item in taggedItems)
            foreach (var sprite in Board.sprites.Where(s => s.Tag == "Appealing"))
                appealingPoints += Board.Intersection(item, sprite);
    }

    float appealingPoints = 0;
    public override bool IsCompleted()
    {
        UpdateAppealing();

        if (LessMode)
            return appealingPoints < RequiredAppealingPoints;
        else
            return appealingPoints >= RequiredAppealingPoints;
    }
}
