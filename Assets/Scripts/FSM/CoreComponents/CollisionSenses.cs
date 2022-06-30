using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEditor;

public class CollisionSenses : CoreComponent
{
    #region Check Transforms
    public LayerMask WhatIsGround { get => whatIsGround; set => whatIsGround = value; }
    public LayerMask WhatIsPlayer { get => whatIsPlayer; set => whatIsPlayer = value; }
    public float distanceFromTarget { get; set; }
    public Vector3 targetPosition { get; set; }
    public int randomLocker { get; set; }

    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private LayerMask whatIsLockers;
    [SerializeField] private float viewRadius;
    [Range(0,360)]
    [SerializeField] private float viewAngle;

    public float lookAtSpeed = 0.5f;
    public List<Transform> visibleTargets = new List<Transform>();
    public List<Transform> lockerTargets = new List<Transform>();

    #endregion

    public bool PlayerTargetRaycast
    {
        get => Convert.ToBoolean(FindTargets());
    }

    public bool LockerDoorRaycast
    {
        get => Convert.ToBoolean(FindLockers());
    }

    public int FindTargets()
    {
        visibleTargets.Clear();
        Collider[] PlayerTargets = new Collider[1];
        int findTargets = Physics.OverlapSphereNonAlloc(transform.parent.position, viewRadius, PlayerTargets, whatIsPlayer);
        for (int i = 0; i < findTargets; i++)
        {
            Transform target = PlayerTargets[i].transform;
            Vector3 dirToTarget = (target.position - transform.parent.position).normalized;
            if (Vector3.Angle(transform.parent.forward, dirToTarget) < viewAngle / 2)
            {
                float disToTarget = Vector3.Distance(transform.parent.position, target.position);
                if (!Physics.Raycast(transform.parent.position, dirToTarget, disToTarget, WhatIsGround))
                {
                    visibleTargets.Add(target);
                    distanceFromTarget = core.Movement.GetSqrDistXZ(transform.parent.position, target.position);
                    core.WaypointsBase.candidatePosition = target.position;
                    targetPosition = target.position;
                    return 1;
                }
            }
        }
        return 0;
    }

    public int FindLockers()
    {
        Collider[] LockerTargets = new Collider[6];
        int findTargets = Physics.OverlapSphereNonAlloc(transform.parent.position, viewRadius, LockerTargets, whatIsLockers);
        for (int i = 0; i < findTargets; i++)
        {
            Transform target = LockerTargets[i].transform;
            Vector3 dirToTarget = (target.position - transform.parent.position).normalized;
            if (Vector3.Angle(transform.parent.forward, dirToTarget) < viewAngle / 2)
            {
                if (lockerTargets.IndexOf(target) == -1)
                {
                    lockerTargets.Add(target);
                }
                targetPosition = target.position;
            }
        }
        return 0;
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad),0,Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Handles.DrawWireArc(transform.position, Vector3.up, Vector3.forward, 360, viewRadius);
        Vector3 viewAngleA = DirFromAngle(-viewAngle /2, false);
        Vector3 viewAngleB = DirFromAngle(viewAngle / 2, false);
        Gizmos.DrawLine(transform.position, transform.position + viewAngleA * viewRadius);
        Gizmos.DrawLine(transform.position, transform.position + viewAngleB * viewRadius);
    }
}
