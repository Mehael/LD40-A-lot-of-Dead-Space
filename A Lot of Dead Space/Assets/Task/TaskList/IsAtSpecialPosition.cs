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
        foreach (var sprite in Board.sprites.Where(s => s.Tag == Tag))
            if ((EnabledArea == Direction.bottom && sprite.leftbottom.y < AmountOfLegalLines)
                || (EnabledArea == Direction.top && Board.instance.Height - sprite.righttop.y < AmountOfLegalLines)
                || (EnabledArea == Direction.left && sprite.leftbottom.x < AmountOfLegalLines)
                || (EnabledArea == Direction.right && Board.instance.Width - sprite.righttop.x < AmountOfLegalLines))
                return true;

        return false;
    }
}
