using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class IsAtSpecialPosition : Task {
    public enum Direction { left , right, top, bottom};
    public string Tag = "";
    public Direction EnabledArea;
    public int AmountOfLegalLines = 1;

    public override bool IsCompleted()
    {
        var items = Board.sprites.Where(s => s.Tag == Tag);
        if (items.Count() > 0)
            InterestingSprite = items.First();

        foreach (var sprite in items)
            if ((EnabledArea == Direction.bottom && sprite.leftbottom.y < AmountOfLegalLines)
                || (EnabledArea == Direction.top && Board.instance.Height - sprite.righttop.y < AmountOfLegalLines)
                || (EnabledArea == Direction.left && sprite.leftbottom.x < AmountOfLegalLines)
                || (EnabledArea == Direction.right && Board.instance.Width - sprite.righttop.x < AmountOfLegalLines))
                return true;

        return false;
    }
}
