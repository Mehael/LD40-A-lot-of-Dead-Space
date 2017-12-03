using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameGizmo : MonoBehaviour {
    public float speedScale = 1f;
    Image image;
    float maxAlpha;
    private void Awake()
    {
        image = GetComponent<Image>();
        maxAlpha = image.color.a;
        CanvasScript.inGameGizmos.Add(this);
    }

    private void OnDestroy()
    {
        image = GetComponent<Image>();
        CanvasScript.inGameGizmos.Remove(this);
    }

    float alphaVelocity = 0f;
    private void Update()
    {
        if (alphaVelocity == 0f) return;

        image.color = new Color(image.color.r, 
            image.color.g,
            image.color.b,
            Mathf.Clamp(image.color.a+alphaVelocity *Time.deltaTime, 0, maxAlpha));

        if (image.color.a == 0 && alphaVelocity < 0)
            alphaVelocity = 0;
        else if (image.color.a == maxAlpha && alphaVelocity > 0)
            alphaVelocity = 0;
    }

    internal void Show(float speed)
    {
        alphaVelocity = speed * maxAlpha * speedScale;
    }

    internal void Hide(float speed)
    {
        alphaVelocity = -speed * maxAlpha * speedScale;
    }
}
