using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : State
{
    [SerializeField] private AnimationClip animationClip;
    public PlayerInput input;


    public override void Enter()
    {
        animator.Play(animationClip.name);
    }

    public override void Do()
    {
        animator.speed = Helpers.Map(input.groundSpeed, 0, 1, 0, 1.6f, true);

        if (!groundSensor.grounded)
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
