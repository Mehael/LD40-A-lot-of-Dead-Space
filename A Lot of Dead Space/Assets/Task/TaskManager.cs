﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TaskManager : MonoBehaviour {
    public static TaskManager instance;

    public Transform TasksNode;
    public TaskViewer View;
    public Task FirstTask;

    List<Task> tasks = new List<Task>();
    List<Task> activeTasks = new List<Task>();
    Task currentMainTask;
	void Awake () {
        instance = this;
        for (var i = 0; i < TasksNode.childCount; i++)
            tasks.Add(TasksNode.GetChild(i).GetComponent<Task>());        
    }

    private void Start()
    {
        ActivateTask(FirstTask);
    }

    public void OnIdle()
    {
        var taskList = activeTasks.Where(t => t.InterestingSprite != null).ToList();
        if (taskList.Count == 0) return;

        var rTask = taskList[Random.Range(0, taskList.Count)];
        var reaction = Coursor.Reaction.angry;
        if (rTask.IsCompleted())
            reaction = (Random.value > 0.5f) ? Coursor.Reaction.happy : Coursor.Reaction.tap;

        CoursorHost.instance.SpawnCoursors(rTask
            .InterestingSprite, reaction);
    }

    public void UpdateTaskProgression()
    {
        if (currentMainTask == null) return;

        var sideTasks = "";
        foreach (var task in activeTasks)
            if (currentMainTask != task && !task.IsCompleted())
                if (sideTasks == "")
                    sideTasks = task.Description;
                else
                    sideTasks += "\n\n" + task.Description;

        if (sideTasks != "")
            View.Description.text = sideTasks;
        else
            View.Description.text = currentMainTask.Description;

        View.Done.interactable = currentMainTask.IsCompleted() 
            && sideTasks == "";
    }

    public void Done()
    {
        CoursorHost.instance.SpawnCoursors(currentMainTask.InterestingSprite, 
            Coursor.Reaction.tap);

        currentMainTask.OnCompleted();
        DisableTask(currentMainTask);

        var MainTaskBefore = currentMainTask;
        foreach (var item in currentMainTask.enabledTasks)
            ActivateTask(item);

        if (MainTaskBefore == currentMainTask)
            return;

        if (currentMainTask.IsCompleted() 
            && currentMainTask.tag != "GameController")
            Done();
    }

    void ActivateTask(Task task)
    {
        if (task == null) return;

        if (task.Title != "")
        {
            currentMainTask = task;

            View.Label.text = task.Title;
            View.Description.text = task.Description;
            View.Done.interactable = false;
        }
        task.OnEnabled();

        foreach (var item in task.unlockedTools)
            Toolbar.instance.AddTool(item);

        activeTasks.Add(task);
    }

    void DisableTask(Task task)
    {
        if (activeTasks.Contains(task))
            activeTasks.Remove(task);

        foreach (var subtask in task.disabledTasks)
            DisableTask(subtask);

        foreach (var item in task.unlockedTools)
            SafeDestroy(item); 
    }

    void SafeDestroy(ToolbarItem item)
    {
        bool isUsed = false;
        foreach (var t in activeTasks)
            foreach (var i in t.unlockedTools)
                if (i.tag == item.tag)
                    isUsed = true;

        if (!isUsed)
            Toolbar.instance.RemoveTool(item.tag);
    }
}
