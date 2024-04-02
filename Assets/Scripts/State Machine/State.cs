using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    public bool isComplete { get; protected set; }

    protected float startTime;
    public float time => Time.time - startTime;

    protected Core core;

    protected Rigidbody2D body => core.body;
    protected Animator animator => core.animator;
    protected GroundSensor groundSensor => core.groundSensor;
    /*protected PlayerInput input => core.input;*/

    public virtual void Enter()
    {

    }

    public virtual void Do()
    {

    }

    public virtual void FixedDo()
    {

    }

    public virtual void Exit()
    {

    }

    public void Initialize()
    {
        isComplete = false;
        startTime = Time.time;
    }

    public void SetCore(Core _core)
    {
        core = _core; 
    }
}
