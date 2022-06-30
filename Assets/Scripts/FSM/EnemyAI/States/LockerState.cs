using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockerState : State
{
    protected D_LockerState stateData;

    protected bool isPlayerDetected;
    protected bool isLockerDetected;

    protected int xLockerState = Animator.StringToHash("lockerXVelocity");

    public LockerState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, D_LockerState stateData) : base(etity, stateMachine, animBoolName)
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
