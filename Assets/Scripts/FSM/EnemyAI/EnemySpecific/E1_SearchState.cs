using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class E1_SearchState : SearchState
{
    private Enemy1 enemy;

    private float lockerTime;
    private bool lockerDelay;
    private float randomSearchTime;
    private float currentVal;

    public E1_SearchState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, D_SearchState stateData, Enemy1 enemy) : base(etity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        core.Movement.MoveToPosition(core.WaypointsBase.candidatePosition);
        randomSearchTime = Random.Range(stateData.minSearchTime, stateData.maxSearchTime);
        lockerTime = Random.Range(stateData.minSearchTime, stateData.maxSearchTime);
        core.Movement.agent.speed = stateData.searchMoveSpeed;
        lockerDelay = false;
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
        core.Movement.SearchBehavior();
        SearchBlendTreeAnimation();
        lockerTime -= Time.deltaTime;
        if (lockerTime < 0)
            lockerDelay = true;

        if (Time.time >= startTime + randomSearchTime)
        {
            stateMachine.ChangeState(enemy.idleState);
        }
        else
        {
            if (!isPlayerDetected && core.CollisionSenses.lockerTargets.Any() && lockerDelay)
            {
                stateMachine.ChangeState(enemy.lockerState);
            }
            else if (isPlayerDetected)
            {
                stateMachine.ChangeState(enemy.playerDetectedState);
            }
        }
    }

    private void SearchBlendTreeAnimation()
    {
        float distanceCheck = core.Movement.GetSqrDistXZ(enemy.transform.position, core.WaypointsBase.candidatePosition);

        if (distanceCheck > core.WaypointsBase.WaypointInRange)
            enemy.Anim.SetFloat(xState, 1f, 0.25f, 1f);
        else
            enemy.Anim.SetFloat(xState, 0f, 0.15f, 1f);
    }
}
