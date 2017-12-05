using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoardSizeChange : Task {
    public int NewWidth;
    public int NewHeight;
    public Vector3 CameraShift;

    public override void OnEnabled()
    {
        Board.instance.Width = NewWidth;
        Board.instance.Height = NewHeight;

        StartCoroutine(MoveCamera(CameraShift, 2f));

        foreach (var s in Board.sprites)
            if (s.righttop.x > NewWidth || s.righttop.y > NewHeight)
                Destroy(s.gameObject);

        base.OnEnabled();
    }

    IEnumerator MoveCamera(Vector3 sgift, float speed)
    {
        var startPos = Camera.main.transform.position;
        var endPos = startPos + CameraShift;
        var t = 0f;

        while (t < 1)
        {
            t += Time.deltaTime * speed;
            Camera.main.transform.position =
                Vector3.Lerp(startPos, endPos, t);

            yield return null;
        }
    }

    public override bool IsCompleted()
    {
        return true;
    }
}
