﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour {
    public static TaskManager instance;

    public Transform TasksNode;
    public Transform Toolbar;
    public TaskViewer View;
    public Task FirstTask;

    List<Task> tasks = new List<Task>();
    List<Task> activeTasks = new List<Task>();
    Task currentMainTask;
	void Awake () {
        instance = this;
        for (var i = 0; i < TasksNode.childCount; i++)
            tasks.Add(TasksNode.GetChild(i).GetComponent<Task>());

        ActivateTask(FirstTask);
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
                    sideTasks += task.Description + "\n";

        if (sideTasks != "")
            View.Description.text = sideTasks;
        else
            View.Description.text = currentMainTask.Description;

        View.Done.enabled = currentMainTask.IsCompleted() 
            && sideTasks == "";
    }

    public void Done()
    {
        DisableTask(currentMainTask);

        foreach (var item in currentMainTask.enabledTasks)
            ActivateTask(item);
    }

    void ActivateTask(Task task)
    {
        if (task == null) return;

        if (task.Title != "")
        {
            currentMainTask = task;

            View.Label.text = task.Title;
            View.Description.text = task.Description;
            View.Done.enabled = false;
        }

        foreach (var item in task.unlockedPrefabs) {
            var ToolbarItem = Instantiate(item, Toolbar);
            task.createdItems.Add(ToolbarItem.gameObject);
        }

        activeTasks.Add(task);
    }

    void DisableTask(Task task)
    {
        foreach (var item in task.createdItems)
            if (item != null)
                Destroy(item);

        activeTasks.Remove(task);
    }
}
