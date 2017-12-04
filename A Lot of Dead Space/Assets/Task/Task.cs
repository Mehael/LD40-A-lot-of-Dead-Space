using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task : MonoBehaviour {
    public string Title;
    public string Description;
    public List<ToolbarItem> unlockedTools;
    public List<Task> disabledTasks;
    public List<Task> enabledTasks;

    public CustomisableSprite InterestingSprite;
    public Coursor.Reaction reaction = Coursor.Reaction.random;

    public virtual bool IsCompleted()
    {
        return true;
    }
    public virtual void OnEnabled() { }
    public virtual void ShowErrorCoursor() { }
}
