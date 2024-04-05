using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class PoisonSlime : Core
{
    public PatrolState patrolState;
    public HurtState hurtState;

    public int health { get; private set; }
    private int lastHealth;

    private void Awake()
    {
        health = 1;
        lastHealth = health;
    }

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

        if (lastHealth != health)
        {
            Set(hurtState);
        }

        state.DoBranch();

        lastHealth = health;
    }

    private void FixedUpdate()
    {
        state.FixedDoBranch();
    }

    public void takeDamage(int damage)
    {
        if (health > 0)
        {
            health -= damage;
        }
    }
}
