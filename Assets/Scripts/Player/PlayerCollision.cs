using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public HealthManager healthManager;
    public Animator animator;
    // The duration to ignore collisions (in seconds)
    [SerializeField] private float invicibilityDuration = 3f;

    private bool invincible = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemies") && invincible == false) {
            healthManager.takeDamage(1);
            invincible = true;
            animator.SetLayerWeight(1, 1);
            StartCoroutine(IgnoreEnemiesCollisions());
        }
    }

    IEnumerator IgnoreEnemiesCollisions()
    {
        // Ignore collisions with the enemies layer
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemies"));
        
        // Wait for the specified duration
        yield return new WaitForSeconds(3);

        animator.SetLayerWeight(1, 0);
        // Revert the collision ignoring
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemies"), false);

        invincible = false;
    }
}
