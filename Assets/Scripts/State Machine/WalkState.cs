using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : State
{
    public override void Enter()
    {
        animator.Play("Walk");
    }

    public override void Do()
    {
        if (input.xInput == 0)
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
