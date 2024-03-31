using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    [SerializeField] private AnimationClip animationClip;

    public override void Enter()
    {
        animator.Play(animationClip.name);
    }

    public override void Do()
    {
        if(!input.isGrounded)
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
