using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public FiniteStateMachine stateMachine;
    public EntityData entityData;
    public int facingDirection {get; private set;}
    public Rigidbody2D rb {get; private set;}
    public Animator anim { get; private set; }

    [SerializeField] private Transform wallCheck;
    //[SerializeField] private Transform ledgeCheck;

    private Vector2 velocityWorkSpace;

    public virtual void Start()
    {
        facingDirection = 1;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        stateMachine = new FiniteStateMachine();
    }

    public virtual void Update()
    {
        stateMachine.currentState.LogicUpdate();   
    }

    public virtual void FixedUpdate() 
    {
        stateMachine.currentState.PhysicsUpdate();
    }

    public virtual void SetVelocity(float velocity)
    {
        velocityWorkSpace.Set(facingDirection * velocity, rb.velocity.y);
        rb.velocity = velocityWorkSpace;
    }

    public virtual bool CheckWall()
    {
        Debug.Log("checkwall");
        return Physics2D.OverlapCircle(wallCheck.position,entityData.wallCheckDistance, entityData.whatGround);
         //return Physics2D.Raycast(wallCheck.position, transform.right, entityData.wallCheckDistance, entityData.whatGround);
    }

    public virtual void CheckLedge()
    {

    }

    public virtual void ChangeMove()
    {
        facingDirection *= -1;
    }

    public virtual void OnDrawGizmos() {
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right * facingDirection *entityData.wallCheckDistance));
    }


}
