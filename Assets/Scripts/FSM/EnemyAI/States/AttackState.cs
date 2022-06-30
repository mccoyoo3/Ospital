using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    protected D_AttackState stateData;

    protected bool isPlayerDetected;

    public AttackState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, D_AttackState stateData) : base(etity, stateMachine, animBoolName)
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
}
