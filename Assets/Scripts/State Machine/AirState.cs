using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class AirState : State
{
    public float jumpSpeed;

    [SerializeField] private AnimationClip animationClip;
    [SerializeField] AudioSource jumpSound;

    public override void Enter()
    {
        animator.Play(animationClip.name);
        jumpSound.Play();
    }

    public override void Do()
    {
        float time = Helpers.Map(body.velocity.y, jumpSpeed, -jumpSpeed, 0, 1, true);
        animator.Play(animationClip.name, 0, time);
        // body.velocity = new Vector2(body.velocity.x, jumpSpeed);

        if (input.isGrounded)
        {
            isComplete = true;
        }
    }

    public override void FixedDo()
    {

    }

    public override void Exit()
    {

    }
}
