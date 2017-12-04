using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartOfTheGame : Task {
    public Animator mainScene;
    public string Trigger;

    public override void OnEnabled()
    {
        TaskManager.instance.View.Done.interactable = true;
    }

    public override void OnCompleted()
    {
        if (Trigger == "Stop")
            TaskManager.instance.View.Done.gameObject.SetActive(false);
        if (Trigger == "Play")
            TaskManager.instance.View.DoneText.text = "TO TESTS";
            mainScene.SetTrigger(Trigger);
    }
}
