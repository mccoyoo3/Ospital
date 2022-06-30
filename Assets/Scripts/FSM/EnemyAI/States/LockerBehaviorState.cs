using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockerBehaviorState : State
{
    protected D_LockerBehaviorState stateData;

    protected bool isPlayerDetected;
    protected bool isLockerDetected;

    public LockerBehaviorState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, D_LockerBehaviorState stateData) : base(etity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isPlayerDetected = core.CollisionSenses.PlayerTargetRaycast;
        isLockerDetected = core.CollisionSenses.LockerDoorRaycast;
    }

    public override void Enter()
    {
        base.Enter();
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
