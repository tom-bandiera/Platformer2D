using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State
{
    public IdleState idleState;
    public NavigateState navigateState;

    public override void Enter()
    {
        GoToNextDestination();
    }

    public override void Do()
    {
        if (machine.state == navigateState)
        {
            if (navigateState.isComplete)
            {
                Set(idleState, true);
            }
        } else {
            if (machine.state == idleState && machine.state.time > 1)
            {
                GoToNextDestination();
            }
        }
    }

    public void GoToNextDestination()
    {
        PickRandomDirection();
        Set(navigateState, true);
    }

    public void PickRandomDirection()
    {
        int randomNumber = Random.Range(-1, 2);

        navigateState.directionX = Mathf.Sign(randomNumber);
    }
}
