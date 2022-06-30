using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class ChaseState : State
{
    protected D_ChaseState stateData;

    protected bool isIdle;
    protected bool isChasing;

    public ChaseState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, D_ChaseState stateData) : base(etity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isChasing = core.CollisionSenses.PlayerTargetRaycast;
        if(core.CollisionSenses.visibleTargets.Any())
        {
            core.Movement.RotateTowardsTarget();
            core.Movement.Chase();
        }
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
