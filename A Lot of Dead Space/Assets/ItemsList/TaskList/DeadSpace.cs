using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadSpace : Task
{
    public int LegalAmountOfEmptyCells = 2;
    public bool FewMode = false;

    public override bool IsCompleted()
    {
        return true;
    }
}
