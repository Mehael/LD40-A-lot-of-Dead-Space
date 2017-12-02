using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Board : MonoBehaviour {
    public static Board instance;

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
        var lines = new List<Transform>();
        for (int i = 0; i < transform.childCount; i++)
            lines.Add(transform.GetChild(i));

        foreach (var line in lines)
            DestroyImmediate(line.gameObject);

        //Draw new
        var angle = Quaternion.Euler(0, 0, 90);
        for (int x = 0; x <= Width; x++)
            Instantiate(LinePrefub, new Vector3(x,0,0) + transform.position, angle, transform);

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
}
