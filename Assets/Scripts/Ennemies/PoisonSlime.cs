using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class PoisonSlime : Core
{
    public PatrolState patrolState;

    // Start is called before the first frame update
    void Start()
    {
        SetupInstances();
        Set(patrolState);
    }

    // Update is called once per frame
    void Update()
    {

        if (state.isComplete)
        {

        }

        state.DoBranch();


    }

    private void FixedUpdate()
    {
        state.FixedDoBranch();
    }
}
