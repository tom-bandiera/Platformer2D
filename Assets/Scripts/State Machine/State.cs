using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    public StateMachine machine;
    public StateMachine parent;
    public State state => machine.state;

    // Wrapping the StateMachine Set() function for shortcut
    protected void Set(State newState, bool forceReset = false)
    {
        machine.Set(newState, forceReset);
    }

    public void SetCore(Core _core)
    {
        machine = new StateMachine();
        core = _core;
    }
    public void Initialize(StateMachine _parent)
    {
        parent = _parent;
        isComplete = false;
        startTime = Time.time;
    }

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


    public void DoBranch()
    {
        Do();
        state?.DoBranch();
    }

    public void FixedDoBranch()
    {
        FixedDo();
        state?.FixedDoBranch();
    }
}
