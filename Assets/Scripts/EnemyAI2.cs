using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace SA.AI
{
    public class EnemyAI2 : MonoBehaviour
    {

        public Transform playerTarget;
        Vector3 direction;
        Vector3 rotDirection;

        bool isInView;
        bool isInAngle;
        bool isClear;
        float radius = 6;
        float maxDistance = 5;

        int lFrame = 15;
        int lFrame_counter = 0;

        int llFrame = 35;
        int llf_counter = 0;

        public AIState aiState;
        public AIState targetState;

        delegate void EveryFrame();
        EveryFrame everyFrame;
        delegate void LateFrame();
        LateFrame lateFrame;
        delegate void LateLateFrame();
        LateLateFrame llateFrame;


        [HideInInspector]
        public NavMeshAgent agent;
        public EnemyStats enStats = new EnemyStats();

        public bool changeSt;


        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            aiState = AIState.idle;
            ChangeState(AIState.idle);
        }

        void Update()
        {
            if (changeSt) // pang Debug pag may problema
            {
                ChangeState(targetState);
                changeSt = false;
            }
            Debug.Log(aiState.ToString());
            MonitorStates();
            if (everyFrame != null)
                everyFrame();

            lFrame_counter++;
            if (lFrame_counter > lFrame)
            {
                if (lateFrame != null)
                    lateFrame();

                lFrame_counter = 0;
            }

            llf_counter++;
            if (llf_counter > llFrame)
            {
                if (llateFrame != null)
                    llateFrame();

                llf_counter = 0;
            }

        }

        void MonitorStates() //Nagrurun bawat Frame
        {
            switch (aiState)
            {
                case AIState.idle:
                    if (enStats.distanceFromTarget < radius)
                        ChangeState(AIState.inRadius);
                    if (enStats.distanceFromTarget > maxDistance)
                        ChangeState(AIState.lateIdle);
                    break;
                case AIState.lateIdle:
                    if (enStats.distanceFromTarget < maxDistance)
                        ChangeState(AIState.idle);
                    break;
                case AIState.inRadius:
                    if (enStats.distanceFromTarget > radius)
                        ChangeState(AIState.idle);
                    if (isClear)
                        ChangeState(AIState.inView);
                    break;
                case AIState.inView:
                    if (enStats.distanceFromTarget > radius)
                        ChangeState(AIState.idle);
                    if (!isClear)
                        ChangeState(AIState.inSearch);
                    break;
                case AIState.inSearch:
                    if (isClear)
                        ChangeState(AIState.inView);
                    MonitorBehaviorLife();
                    break;
                default:
                    break;
            }

        }

        public void MonitorBehaviorLife()
        {
            enStats.behaviorLife += Time.deltaTime;
            if (enStats.behaviorLife > enStats.maxBehaviorLife)
            {
                enStats.behaviorLife = 0;
                ChangeState(targetState);
            }
        }

        public void ChangeState(AIState targetState)
        {

            aiState = targetState;
            everyFrame = null;
            lateFrame = null;
            llateFrame = null;

            switch (targetState)
            {
                case AIState.idle:
                    lateFrame = IdleBehaviours;
                    agent.speed = 0.5f;
                    break;
                case AIState.lateIdle:
                    llateFrame = IdleBehaviours;
                    break;
                case AIState.inRadius:
                    lateFrame = inRadiusBehaviors;
                    break;
                case AIState.inView:
                    lateFrame = InViewBehaviorsSecondary;
                    //llateFrame = MonitorTargetPosition;
                    everyFrame = InViewBehaviors;
                    enStats.lastKnownPosition = playerTarget.position;
                    agent.speed = 2.5f;
                    //StopMoving();
                    //MoveToPosition(enStats.lastKnownPosition);
                    break;
                case AIState.inSearch:
                    lateFrame = InSearchBehaviors;
                    enStats.lastKnownPosition = playerTarget.position;
                    enStats.canidatePosition = enStats.lastKnownPosition;
                    MoveToPosition(enStats.lastKnownPosition);
                    float bhlifeOffset = Random.Range(-2, 3);
                    enStats.maxBehaviorLife = CommonBehaviors.searchLife + bhlifeOffset;
                    targetState = AIState.idle;
                    WaypointsBase wp = new WaypointsBase();
                    enStats.curWaypoint = wp;
                    agent.speed = 1;
                    break;
                default:
                    break;
            }

        }
        void IdleBehaviours()
        {
            if (playerTarget == null)
                return;
            DistanceCheckPlayer(playerTarget);
            CommonBehaviors.PatrolBehavior(this);
        }
        void inRadiusBehaviors()
        {
            if (playerTarget == null)
                return;

            Sight();
            CommonBehaviors.PatrolBehavior(this);

        }
        void InViewBehaviors()
        {
            if (playerTarget == null)
                return;

            FindDirection(playerTarget);
            RotateTowardsTarget();
            CommonBehaviors.ChaseBehavior(this);
        }
        void InViewBehaviorsSecondary()
        {
            Sight();
        }
        void InSearchBehaviors()
        {
            CommonBehaviors.SearchBehavior(this);
            Sight();
        }

        //AI moves

        void Sight()
        {
            
            DistanceCheckPlayer(playerTarget);
            FindDirection(playerTarget);
            AngleCheck();
            if(isInAngle)
                IsClearView(playerTarget);
        }

        void DistanceCheckPlayer(Transform target)
        {
            enStats.distanceFromTarget = Vector3.Distance(transform.position, target.position);
        }

        void IsClearView(Transform target)
        {
            isClear = false;
            RaycastHit hit;
            Vector3 origin = transform.position;
            Debug.DrawRay(origin, direction * radius);
            if (Physics.Raycast(origin, direction, out hit, 5))
            {
                if (hit.transform.CompareTag("Player"))
                {
                    Debug.Log(hit.transform.name);
                    isClear = true;
                }
            }

        }

        void AngleCheck()
        {
            rotDirection = direction;
            rotDirection.y = 0;
            if (rotDirection == Vector3.zero)
                rotDirection = transform.forward;

            float angle = Vector3.Angle(transform.forward, rotDirection);

            isInAngle = (angle < 75);
        }

        void RotateTowardsTarget()
        {
            if (rotDirection == Vector3.zero)
                rotDirection = transform.forward;

            Quaternion targetRotation = Quaternion.LookRotation(rotDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5);
        }

        void FindDirection(Transform target)
        {
            direction = target.position - transform.position;
            rotDirection = direction;
            rotDirection.y = 0;
        }

        void MonitorTargetPosition()
        {
            float delta_distance = Vector3.Distance(playerTarget.position, enStats.lastKnownPosition);
            if (delta_distance > 2)
            {
                enStats.lastKnownPosition = playerTarget.position;
                MoveToPosition(enStats.lastKnownPosition);
            }
        }

        public void MoveToPosition(Vector3 targetPosition)
        {
            agent.isStopped = false;
            agent.SetDestination(targetPosition);
        }

        public void StopMoving()
        {
            agent.isStopped = true;
        }

        public Vector3 RandomVector3AroundPosition(Vector3 targetPosition)
        {
            float offsetX = Random.Range(-10, 10);
            float offsetZ = Random.Range(-10, 10);
            Vector3 originPos = targetPosition;
            originPos.x += offsetX;
            originPos.z += offsetZ;

            NavMeshHit hit;
            if(NavMesh.SamplePosition(originPos, out hit, 5, NavMesh.AllAreas))
            {
                return hit.position;
            }

            return targetPosition;
        }

        public enum AIState
        {
            idle, lateIdle, inRadius, inView, inSearch
        }
    }

}
