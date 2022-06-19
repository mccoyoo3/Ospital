using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newSearchStateData", menuName = "Data/State Data/Search State")]
public class D_SearchState : ScriptableObject
{
    public float searchTime = 30f;
    public float searchTimeDelay = 2f;
}
