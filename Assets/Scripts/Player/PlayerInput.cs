using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR;

public class PlayerInput : MonoBehaviour
{
    public Rigidbody2D body;
    public Animator animator;

    public BoxCollider2D groundCheck;

    [Range(0f, 1f)]
    [SerializeField] private float groundDecay;
    
    [SerializeField] private float acceleration;
    
    [SerializeField] private LayerMask groundMask;


    public AirState airState;
    public IdleState idleState;
    public WalkState walkState;

    State state;

    public float groundSpeed;
    public bool isGrounded { get; private set; }
    public float xInput { get; private set; }
    public float yInput { get; private set; }

    private void Start()
    {
        airState.Setup(body, animator, this);
        idleState.Setup(body, animator, this);
        walkState.Setup(body, animator, this);
        state = idleState;
    }

    private void Update()
    {
        GetInput();
        HandlePlayerMovement();
        HandleJump();
        

        SelectState();
        
   
        state.Do();
    }

    void FixedUpdate()
    {
        CheckGround();
        ApplyFriction();
    }

    void SelectState()
    {
        State oldState = state;

        if (isGrounded)
        {
            if (xInput == 0)
            {
                state = idleState;
            } else
            {
                state = walkState;
            }
        }
        else
        {
            state = airState;
        }

        if (oldState != state)
        {
            oldState.Exit();
            state.Initialize();
            state.Enter();
        }

    }

    private void HandlePlayerMovement()
    {
        if (Mathf.Abs(xInput) > 0)
        {
            float increment = xInput * acceleration;
            float newSpeed = isGrounded ? Mathf.Clamp(body.velocity.x + increment, -groundSpeed, groundSpeed) : groundSpeed * xInput;
     
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

    private void CheckGround()
    {
        isGrounded = Physics2D.OverlapAreaAll(groundCheck.bounds.min, groundCheck.bounds.max, groundMask).Length > 0;
    }

    private void ApplyFriction()
    {
 
        if (isGrounded && xInput == 0 && body.velocity.y <= 0.01f)
        {
            body.velocity *= groundDecay;
        }
    }

    private void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {

            body.velocity = new Vector2(body.velocity.x, airState.jumpSpeed);

            isGrounded = false;
        }
    }
}
