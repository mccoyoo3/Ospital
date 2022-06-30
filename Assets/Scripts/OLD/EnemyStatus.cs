using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FSMState.Roland
{
    [System.Serializable]
    public class EnemyStatus
    {
        public List<WaypointsBase> waypoints = new List<WaypointsBase>();
        [HideInInspector]
        public WaypointsBase curWaypoint;

        public float inRange = 5;
        //[HideInInspector]
        public int waypointIndex;
        //[HideInInspector]
        public float waypointDistance;
        //[HideInInspector]
        public float waitTimeWP;
        //[HideInInspector]
        public float maxWaitTime;
        //[HideInInspector]
        public float behaviorLife;
        //[HideInInspector]
        public float maxBehaviorLife;
        //[HideInInspector]
        public float distanceFromTarget;
        //[HideInInspector]
        public Vector3 lastKnownPosition;
        //[HideInInspector]
        public Vector3 canidatePosition;

    }

}
