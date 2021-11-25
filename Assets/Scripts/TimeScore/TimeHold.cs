using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds the score of the time
/// </summary>
[CreateAssetMenu(menuName = "TimeKeep", fileName = "New Time Keep")]
public class TimeHold : ScriptableObject
{
    //The time score
    [SerializeField] public float timeScore;        //How much time has passed
}
