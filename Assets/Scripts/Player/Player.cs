using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR;

public class Player : MonoBehaviour
{
    public Rigidbody2D body;
    public BoxCollider2D groundCheck;

    [SerializeField] AudioSource jumpSound;

    [Range(0f, 1f)]
    [SerializeField] private float groundDecay;
    [SerializeField] private float groundSpeed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float acceleration;
    
    [SerializeField] private LayerMask groundMask;

    private float xInput;
    private float yInput;

    private bool isWalking;
    private bool isGrounded;
    private bool isJumping;
    private bool isFalling;
    private Vector3 previousPosition;

    private void Update()
    {
        GetInput();
        HandlePlayerMovement();
        HandleJump();
    }

    void FixedUpdate()
    {
        isFalling = transform.position.y < previousPosition.y;
        previousPosition = transform.position;

        CheckGround();
        ApplyFriction();
    }

    private void HandlePlayerMovement()
    {
        if (Mathf.Abs(xInput) > 0)
        {
            float increment = xInput * acceleration;
            float newSpeed = Mathf.Clamp(body.velocity.x + increment, -groundSpeed, groundSpeed);
            body.velocity = new Vector2(xInput * groundSpeed, body.velocity.y);

            isWalking = Mathf.Abs(body.velocity.x) > 0.3f;

            // Face direction we are moving on X
            float direction = Mathf.Sign(xInput);
            transform.localScale = new Vector3(direction, 1, 1);
        }
    }

    private void GetInput()
    {
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");
    }

    private void CheckGround()
    {
        if (isFalling == false) return;

        isGrounded = Physics2D.OverlapAreaAll(groundCheck.bounds.min, groundCheck.bounds.max, groundMask).Length > 0;

        if (isGrounded)
        {
            isJumping = false;
        }
    }

    private void ApplyFriction()
    {
        if (isGrounded && xInput == 0 && body.velocity.y <= 0)
        {
            body.velocity *= groundDecay;
        }
    }

    private void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jumpSound.Play();
            body.velocity = new Vector2(body.velocity.x, jumpSpeed);

            isJumping = true;
            isFalling = false;
            isGrounded = false;
        }
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    public bool IsGrounded()
    {
        return isGrounded;
    }

    public bool IsJumping()
    {
        return isJumping;
    }

    public bool IsFalling()
    {
        return isFalling;
    }
}
