using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public HealthManager healthManager;

    [SerializeField] private LayerMask playerMask;
    [SerializeField] private LayerMask enemiesMask;

    // The duration to ignore collisions (in seconds)
    [SerializeField] private float invicibilityDuration = 3f;

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
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemies")) {
            healthManager.takeDamage(1);
            StartCoroutine(IgnoreEnemiesCollisions());
        }
    }

    IEnumerator IgnoreEnemiesCollisions()
    {
        // Ignore collisions with the enemies layer
        Physics2D.IgnoreLayerCollision(gameObject.layer, enemiesMask, true);

        // Wait for the specified duration
        yield return new WaitForSeconds(invicibilityDuration);

        // Revert the collision ignoring
        Physics2D.IgnoreLayerCollision(gameObject.layer, enemiesMask, false);
    }
}
