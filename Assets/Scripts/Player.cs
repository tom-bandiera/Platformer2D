using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR;

public class Player : MonoBehaviour
{
    public Rigidbody2D body;
    public BoxCollider2D groundCheck;

    [SerializeField] private float groundSpeed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float drag;
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
            body.velocity = new Vector2(xInput * groundSpeed, body.velocity.y);
        }

        if (Mathf.Abs(yInput) > 0 && IsGrounded)
        {
            body.velocity = new Vector2(body.velocity.x, yInput * jumpSpeed);
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
        if (IsGrounded && xInput == 0)
        {
            body.velocity *= drag;
        }
    }
}
