using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParralax : MonoBehaviour {
    public Vector3 shift;

    Vector3 startPos;
    private void Awake()
    {
        startPos = transform.position;
    }

    void LateUpdate () {
        transform.position = startPos +
            (Input.mousePosition.x / Screen.width - 0.5f) * shift; 		
	}
}
