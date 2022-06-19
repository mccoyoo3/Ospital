using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class E1_SearchState : SearchState
{
    private Enemy1 enemy;

    public E1_SearchState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, D_SearchState stateData, Enemy1 enemy) : base(etity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("searching for player");
        core.Movement.MoveToPosition(core.WaypointsBase.lastKnownPosition);
        core.WaypointsBase.candidatePosition = core.WaypointsBase.lastKnownPosition;
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
        if (Time.time >= startTime + stateData.searchTimeDelay && !isPlayerDetected)
        {
            SearchBehavior();
            if(Time.time >= startTime + stateData.searchTime)
            {
                stateMachine.ChangeState(enemy.idleState);
            }
        }
        else if (isPlayerDetected)
        {
            stateMachine.ChangeState(enemy.playerDetectedState);
        }
    }

    private void SearchBehavior()
    {
        float distanceCheck = Vector3.Distance(enemy.transform.position, core.WaypointsBase.candidatePosition);
        if (distanceCheck < 1)
        {
            core.WaypointsBase.waitTimeWP += Time.deltaTime;
            core.WaypointsBase.maxWaitTime = Random.Range(3, 6);
            if (core.WaypointsBase.waitTimeWP > core.WaypointsBase.maxWaitTime)
            {
                core.WaypointsBase.waitTimeWP = 0;
                core.WaypointsBase.candidatePosition = core.Movement.RandomVector3AroundPosition(core.WaypointsBase.lastKnownPosition);
                core.Movement.MoveToPosition(core.WaypointsBase.candidatePosition);
            }
        }
    }

}
