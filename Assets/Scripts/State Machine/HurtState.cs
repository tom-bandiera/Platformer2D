using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class HurtState : State
{
    [SerializeField] private AnimationClip animationClip;
    [SerializeField] private AudioSource stompSound;

    // MOVE THIS OUT, TOO SPECIFIC FOR STATE
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
        stompSound.Play();

        
        yield return new WaitForSeconds(1);


        // MOVE THIS OUT
        if (poisonSlime.health == 0)
        {
            Destroy(poisonSlime.gameObject);
        }
    }
}
