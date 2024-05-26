using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    protected IdleState_Data stateData;
    protected bool isIdleTimeOver;
    protected bool moveAfterIdle;
    protected float idleTime;
    public IdleState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, IdleState_Data stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }
    
    public override void Enter()
    {
        base.Enter();
        entity.SetVelocity(0f);
        isIdleTimeOver = false;
        setRandomIdleTime();
        
    }

    public override void Exit()
    {
        base.Exit();
        if(moveAfterIdle){
            entity.ChangeMove();
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(Time.time>= startTime + idleTime)
        {
            isIdleTimeOver=true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void setMoveAfterIdle(bool move){
        moveAfterIdle = move;
        Debug.Log("changeMove");
    }

    private void setRandomIdleTime()
    {
        idleTime = Random.Range(stateData.maxIdleTime, stateData.maxIdleTime);
    }


}
