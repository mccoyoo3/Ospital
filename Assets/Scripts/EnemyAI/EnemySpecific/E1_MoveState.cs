using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_MoveState : MoveState
{
    private Enemy1 enemy;

    public E1_MoveState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, D_MoveState stateData, Enemy1 enemy) : base(etity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        core.Movement.agent.speed = stateData.movementSpeed;
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
        core.Movement.PatrolBehavior();
        if (core.WaypointsBase.waypointDistance < core.WaypointsBase.WaypointInRange)
        {
            stateMachine.ChangeState(enemy.idleState);
        }
        else if (isPlayerDetected)
        {
            stateMachine.ChangeState(enemy.playerDetectedState);
        }
    }
}
