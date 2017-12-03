using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CheckAppealing : Task {
    public string Tag = "PlayButton";
    public int RequiredAppealingPoints = 1;

    float appealingPoints = 0;
    public override bool IsCompleted()
    {
        appealingPoints = 0;
        var taggedItems = Board.sprites.Where(s => s.Tag == Tag).ToList();
        if (taggedItems.Count == 0) return true;

        foreach (var item in taggedItems)
            foreach (var sprite in Board.sprites.Where(s => s.Tag == "Appealing"))
                appealingPoints += Board.Intersection(item, sprite);

        return appealingPoints >= RequiredAppealingPoints;
    }
}
