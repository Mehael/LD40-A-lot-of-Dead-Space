using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadSpace : Task
{
    public int LegalAmountOfEmptyCells = 2;
    public bool FewMode = false;
    public Animator mainScene;
    public string Trigger;

    bool[,] grid;

    public override void OnEnabled()
    {
        mainScene.SetTrigger("DeadSpace");
    }

    public override bool IsCompleted()
    {
        grid = new bool[Board.instance.Width, Board.instance.Height];

        foreach (var s in Board.sprites)
        {
            int ix = (int)s.leftbottom.x;
            int iy = (int)s.leftbottom.y;

            for (var x = 0; x < s.size.x - 1; x++)
                for (var y = 0; y < s.size.y - 1; y++)
                {
                    grid[ix + x, iy + y] = true;
                }
        }
        var freeCells = 0;
        for (var x = 0; x < Board.instance.Width; x++)
            for (var y = 0; y < Board.instance.Height; y++)
                if (!grid[x, y])
                    freeCells++;

        if (FewMode)
            return freeCells >= LegalAmountOfEmptyCells;
        else
            return freeCells <= LegalAmountOfEmptyCells;
    }
}
