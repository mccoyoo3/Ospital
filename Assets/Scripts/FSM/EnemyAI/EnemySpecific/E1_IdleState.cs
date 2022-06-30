using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_IdleState : IdleState
{
    private Enemy1 enemy;

    public E1_IdleState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, D_IdleState stateData, Enemy1 enemy) : base(etity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        core.WaypointsBase.curWaypoint.waitTime = idleTime;
        core.CollisionSenses.lockerTargets.Clear();
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

        if (isIdleTimeOver && !isPlayerDetected)
        {
            stateMachine.ChangeState(enemy.moveState);
        }
        else if (isPlayerDetected)
        {
            stateMachine.ChangeState(enemy.playerDetectedState);
        }
    }
}
