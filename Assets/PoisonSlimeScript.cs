using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonSlimeScript : MonoBehaviour
{

    [SerializeField] private Rigidbody2D body;
    [SerializeField] private float moveSpeed;
    [SerializeField] private LayerMask groundMask;

    public float raycastDistance = 1f;
    public Vector2 raycastDirection = new Vector2(1, -1); // Diagonal ray direction


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        // Cast a ray diagonally to detect the ground
        RaycastHit2D hit = Physics2D.Raycast(transform.position, raycastDirection, raycastDistance, groundMask);

        // Move the enemy
        Vector2 movement = new Vector2(moveSpeed, body.velocity.y);
        body.velocity = movement;

        Debug.Log(hit.collider);

        // If the ray hits the ground, keep moving
        if (hit.collider == null)
        {
            if (moveSpeed > 0)
            {
                moveSpeed = -moveSpeed;
                raycastDirection = new Vector2(-1, -1);
            } else
            {
                moveSpeed = Mathf.Abs(moveSpeed);
                raycastDirection = new Vector2(1, -1);
            }

            transform.Rotate(0f, 180f, 0f);
        }
    }
}
