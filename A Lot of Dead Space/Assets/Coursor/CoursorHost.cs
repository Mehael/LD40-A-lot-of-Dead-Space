using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoursorHost : MonoBehaviour {
    public static CoursorHost instance;
    public Coursor coursorPrefab;

    private void Awake()
    {
        instance = this;
    }

    public void SpawnCoursors(CustomisableSprite sprite, Coursor.Reaction reaction
        = Coursor.Reaction.random)
    {
        if (sprite == null) return;

        StartCoroutine(Creator(sprite, reaction));
    }

    private IEnumerator Creator(CustomisableSprite sprite, Coursor.Reaction reaction)
    {
        var i = 0;
        var m = Random.Range(1,  TaskManager.instance.tasksDone / 8f);
        while (i < m)
        {
            i++;
            Instantiate(coursorPrefab, transform)
                .MoveDoDie(sprite, reaction);
            yield return new WaitForSeconds(Random.value);
        }
    }
}
