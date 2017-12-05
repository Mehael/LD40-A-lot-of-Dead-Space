using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coursor : MonoBehaviour {
    public float moveSpeed = 1f;
    public enum Reaction {tap, angry, happy, random}

    Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();

        var x = Random.Range(-1.5f, -0.5f);
        if (Random.value > 0.5f)
            x += Board.instance.Width + 1f;

        transform.position = new Vector2(x,
            Random.Range(-1, Board.instance.Height + 1));
    }

    float padding = 0.3f;
    public void MoveDoDie(CustomisableSprite targetRect, Reaction reaction)
    {
        var target = new Vector2(Random.Range(targetRect.leftbottom.x + padding, targetRect.righttop.x - padding),
            Random.Range(targetRect.leftbottom.y + padding, targetRect.righttop.y - padding));

        target += new Vector2(0,-0.5f);
        StartCoroutine(MoveAnimate(target, reaction));
    }

    public void Coins(string param)
    {
        Music.instance.MoneyPlay();
    }

    IEnumerator MoveAnimate(Vector2 target, Reaction reaction)
    {
        yield return new WaitForSeconds(2 * Random.value);

        var startPos = transform.position;
        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime * moveSpeed;
            transform.position =  Vector3.Lerp(startPos, target, t);
            yield return null;
        }

        if (reaction == Reaction.random)
            reaction = (Reaction)Random.Range(0, 2);

        if (reaction == Reaction.angry) anim.SetTrigger("Sad");
        else if (reaction == Reaction.happy) anim.SetTrigger("Happy");
        else if (reaction == Reaction.tap) anim.SetTrigger("Tapping");
    }

    public void Kill(string param) { Destroy(gameObject);  }
}
