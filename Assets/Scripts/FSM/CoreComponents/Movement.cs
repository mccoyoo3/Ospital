using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement: CoreComponent
{
    public Rigidbody RB { get; private set; }

    public NavMeshAgent agent { get; private set; }

    public bool CanSetVelocity { get; set; }

    public Vector3 CurrentVelocity { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        RB = GetComponentInParent<Rigidbody>();
        agent = GetComponentInParent<NavMeshAgent>();
    }

    public void LogicUpdate()
    {
        CurrentVelocity = RB.velocity;
    }

    #region Set Functions

    public void MoveToPosition(Vector3 targetPosition)
    {
        agent.isStopped = false;
        agent.SetDestination(targetPosition);
    }

    public void RotateTowardsTarget()
    {
        Vector3 lTargetDir = core.CollisionSenses.visibleTargets[0].position - transform.parent.parent.transform.position;
        lTargetDir.y = 0.0f;
        transform.parent.parent.rotation = Quaternion.RotateTowards(transform.parent.parent.transform.rotation, Quaternion.LookRotation(lTargetDir), Time.time * core.CollisionSenses.lookAtSpeed);
    }

    public void Chase()
    {
        MoveToPosition(core.CollisionSenses.visibleTargets[0].position);
    }

    public void PatrolBehavior()
    {
        if (core.WaypointsBase.waypoints.Count > 0)
        {
            if (core.WaypointsBase.curWaypoint.targetDestination == null)
            {
                core.WaypointsBase.curWaypoint = core.WaypointsBase.waypoints[core.WaypointsBase.waypointIndex];
                core.Movement.MoveToPosition(core.WaypointsBase.curWaypoint.targetDestination.position);
            }

            core.WaypointsBase.curWaypoint = core.WaypointsBase.waypoints[core.WaypointsBase.waypointIndex];
            core.WaypointsBase.waypointDistance = Vector3.Distance(core.Movement.transform.position, core.WaypointsBase.curWaypoint.targetDestination.position);
        }
        core.Movement.MoveToPosition(core.WaypointsBase.curWaypoint.targetDestination.position);
    }

    public void IdleBehavior()
    {
        core.WaypointsBase.waitTimeWP = 0;

        if (core.WaypointsBase.waypointIndex < core.WaypointsBase.waypoints.Count - 1)
        {
            core.WaypointsBase.waypointIndex++;
        }
        else
        {
            core.WaypointsBase.waypointIndex = 0;
            core.WaypointsBase.waypoints.Reverse();
        }
        core.WaypointsBase.curWaypoint = core.WaypointsBase.waypoints[core.WaypointsBase.waypointIndex];
        core.WaypointsBase.maxWaitTime = core.WaypointsBase.curWaypoint.waitTime;
    }

    public Vector3 RandomVector3AroundPosition(Vector3 targetPosition)
    {
        float offsetX = Random.Range(-10, 10);
        float offsetZ = Random.Range(-10, 10);
        Vector3 originPos = targetPosition;
        originPos.x += offsetX;
        originPos.z += offsetZ;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(originPos, out hit, 5, NavMesh.AllAreas))
        {
            return hit.position;
        }

        return targetPosition;
    }

    public void SetVelocityZero()
    {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
    }


    public void SetVelocityToDefault()
    {
        agent.isStopped = false;
    }

    #endregion
}
