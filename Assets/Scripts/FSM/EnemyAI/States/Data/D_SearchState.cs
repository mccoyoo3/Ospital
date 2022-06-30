using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newSearchStateData", menuName = "Data/State Data/Search State")]
public class D_SearchState : ScriptableObject
{
    public float minSearchTime = 10f;
    public float maxSearchTime = 20f;
    public float lockerSearchDelayTime = 5f;
    public float searchMoveSpeed = 4.5f;
}
