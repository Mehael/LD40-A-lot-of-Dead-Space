using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasScript : MonoBehaviour {
    public static float scaleFactor = 37f;
    public static RectTransform ItemsNode;
    public RectTransform itemsNode;

    private void Awake()
    {
        ItemsNode = itemsNode;
    }

    public static List<InGameGizmo> inGameGizmos =
        new List<InGameGizmo>();

    public float timeBeforeHidingGUI = 2f;
    public float hidingSpeed = 0.8f;
    public float showingSpeed = 2f;
    public float mouseEpsilon = 1.5f;

    float timerBeforeIdle = 0;
    Vector3 lastMousePosition = Vector3.zero;
    bool isHidden = false;

    float timerInIdle = 0;
    private void Update()
    {
        var newMousePosition = Input.mousePosition;
        if ((lastMousePosition - newMousePosition).magnitude > mouseEpsilon ||
            Input.GetMouseButtonDown(0))
        {
            timerBeforeIdle = 0;
            isHidden = false;
            foreach (var gizmo in inGameGizmos)
                gizmo.Show(showingSpeed);
        }
        else if (!isHidden)
        {
            if (timerBeforeIdle >= timeBeforeHidingGUI)
            {
                isHidden = true;
                foreach (var gizmo in inGameGizmos)
                    gizmo.Hide(hidingSpeed);
            }
            else if (!Input.GetMouseButton(0))
                timerBeforeIdle += Time.deltaTime;
        }
        else
        {
            timerInIdle += Time.deltaTime;
            if (timerInIdle > 4f)
            {
                TaskManager.instance.OnIdle();
                timerInIdle = 0f;
            }
        }

        lastMousePosition = newMousePosition;
    }
}
