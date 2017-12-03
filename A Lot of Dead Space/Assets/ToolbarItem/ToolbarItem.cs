using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ToolbarItem : MonoBehaviour {
    public Color highlited;
    Image image;
    public CustomisableSprite spawnedItem;
    public Vector2 spawnedSize = Vector2.one;

    CustomisableSprite item;
    private void Awake()
    {
        image = GetComponent<Image>();
        image.sprite = spawnedItem.GetComponent<Image>().sprite;
    }

    public void OnPointerEnter()
    {
        image.color = highlited;
    }

    public void OnPointerExit()
    {
        image.color = Color.white;
    }

    public void LateUpdate()
    {
        if (!isDragged) return;

        if (Input.GetMouseButtonUp(0))
        {
            isDragged = false;
            item.movingAnchor.OnEndDrag(null);
        }
        else
            item.movingAnchor.OnDrag(null);
    }

    bool isDragged = false;
    public void OnPointerDown()
    {
        item = Instantiate(spawnedItem, transform.position, Quaternion.identity, image.canvas.transform);
        item.movingAnchor.OnPointerDown(null);
        item.movingAnchor.SetSize(spawnedSize);
        isDragged = true;
    }
}
