using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadSpace : Task
{
    public int LegalAmountOfEmptyCells = 2;
    public bool FewMode = false;

    bool[,] grid;
    public override void OnEnabled()
    {
        grid = new bool[Board.instance.Width, Board.instance.Height];
    }

    public override bool IsCompleted()
    {
        foreach (var s in Board.sprites)
            for (var x = 0; x < s.size.x; x++)
                for (var y = 0; y < s.size.y; y++)
                    grid[x, y] = true;

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
