using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class E1_LockerState : LockerState
{
    private Enemy1 enemy;


    public E1_LockerState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, D_LockerState stateData, Enemy1 enemy) : base(etity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        core.CollisionSenses.randomLocker = Random.Range(0, core.CollisionSenses.lockerTargets.Count);
        core.Movement.MoveToLocker();
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
        float distanceCheck = core.Movement.GetSqrDistXZ(enemy.transform.position, core.CollisionSenses.lockerTargets[core.CollisionSenses.randomLocker].transform.position);
        SearchBlendTreeAnimation();
        if (core.Movement.agent.isStopped && Time.time >= startTime + stateData.lockerSearchTime)
        {
           stateMachine.ChangeState(enemy.searchState);
        }
        else if (distanceCheck < core.WaypointsBase.LockerInRange)
        {
            stateMachine.ChangeState(enemy.lockerBehaviorState);
        }
    }

    private void SearchBlendTreeAnimation()
    {
        float distanceCheck = core.Movement.GetSqrDistXZ(enemy.transform.position,
            core.CollisionSenses.lockerTargets[core.CollisionSenses.randomLocker].transform.position);
        if (distanceCheck > core.WaypointsBase.LockerInRange)
            enemy.Anim.SetFloat(xLockerState, 1f, 0.25f, 1f);
        else
            enemy.Anim.SetFloat(xLockerState, 0.0f, 0.25f, 1f);
    }
}
