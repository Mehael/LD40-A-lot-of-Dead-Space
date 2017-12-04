using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Board : MonoBehaviour {
    public static Board instance;
    public RectTransform Background;
    public static List<CustomisableSprite> sprites =
        new List<CustomisableSprite>();

    public int Width = 10;
    public int Height = 10;
    public Transform LinePrefub;

    int _width;
    int _height;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        RedrawBoard();
    }

    void Update () {
        if (_width != Width || _height != Height)
            RedrawBoard();
	}

    private void RedrawBoard()
    {
        if (Background != null)
        {
            var padding = 10;
            Background.sizeDelta = new Vector2(Width * CanvasScript.scaleFactor + padding*2,
                Height * CanvasScript.scaleFactor + padding * 2);

            Background.localPosition = new Vector3(-padding, -padding);
        }

        //kill lines
        var lines = new List<Transform>();
        for (int i = 0; i < transform.childCount; i++)
            lines.Add(transform.GetChild(i));

        foreach (var line in lines)
            DestroyImmediate(line.gameObject);

        //Draw new
        var angle = Quaternion.Euler(0, 0, 90);
        for (int x = 0; x <= Width; x++)
            Instantiate(LinePrefub, new Vector3(x, 0, 0) + transform.position, angle, transform);

        for (int y = 0; y <= Height; y++)
            Instantiate(LinePrefub, new Vector3(0, y, 0) + transform.position, Quaternion.identity, transform);

        _width = Width;
        _height = Height;
    }

    internal Vector2 GetClosestSnap(Vector3 vector3)
    {
        var localCoords = vector3 - transform.localPosition;

        var snappedCoords = new Vector3(
            Mathf.RoundToInt(localCoords.x) + transform.localPosition.x,
            Mathf.RoundToInt(localCoords.y) + transform.localPosition.y,
            0);

        return snappedCoords;
    }

    internal Vector2 GetCoords(Vector3 position)
    {
        return new Vector2(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y));
    }

    public static float Intersection(CustomisableSprite item, CustomisableSprite sprite)
    {
        var a = item;
        var b = sprite;
        var xIntersection = 0f;
        var yIntersection = 0f;
        if (a.leftbottom.x > b.leftbottom.x)
        {
            var c = a;
            a = b;
            b = c;
        }

        if (a.righttop.x > b.leftbottom.x)
        {
            var lowerVal = (a.leftbottom.x > b.leftbottom.x) ? a.leftbottom.x : b.leftbottom.x;
            var highterVal = (a.righttop.x > b.righttop.x) ? b.righttop.x : a.righttop.x;

            xIntersection = highterVal - lowerVal;
        } 
        else return 0;

        if (a.leftbottom.y > b.leftbottom.y)
        {
            var d = a;
            a = b;
            b = d;
        }

        if (a.righttop.y > b.leftbottom.y)
        {
            var lowerVal = (a.leftbottom.y > b.leftbottom.y) ? a.leftbottom.y : b.leftbottom.y;
            var highterVal = (a.righttop.y > b.righttop.y) ? b.righttop.y : a.righttop.y;

            yIntersection = highterVal - lowerVal;
        }

        return xIntersection * yIntersection;
    }
}
