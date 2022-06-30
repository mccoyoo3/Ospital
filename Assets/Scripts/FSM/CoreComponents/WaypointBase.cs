using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WaypointBase : CoreComponent
{
    public List<EnemyWaypoints> waypoints = new List<EnemyWaypoints>();
    [HideInInspector] public EnemyWaypoints curWaypoint;

    public float WaypointInRange = 0.1f;
    public float LockerInRange = 1f;
    public int waypointIndex { get; set; }
    public float waypointDistance { get; set; }
    public float waitTimeWP { get; set; }
    public float maxWaitTime { get; set; }
    public Vector3 lastKnownPosition { get; set; }
    public Vector3 candidatePosition { get; set; }

    protected override void Awake()
    {
        base.Awake();
    }
}

[System.Serializable]
public class EnemyWaypoints
{
    public Transform targetDestination;
    public float waitTime { get; set; }
}