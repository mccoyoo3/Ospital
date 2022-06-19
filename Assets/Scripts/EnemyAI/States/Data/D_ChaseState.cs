using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerChaseStateData", menuName = "Data/State Data/Chase State")]
public class D_ChaseState : ScriptableObject
{
    public float chaseSpeed = 1f;
    public float attackDistance = 2f;
}
