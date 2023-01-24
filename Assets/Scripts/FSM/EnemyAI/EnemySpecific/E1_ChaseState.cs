using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class E1_ChaseState : ChaseState
{
    private Enemy1 enemy;

    public E1_ChaseState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, D_ChaseState stateData, Enemy1 enemy) : base(etity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        core.Movement.agent.speed = stateData.chaseSpeed;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(!core.CollisionSenses.visibleTargets.Any())
        {
            stateMachine.ChangeState(enemy.searchState);
        }
        else if(core.CollisionSenses.distanceFromTarget < stateData.attackDistance)
        {
            stateMachine.ChangeState(enemy.attackState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
