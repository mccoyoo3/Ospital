using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA.AI
{

    public static class CommonBehaviors
    {
        public static float searchLife = 60;

        public static void PatrolBehavior(EnemyAI2 ai)
        {
            EnemyStats e = ai.enStats;

            if (e.waypoints.Count > 0)
            {
                if (e.curWaypoint.targetDestination == null)
                {
                    e.curWaypoint = e.waypoints[e.waypointIndex];
                    ai.MoveToPosition(e.curWaypoint.targetDestination.position);
                }

                if (ai.agent.velocity == Vector3.zero)
                    ai.agent.isStopped = false;

                e.curWaypoint = e.waypoints[e.waypointIndex];
                e.waypointDistance = Vector3.Distance(ai.transform.position, e.curWaypoint.targetDestination.position);

                if (e.waypointDistance < 2)
                {
                    e.waitTimeWP += Time.deltaTime * 15;

                    if (e.waitTimeWP > e.maxWaitTime)
                    {
                        e.waitTimeWP = 0;


                        if (e.waypointIndex < e.waypoints.Count - 1)
                        {
                            e.waypointIndex++;
                        }
                        else
                        {
                            e.waypointIndex = 0;
                            //e.waypoints.Reverse();
                        }
                        e.curWaypoint = e.waypoints[e.waypointIndex];
                        e.maxWaitTime = e.curWaypoint.waitTime;
                        ai.MoveToPosition(e.curWaypoint.targetDestination.position);


                    }
                }
            }
        }

        public static void SearchBehavior(EnemyAI2 ai)
        {
            EnemyStats e = ai.enStats;

            float distanceCheck = Vector3.Distance(ai.transform.position, ai.enStats.canidatePosition);

            if(distanceCheck < 2)
            {
                e.waitTimeWP += Time.deltaTime * 15;

                if (e.waitTimeWP > e.maxWaitTime)
                {
                    e.waitTimeWP = 0;
                    e.maxWaitTime = Random.Range(3, 6);
                    e.canidatePosition = ai.RandomVector3AroundPosition(e.lastKnownPosition);
                    ai.MoveToPosition(ai.enStats.canidatePosition);
                }
            }
        }

        public static void ChaseBehavior(EnemyAI2 ai)
        {
            EnemyStats e = ai.enStats;

            float distanceCheck = Vector3.Distance(ai.transform.position, ai.enStats.lastKnownPosition);
            
            if(distanceCheck < e.inRange)
            {
                ai.StopMoving();
            }
            else
            {
                ai.MoveToPosition(e.lastKnownPosition);
            }
        }
    }

    [System.Serializable]
    public class WaypointsBase
    {
        public Transform targetDestination;
        public float waitTime = 1;
    }

}
