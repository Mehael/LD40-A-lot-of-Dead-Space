using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardIsCreated : Task {
    public override bool IsCompleted()
    {
        return Board.instance.Background.GetComponent<Mask>().showMaskGraphic;
    }
}
