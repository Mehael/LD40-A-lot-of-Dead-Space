using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task : MonoBehaviour {
    public string Title;
    public string Description;
    public List<ToolbarItem> unlockedPrefabs;
    public List<Task> disabledTasks;
    public List<Task> enabledTasks;

    public List<GameObject> createdItems;

    public virtual bool IsCompleted()
    {
        return false;
    }

    public virtual void ShowErrorCoursor() { }
}
