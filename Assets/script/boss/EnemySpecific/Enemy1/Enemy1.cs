using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Entity
{
   public E1_IdleState idleState {get; private set;}
   public E1_MoveState moveState {get; private set;}

   [SerializeField] private IdleState_Data idleStateData;
   [SerializeField] private MoveState_Data moveStateData;


    public override void Start()
    {
        base.Start();

        moveState = new E1_MoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new E1_IdleState(this, stateMachine, "idle", idleStateData, this);

        stateMachine.Initialize(idleState);
    }

}
