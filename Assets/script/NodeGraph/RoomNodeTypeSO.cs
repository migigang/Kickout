using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomNodeType_", menuName ="Data/Dungeon/Room Node Type")]

public class RoomNodeTypeSO : ScriptableObject
{
    public string roomNodeTypeName;

    #region Header
    [Header("Only flag the RoomNodeTypes that should be visible in the editor")]
    #endregion
    public bool displayInNodeGraphEditor = true;
    #region Header
    [Header("One type should be a corridor")]
    #endregion
    public bool isCorridor;
    #region Header
    [Header("One type should be a corridorNS")]
    #endregion
    public bool isCorridorNS;
    #region Header
    [Header("One type should be a corridorEW")]
    #endregion
    public bool isCorridorEW;
    #region Header
    [Header("One type should be an Entrance")]
    #endregion
    public bool isEntrance;
    #region Header
    [Header("One type should be a Boss Room")]
    #endregion
    public bool isBossRoom;
    
    #region Header
    [Header("One type should be a none (unassigned)")]
    #endregion
    public bool isNone;

    #region validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUltilities.ValidateCheckEmptyString(this, nameof(roomNodeTypeName), roomNodeTypeName);
    }
#endif
    #endregion

}
