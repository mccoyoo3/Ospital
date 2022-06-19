using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

public class CollisionSenses : CoreComponent
{
    #region Check Transforms

    /*public Transform GroundCheck
    {
        get => GenericNotImplementedError<Transform>.TryGet(groundCheck, core.transform.parent);
        private set => groundCheck = value;
    }
    public Transform PlayerTarget
    {
        get => GenericNotImplementedError<Transform>.TryGet(target, core.transform.parent);
        private set => target = value;
    }*/

   // public float GroundCheckRadius { get => groundCheckRadius; set => groundCheckRadius = value; }
    public LayerMask WhatIsGround { get => whatIsGround; set => whatIsGround = value; }
    public LayerMask WhatIsPlayer { get => whatIsPlayer; set => whatIsPlayer = value; }

    //[SerializeField] private Transform groundCheck;

    //[SerializeField] private float groundCheckRadius;
    public Vector3 targetPosition { get; set; }

    [SerializeField] private LayerMask whatIsGround;

    [SerializeField] private LayerMask whatIsPlayer;

    [SerializeField] private float viewRadius;

    [Range(0,360)]
    [SerializeField] private float viewAngle;
    public float distanceFromTarget { get; set; }

    public float lookAtSpeed = 0.5f;



    public List<Transform> visibleTargets = new List<Transform>();

    #endregion

    /*public bool Ground
    {
        get => Physics2D.OverlapCircle(GroundCheck.position, groundCheckRadius, whatIsGround);
    }*/

    public bool PlayerTargetRaycast
    {
        get => Convert.ToBoolean(FindTargets());
    }

    public int FindTargets()
    {
        Collider[] PlayerTargets = new Collider[1];
        int findTargets = Physics.OverlapSphereNonAlloc(transform.position, viewRadius, PlayerTargets, whatIsPlayer);
        visibleTargets.Clear();
        for (int i = 0; i < findTargets; i++)
        {
            Transform target = PlayerTargets[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float disToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, dirToTarget, disToTarget, WhatIsGround))
                {
                    visibleTargets.Add(target);
                    distanceFromTarget = Vector3.Distance(transform.position, target.position);
                    targetPosition = target.position;
                    //Debug.Log(distanceFromTarget);
                    return 1;
                }
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
        Gizmos.color = Color.black;
        //Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        Handles.DrawWireArc(transform.position, Vector3.up, Vector3.forward, 360, viewRadius);
        Vector3 viewAngleA = DirFromAngle(-viewAngle /2, false);
        Vector3 viewAngleB = DirFromAngle(viewAngle / 2, false);

        Gizmos.DrawLine(transform.position, transform.position + viewAngleA * viewRadius);
        Gizmos.DrawLine(transform.position, transform.position + viewAngleB * viewRadius);
    }
}
