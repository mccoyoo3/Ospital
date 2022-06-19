using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    protected D_MoveState stateData;

    protected bool isIdle;
    protected bool isPlayerDetected;

    public MoveState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, D_MoveState stateData) : base(etity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        //isGrounded = core.CollisionSenses.Ground;
        isPlayerDetected = core.CollisionSenses.PlayerTargetRaycast;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("move to the set waypoint");
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
    }
}
