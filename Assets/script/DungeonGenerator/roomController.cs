using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomInfo{
    public string name;
    public int x;
    public int y;
}

public class roomController : MonoBehaviour
{

    public static roomController instance;

    string currentWorldName = "basement";

    RoomInfo currentLoadRoomData;

    Queue<RoomInfo> loadRoomQueue = new Queue<RoomInfo>();

    public List<room> loadedRooms = new List<room>();

    bool isLoadingRoom = false;

    void Awake() {
        instance = this;
    }
    
    public bool DoesRoomExist(int x, int y){
        return loadedRooms.Find(item => item.X == x && item.Y == y) !=null;
    }
}
