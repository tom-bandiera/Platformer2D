using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class PoisonSlimeOld : MonoBehaviour
{
    [SerializeField] private Rigidbody2D body;

    [Range(0f, 10f)] [SerializeField] private float moveSpeed;
    [Range(0f, 10f)] [SerializeField] private float raycastPlayerDistance; 
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private LayerMask playerMask;

    private RaycastHit2D checkGround;
    private RaycastHit2D checkWall;
    private RaycastHit2D checkPlayer;

    private Vector2 movement;
    private Vector2 raycastGroundDirection;
    private Vector2 raycastWallDirection;
    private Vector2 raycastPlayerDirection;

    private float raycastObstacleDistance = 1f;

    public bool isWalking = true;
    public bool isAttacking = false;

    [SerializeField] 
    private enum WalkableDirection
    {
        Right,
        Left
    }

    [SerializeField] private WalkableDirection WalkDirection;

    [SerializeField] 
    private WalkableDirection _walkDirection
    {
        get { return _walkDirection; }
        set { 
            _walkDirection = value; 
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (WalkDirection == WalkableDirection.Left) 
        {
            raycastGroundDirection = new Vector2(-1, -1);
            raycastWallDirection = -transform.right;
            raycastPlayerDirection = new Vector2(-1, 0);

            moveSpeed = -moveSpeed;
            
            // transform.localScale = new Vector3(-1, 1, 1);

        } else if (WalkDirection == WalkableDirection.Right)
        {
            raycastGroundDirection = new Vector2(1, -1);
            raycastWallDirection = transform.right;
            raycastPlayerDirection = new Vector2(1, 0);

            FlipScale();
        }
    }


    private void HandleMovement()
    {
        // Cast a ray diagonally to detect the ground
        checkGround = Physics2D.Raycast(transform.position, raycastGroundDirection, raycastObstacleDistance, groundMask);
        // Cast a ray diagonally to detect the wall
        checkWall = Physics2D.Raycast(transform.position, raycastWallDirection, raycastObstacleDistance, groundMask);
        // Cast a ray diagonally to detect the wall
        checkPlayer = Physics2D.Raycast(transform.position, raycastPlayerDirection, raycastPlayerDistance, playerMask);

        Debug.DrawRay(transform.position, raycastGroundDirection, Color.blue);
        Debug.DrawRay(transform.position, raycastWallDirection, Color.blue);
        Debug.DrawRay(transform.position, raycastPlayerDirection, Color.red);

        // If the ray hits no ground or If the ray hits the wall, flip direction
        if (checkGround.collider == null || checkWall.collider != null)
        {
            Flip();
        }

        if (checkPlayer.collider != null)
        {
            isWalking = false;
            movement = new Vector2(0, body.velocity.y);
        }

        if (checkPlayer.collider != null)
        {
            // Stop the enemy
            movement = new Vector2(0, body.velocity.y);
            isWalking = false;
            isAttacking = true;
        } else
        {
            // Move the enemy
            movement = new Vector2(moveSpeed, body.velocity.y);
            isWalking = true;
            isAttacking = false;
        }

        body.velocity = movement;
    }

    private void Flip()
    {
        if (WalkDirection == WalkableDirection.Left)
        {
            // Flip WalkDirection
            WalkDirection = WalkableDirection.Right;
            // Reverse moveSpeed direction
            moveSpeed = Mathf.Abs(moveSpeed);
            // Reverse Raycast directions
            raycastGroundDirection = new Vector2(1, -1);
            raycastWallDirection = transform.right;
            raycastPlayerDirection = new Vector2(1, 0);

            // transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            // Flip WalkDirection
            WalkDirection = WalkableDirection.Left;
            // Reverse moveSpeed direction
            moveSpeed = -moveSpeed;
            // Reverse Raycast directions
            raycastGroundDirection = new Vector2(-1, -1);
            raycastWallDirection = -transform.right;
            raycastPlayerDirection = new Vector2(-1, 0);

            // transform.localScale = new Vector3(1, 1, 1);
        }

        FlipScale();
    }

    private void FlipScale()
    {
        Vector3 scale = transform.localScale;

        // Flip the scale on the x-axis
        scale.x *= -1;

        // Apply the new scale to the object
        transform.localScale = scale;
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    public bool IsAttacking()
    {
        return isAttacking;
    }
}
