using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigateState : State
{
    public float directionX;

    public float groundSpeed = 2;

    private RaycastHit2D checkGround;
    private RaycastHit2D checkWall;
    private RaycastHit2D checkPlayer;

    [SerializeField] private LayerMask groundMask;
    [SerializeField] private LayerMask playerMask;

    [Range(0f, 10f)][SerializeField] private float raycastObstacleDistance;
    [Range(0f, 10f)][SerializeField] private float raycastPlayerDistance;

    public override void Enter()
    {

    }

    public override void Do()
    {
        // Cast a ray diagonally to detect the ground
        checkGround = Physics2D.Raycast(body.transform.position, new Vector2(directionX, -1), raycastObstacleDistance, groundMask);
        // Cast a ray diagonally to detect the wall
        checkPlayer = Physics2D.Raycast(transform.position, new Vector2(directionX, 0), raycastPlayerDistance, playerMask);

        if (checkGround.collider == null)
        {
            directionX = -directionX;
        }
    }

    public override void FixedDo()
    {
        body.velocity = new Vector2(directionX * groundSpeed, body.velocity.y);
    }

    public void GoToNextDestination()
    {
        PickRandomDirection();
    }

    public void PickRandomDirection()
    {
        int randomNumber = Random.Range(-1, 2);

        directionX = Mathf.Sign(randomNumber);
    }
}
