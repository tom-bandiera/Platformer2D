using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonSlimeAnimation : MonoBehaviour
{
    private Animator animator;
    [SerializeField] PoisonSlime poisonSlime;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {

    }
}
