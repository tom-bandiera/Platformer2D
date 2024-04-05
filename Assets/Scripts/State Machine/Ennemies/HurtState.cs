using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class HurtState : State
{
    [SerializeField] private AnimationClip animationClip;
    [SerializeField] private PoisonSlime poisonSlime;

    public override void Enter()
    {
        body.velocity = new Vector2 (0, body.velocity.y);

        StartCoroutine(GetHurtAnimation());
    }

    public override void Do()
    {

    }

    IEnumerator GetHurtAnimation()
    {
        animator.Play(animationClip.name);

        // Wait for the specified duration
        yield return new WaitForSeconds(1);

        if (poisonSlime.health == 0)
        {
            Destroy(poisonSlime.gameObject);
        }
    }
}
