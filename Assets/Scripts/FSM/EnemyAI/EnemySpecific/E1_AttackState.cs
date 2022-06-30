using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_AttackState : AttackState
{
    private Enemy1 enemy;

    public E1_AttackState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, D_AttackState stateData, Enemy1 enemy) : base(etity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
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
        float distanceCheck = core.Movement.GetSqrDistXZ(enemy.transform.position, core.WaypointsBase.lastKnownPosition);
        if (!isPlayerDetected)
        {
            stateMachine.ChangeState(enemy.idleState);
        }
        else if (distanceCheck > stateData.attackRange && isPlayerDetected)
        {
            stateMachine.ChangeState(enemy.chaseState);
        }
    }
}
