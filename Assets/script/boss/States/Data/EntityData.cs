using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEntityData", menuName = "Data/Entity Data/Base Data")]
public class EntityData : ScriptableObject
{
   public float wallCheckDistance = 1f;
   //public float ledgeCheckDistance;

   public LayerMask whatGround;
}
