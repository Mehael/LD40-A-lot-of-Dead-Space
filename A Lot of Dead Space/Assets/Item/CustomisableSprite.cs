using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomisableSprite : MonoBehaviour {
    public MovingObject movingAnchor;
    public string Tag { get; private set; }

    private void Awake()
    {
        Tag = tag;
        Board.sprites.Add(this);
    }

    public void OnDestroy()
    {
        Board.sprites.Remove(this);
        TaskManager.instance.UpdateTaskProgression();
    }

    public Vector2 leftbottom;
    public Vector2 size;
    public Vector2 righttop;

    public virtual void UpdateRectCoords(Vector2 leftbottom, Vector2 size)
    {
        this.leftbottom = leftbottom;
        this.size = size;
        righttop = leftbottom + size;

        TaskManager.instance.UpdateTaskProgression();
    }
}
