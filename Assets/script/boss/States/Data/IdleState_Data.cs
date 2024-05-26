using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newIdleData", menuName = "Data/Idle Data/Idle Data")]
public class IdleState_Data : ScriptableObject
{
    public float minIdleTime = 1f;
    public float maxIdleTime = 2f;
}
