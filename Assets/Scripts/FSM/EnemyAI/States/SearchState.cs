using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchState : State
{
    protected D_SearchState stateData;

    protected bool isPlayerDetected;
    protected bool isLockerDetected;
    protected int xState = Animator.StringToHash("xVelocity");

    public SearchState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, D_SearchState stateData) : base(etity, stateMachine, animBoolName)
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
