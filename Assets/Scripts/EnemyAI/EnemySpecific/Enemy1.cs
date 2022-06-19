using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Entity
{
    public E1_MoveState moveState { get; private set; }
    public E1_IdleState idleState { get; private set; }
    public E1_PlayerDetectedState playerDetectedState { get; private set; }
    public E1_ChaseState chaseState { get; private set; }
    public E1_AttackState attackState { get; private set; }
    public E1_SearchState searchState { get; private set; }
    public Rigidbody2D RB { get; private set; }

    [SerializeField]
    private D_MoveState moveStateData;
    [SerializeField]
    private D_ChaseState chaseStateData;
    [SerializeField]
    private D_AttackState attackStateData;
    [SerializeField]
    private D_PlayerDetectedState detectedStateData;
    [SerializeField]
    private D_SearchState searchStateData;
    [SerializeField]
    private D_IdleState idleStateData;

    public Core core { get; private set; }
    public bool isInAngle { get; private set; }
    [SerializeField]
    private Transform meleeAttackPosition;

    public override void Awake()
    {
        base.Awake();

        moveState = new E1_MoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new E1_IdleState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState = new E1_PlayerDetectedState(this, stateMachine, "detected", detectedStateData, this);
        chaseState = new E1_ChaseState(this, stateMachine, "chase", chaseStateData, this);
        attackState = new E1_AttackState(this, stateMachine, "attack", attackStateData, this);
        searchState = new E1_SearchState(this, stateMachine, "search", searchStateData, this);
    }

    public override void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        stateMachine.Initialize(idleState);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }
}
