using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    public Movement Movement
    {
        get => GenericNotImplementedError<Movement>.TryGet(movement, transform.parent);
        private set => movement = value;
    }
    public CollisionSenses CollisionSenses
    {
        get => GenericNotImplementedError<CollisionSenses>.TryGet(collisionSenses, transform.parent);
        private set => collisionSenses = value;
    }
    public WaypointBase WaypointsBase
    {
        get => GenericNotImplementedError<WaypointBase>.TryGet(waypoints, transform.parent);
        private set => waypoints = value;
    }
    public Combat Combat
    {
        get => GenericNotImplementedError<Combat>.TryGet(combat, transform.parent);
        private set => combat = value;
    }

    private Movement movement;
    private CollisionSenses collisionSenses;
    private Combat combat;
    private WaypointBase waypoints;

    private void Awake()
    {
        Movement = GetComponentInChildren<Movement>();
        CollisionSenses = GetComponentInChildren<CollisionSenses>();
        Combat = GetComponentInChildren<Combat>();
        WaypointsBase = GetComponentInChildren<WaypointBase>();
    }

    public void LogicUpdate()
    {
        Movement.LogicUpdate();
        Combat.LogicUpdate();
    }

}
