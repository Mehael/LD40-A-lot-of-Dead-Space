using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toolbar : MonoBehaviour {
    public static Toolbar instance;

    private void Awake()
    {
        instance = this;
    }

    public Dictionary<string, ToolbarItem> activeTools 
        = new Dictionary<string, ToolbarItem>();

    public void AddTool(ToolbarItem newTool)
    {
        if (activeTools.ContainsKey(newTool.tag))
            return;

        activeTools.Add(newTool.tag, Instantiate(newTool, transform));
    }

    public void RemoveTool(string toolTag)
    {
        Destroy(activeTools[toolTag].gameObject);
        activeTools.Remove(toolTag);
    }
}
