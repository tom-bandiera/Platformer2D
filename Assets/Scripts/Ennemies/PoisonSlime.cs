using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class PoisonSlime : MonoBehaviour
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

    public bool IsWalking = true;

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
            
            transform.localScale = new Vector3(-1, 1, 1);

        } else if (WalkDirection == WalkableDirection.Right)
        {
            raycastGroundDirection = new Vector2(1, -1);
            raycastWallDirection = transform.right;
            raycastPlayerDirection = new Vector2(1, 0);

            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
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
            FlipDirection();
        }

        if (checkPlayer.collider != null)
        {
            IsWalking = false;
            movement = new Vector2(0, body.velocity.y);
        }

        if (checkPlayer.collider != null)
        {
            // Stop the enemy
            movement = new Vector2(0, body.velocity.y);
            IsWalking = false;
        } else
        {
            // Move the enemy
            movement = new Vector2(moveSpeed, body.velocity.y);
            IsWalking = true;
        }

        body.velocity = movement;
    }

    private void FlipDirection()
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

            transform.localScale = new Vector3(-1, 1, 1);
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

            transform.localScale = new Vector3(1, 1, 1);
        }
        // Rotate Ennemy to 180°
        transform.Rotate(0f, 180f, 0f);
    }
}
