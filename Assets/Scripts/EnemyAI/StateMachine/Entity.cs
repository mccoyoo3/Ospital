using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public FiniteStateMachine stateMachine;

    public D_Entity entityData;

    public Animator anim { get; private set; }
    public AnimationToStatemachine atsm { get; private set; }
    public int lastDamageDirection { get; private set; }
    public Core Core { get; private set; }

    [SerializeField]
    private Transform wallCheck;
    [SerializeField]
    private Transform playerCheck;
    [SerializeField]
    private Transform groundCheck;

    protected bool isStunned;
    protected bool isDead;

    public virtual void Awake()
    {
        Core = GetComponentInChildren<Core>();
        anim = GetComponent<Animator>();
        atsm = GetComponent<AnimationToStatemachine>();

        stateMachine = new FiniteStateMachine();
    }

    public virtual void Start()
    {
        Core.WaypointsBase.waitTimeWP = Core.WaypointsBase.curWaypoint.waitTime;
    }

    public virtual void Update()
    {
        Core.LogicUpdate();
        stateMachine.currentState.LogicUpdate();
    }

    public virtual void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }

    public virtual void OnDrawGizmos()
    {
        if (Core != null)
        {
            //Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.closeRangeActionDistance), 0.2f);
            //Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.minAgroDistance), 0.2f);
            //Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.maxAgroDistance), 0.2f);
        }
    }
}
