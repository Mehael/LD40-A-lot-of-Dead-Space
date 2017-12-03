using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsLightlyOverlapped : Task
{
    public string Tag = "";
    public float LegalPercent = 50f;

    public override bool IsCompleted()
    {
        return true;
    }
}

