using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonSlimeAnimation : MonoBehaviour
{
    private Animator animator;
    private const string IS_WALKING = "IsWalking";
    private const string IS_ATTACKING = "IsAttacking";
    [SerializeField] PoisonSlime poisonSlime;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetBool(IS_WALKING, poisonSlime.IsWalking());
        animator.SetBool(IS_ATTACKING, poisonSlime.IsAttacking());
    }
}
