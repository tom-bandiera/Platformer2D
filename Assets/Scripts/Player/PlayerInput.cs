using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR;

public class PlayerInput : Core
{
    [Range(0f, 1f)]
    [SerializeField] private float groundDecay;
    
    [SerializeField] private float acceleration;
    
    public AirState airState;
    public IdleState idleState;
    public WalkState walkState;



    public float xInput { get; private set; }
    public float yInput { get; private set; }

    private void Start()
    {
        SetupInstances();
        machine.Set(idleState);
    }

    private void Update()
    {
        GetInput();
        HandlePlayerMovement();
        HandleJump();
        
        SelectState();
        
        machine.state.Do();
    }

    void FixedUpdate()
    {
        ApplyFriction();
    }

    void SelectState()
    {
        if (groundSensor.grounded)
        {
            if (xInput == 0)
            {
                machine.Set(idleState);
            } else {
                machine.Set(walkState);
            }
        } else {
            machine.Set(airState);
        }
    }

    private void HandlePlayerMovement()
    {
        if (Mathf.Abs(xInput) > 0)
        {
            float increment = xInput * acceleration;
            float newSpeed = groundSensor.grounded ? Mathf.Clamp(body.velocity.x + increment, -walkState.maxSpeed, walkState.maxSpeed) : walkState.maxSpeed * xInput;
     
            body.velocity = new Vector2(newSpeed, body.velocity.y);
            
            FaceInput();
        }
    }

    private void GetInput()
    {
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");
    }

    private void FaceInput()
    {
        float direction = Mathf.Sign(xInput);
        transform.localScale = new Vector3(direction, 1, 1);
    }



    private void ApplyFriction()
    {
 
        if (groundSensor.grounded && xInput == 0 && body.velocity.y <= 0.01f)
        {
            body.velocity *= groundDecay;
        }
    }

    private void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && groundSensor.grounded)
        {
            
            body.velocity = new Vector2(body.velocity.x, airState.jumpSpeed);
        }
    }
}
