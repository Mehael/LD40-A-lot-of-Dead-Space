using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsAtSpecialPosition : Task {
    public enum Direction { left , right, top, bottom};
    public Direction EnabledArea;
    public int AmountOfLegalLines = 1;

    public override bool IsCompleted()
    {
        return true;
    }
}
