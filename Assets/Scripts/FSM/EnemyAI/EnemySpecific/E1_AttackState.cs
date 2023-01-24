using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_AttackState : AttackState
{
    private Enemy1 enemy;

    private protected float distanceCheck;

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
        float _distanceCheck = core.Movement.GetSqrDistXZ(enemy.transform.position, core.CollisionSenses.visibleTargets[0].position);
        distanceCheck = _distanceCheck;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        Debug.Log(distanceCheck);
        if (!isPlayerDetected)
        {
            stateMachine.ChangeState(enemy.searchState);
        }
        else if (distanceCheck > stateData.attackRange && isPlayerDetected)
        {
            stateMachine.ChangeState(enemy.chaseState);
        }
    }
}
