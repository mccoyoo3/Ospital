using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    protected D_IdleState stateData;

    protected bool isIdleTimeOver;
    protected bool isPlayerDetected;
    protected bool targetRaycast;
    protected float idleTime;

    public IdleState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, D_IdleState stateData) : base(etity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isPlayerDetected = core.CollisionSenses.PlayerTargetRaycast;
    }

    public override void Enter()
    {
        base.Enter();
        isIdleTimeOver = false;
        SetRandomIdleTime();
        core.Movement.SetVelocityZero();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (Time.time >= startTime + idleTime)
        {
            isIdleTimeOver = true;
            core.Movement.IdleBehavior();
        }
    }

    private void SetRandomIdleTime()
    {
        idleTime = Random.Range(stateData.minIdleTime, stateData.maxIdleTime);
    }
}
