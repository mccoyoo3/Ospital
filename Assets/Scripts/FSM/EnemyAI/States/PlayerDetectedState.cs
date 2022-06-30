using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerDetectedState : State
{
    protected D_PlayerDetectedState stateData;

    protected bool isIdle;
    protected bool ischaseDelayTimeOver;
    protected bool isPlayerDetected;
    protected float chaseDelayTime;

    public PlayerDetectedState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetectedState stateData) : base(etity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isPlayerDetected = core.CollisionSenses.PlayerTargetRaycast;
        core.WaypointsBase.lastKnownPosition = core.CollisionSenses.targetPosition;
        if (core.CollisionSenses.visibleTargets.Any())
        {
            core.Movement.RotateTowardsTarget();
        }

    }

    public override void Enter()
    {
        base.Enter();
        SetRandomChaseDelayTime();
        ischaseDelayTimeOver = false;
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
    }

    private void SetRandomChaseDelayTime()
    {
        chaseDelayTime = Random.Range(stateData.minchaseDelayTime, stateData.maxchaseDelayTime);
    }
}
