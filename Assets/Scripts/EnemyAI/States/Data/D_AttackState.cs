using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newAttackStateData", menuName = "Data/State Data/Attack State")]
public class D_AttackState : ScriptableObject
{
    public float attackDelay = 1f;
    public float attackRange = 1f;
    public float attackDamage = 2f;
}