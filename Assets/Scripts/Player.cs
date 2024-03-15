using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR;

public class Player : MonoBehaviour
{
    public Rigidbody2D body;
    public BoxCollider2D groundCheck;
    [Range(0f, 1f)]
    [SerializeField] private float groundDecay;
    [SerializeField] private float groundSpeed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float acceleration;
    
    [SerializeField] private bool IsGrounded;
    [SerializeField] private LayerMask groundMask;

    private float xInput;
    private float yInput;

    void Start()
    {
        Debug.Log("Start");
    }

    private void Update()
    {
        GetInput();
        HandlePlayerMovement();
        HandleJump();
    }

    void FixedUpdate()
    {
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
        IsGrounded = Physics2D.OverlapAreaAll(groundCheck.bounds.min, groundCheck.bounds.max, groundMask).Length > 0;
    }

    private void ApplyFriction()
    {
        if (IsGrounded &&  xInput == 0 && body.velocity.y <= 0)
        {
            body.velocity *= groundDecay;
        }
    }

    private void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && IsGrounded)
        {
            body.velocity = new Vector2(body.velocity.x, jumpSpeed);
        }
    }
}
