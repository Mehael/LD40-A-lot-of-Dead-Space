using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasScript : MonoBehaviour {
    public static float scaleFactor = 37f;

    public static List<InGameGizmo> inGameGizmos =
        new List<InGameGizmo>();

    public float timeBeforeHidingGUI = 2f;
    public float hidingSpeed = 0.8f;
    public float showingSpeed = 2f;
    public float mouseEpsilon = 1.5f;

    float timer = 0;
    Vector3 lastMousePosition = Vector3.zero;
    bool isHidden = false;

    private void Update()
    {
        var newMousePosition = Input.mousePosition;
        if ((lastMousePosition - newMousePosition).magnitude > mouseEpsilon ||
            Input.GetMouseButtonDown(0))
        {
            timer = 0;
            isHidden = false;
            foreach (var gizmo in inGameGizmos)
                gizmo.Show(showingSpeed);
        }
        else if (!isHidden)
        {
            if (timer >= timeBeforeHidingGUI)
            {
                isHidden = true;
                foreach (var gizmo in inGameGizmos)
                    gizmo.Hide(hidingSpeed);
            }
            else if (!Input.GetMouseButton(0))
                timer += Time.deltaTime;
        }

        lastMousePosition = newMousePosition;
    }
}
