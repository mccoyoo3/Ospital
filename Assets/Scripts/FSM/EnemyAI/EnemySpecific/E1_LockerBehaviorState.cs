using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class E1_LockerBehaviorState : LockerBehaviorState
{
    private Enemy1 enemy;

    public E1_LockerBehaviorState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, D_LockerBehaviorState stateData, Enemy1 enemy) : base(etity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        core.Movement.RotateTowardsLocker();
        core.Movement.SetVelocityZero();
        core.CollisionSenses.lockerTargets[core.CollisionSenses.randomLocker]
            .gameObject.GetComponentInParent<Animator>().SetBool("opening", true);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (Time.time >= startTime + stateData.inLockerTime)
        {
            stateMachine.ChangeState(enemy.searchState);
        }
        else if(isPlayerDetected)
        {
            Debug.Log("INSTANT DEATH");
        }
    }
}
