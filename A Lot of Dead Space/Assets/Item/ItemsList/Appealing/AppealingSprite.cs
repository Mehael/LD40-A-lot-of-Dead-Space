using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppealingSprite : CustomisableSprite {
    public ParticleSystem particles;

	public override void UpdateRectCoords(Vector2 leftbottom, Vector2 size)
    {
        base.UpdateRectCoords(leftbottom, size);
        particles.transform.localScale = new Vector3(size.x * CanvasScript.scaleFactor, 
            size.y * CanvasScript.scaleFactor, 1);
    }
}
