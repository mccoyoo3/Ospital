using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateManager : MonoBehaviour
{
    public State currentState;
    [HideInInspector]
    public NavMeshAgent agent;
    public Transform playerTarget;
    //[SerializeField]
    private float distanceFromTarget;

    private Vector3 direction;
    private Vector3 rotDirection;

    public bool isInView;
    private bool isInAngle;
    private bool isClear;
    [SerializeField]
    private float radius = 4;
    public float maxDistance = 5;

    public void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RunStateMachine();
        Sight();
    }

    private void RunStateMachine()
    {
        /*State nextState = currentState?.RunCurrentState();
        if(nextState != null)
        {
            //Switch to next State
            SwitchToNextState(nextState);
        }*/
    }

    private void SwitchToNextState(State nextState)
    {
        currentState = nextState;
    }

    #region AI_SightUpdate
    void Sight()
    {
        DistanceCheckPlayer(playerTarget);
        FindDirection(playerTarget);
        AngleCheck();
        if (isInAngle)
        {
            Debug.Log("Is In Angle");
            IsClearView(playerTarget);
        }
    }

    void DistanceCheckPlayer(Transform target)
    {
        distanceFromTarget = Vector3.Distance(transform.position, target.position);
        if (distanceFromTarget > maxDistance || playerTarget.gameObject.activeInHierarchy)
        {
            isInView = false;
        }
    }

    void IsClearView(Transform target)
    {

        isClear = false;
        RaycastHit hit;
        Vector3 origin = transform.position;
        Debug.DrawRay(origin, direction * radius);
        if(distanceFromTarget < maxDistance)
        {
            Debug.Log("In Radius");
            if (Physics.Raycast(origin, direction, out hit, maxDistance) && isInAngle)
            {
                if (hit.transform.CompareTag("Player"))
                {
                    Debug.Log(hit.transform.name);
                    isInView = true;
                    isClear = true;
                }
            }
        }
    }

    void FindDirection(Transform target)
    {
        direction = target.position - transform.position;
        rotDirection = direction;
        rotDirection.y = 0;
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
    #endregion
}
