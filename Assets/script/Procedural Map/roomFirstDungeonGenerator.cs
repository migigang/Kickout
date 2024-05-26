using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class roomFirstDungeonGenerator : simpleRandomWalkDungeonGenerator
{
  [SerializeField]
  private GameObject player;
  [SerializeField]
  private GameObject portal;
  [SerializeField]
  private GameObject[] enemyPrefabs;
  [SerializeField]
  private int numberOfEnemies = 10;
   [SerializeField]
   private int minRoomWidth = 4, minRoomHeight = 4;
   [SerializeField]
   private int dungeonWidth = 20, dungeonHeight = 20;
   [SerializeField]
   [Range(0, 10)]
   private int offset = 1;
   private Vector2Int playerPosition;
  // [SerializeField]
  // private bool randomWalkRooms = false;

  protected override void RunProceduralGeneration(){
    createRooms();
    player = GameObject.FindWithTag("Player");
    portal = GameObject.FindWithTag("Portal");
    playerPosition = new Vector2Int(Mathf.RoundToInt(player.transform.position.x), Mathf.RoundToInt(player.transform.position.y));
  }

  private void createRooms(){
    var roomList = proceduralGenerationAlgoritma.BinarySpacePartitioning(new BoundsInt((Vector3Int)startPosition, new Vector3Int(dungeonWidth, dungeonHeight, 0)), minRoomWidth, minRoomHeight);

    HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
    //if(randomWalkRooms){
      floor = CreateRoomsRandomly(roomList);
   // }else{
   // floor = CreateSimpleRooms(roomList);
    //}

    List<Vector2Int> roomCenters = new List<Vector2Int>();
    foreach(var room in roomList){
      roomCenters.Add((Vector2Int)Vector3Int.RoundToInt(room.center));
    }
    
    HashSet<Vector2Int> corridors = ConnectRooms(roomCenters);
    SpawnEnemies(floor, corridors);
    floor.UnionWith(corridors);
    PlacePortal(corridors);
    tileMapVisualizer.PaintFloorTiles(floor);
    wallGenerator.CreateWalls(floor, tileMapVisualizer);

  }

  private HashSet<Vector2Int> ConnectRooms(List<Vector2Int> roomCenters){
    HashSet<Vector2Int> corridors = new HashSet<Vector2Int>();
    var currentRoomCenter = roomCenters[Random.Range(0, roomCenters.Count)];
    roomCenters.Remove(currentRoomCenter);
      Vector2Int posPlayer = PlacePlayer(currentRoomCenter);

    while(roomCenters.Count > 0){
      Vector2Int closest = FindClosestPointTo(currentRoomCenter, roomCenters);
      roomCenters.Remove(closest);
      roomCenters.Remove(posPlayer);
      HashSet<Vector2Int> newCorridor = CreateCorridor(currentRoomCenter, closest);
      currentRoomCenter = closest;
      corridors.UnionWith(newCorridor);
    }
    
    return corridors;
  }

    private HashSet<Vector2Int> CreateRoomsRandomly(List<BoundsInt> roomList){
    HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
    for (int i=0; i < roomList.Count; i++){
      var roomBounds = roomList[i];
      var roomCenters = new Vector2Int(Mathf.RoundToInt(roomBounds.center.x), Mathf.RoundToInt(roomBounds.center.y));
      var roomFloor = RunRandomWalk(randomWalkParameters,roomCenters);
      foreach(var position in roomFloor){
        if(position.x >= (roomBounds.xMin + offset) && position.x <= (roomBounds.xMax - offset) && position.y >= (roomBounds.yMin - offset) && position.y <= (roomBounds.yMax - offset))
        {
          floor.Add(position);
        }
      }
    }
      return floor;

  }
  private void SpawnEnemies(HashSet<Vector2Int> floor, HashSet<Vector2Int> roomCenters) {
    List<Vector2Int> floorList = new List<Vector2Int>(floor);
    List<Vector2Int> roomCentersList = new List<Vector2Int>(roomCenters); // Convert HashSet to List
     floorList.RemoveAll(pos => roomCenters.Contains(pos) || pos == playerPosition || IsBorderTile(pos, floor));

    // Skip the first room
    Vector2Int firstRoomCenter = roomCentersList[0];
    floorList.RemoveAll(pos => Vector2.Distance(pos, firstRoomCenter) < Mathf.Max(minRoomWidth, minRoomHeight));

    int actualNumberOfEnemies = Mathf.Min(numberOfEnemies, floorList.Count);
    for (int i = 0; i < actualNumberOfEnemies; i++) {
        Vector2Int spawnPosition = floorList[Random.Range(0, floorList.Count)];
        GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)]; // select a random enemy prefab
        Instantiate(enemyPrefab, new Vector3(spawnPosition.x, spawnPosition.y, 0), Quaternion.identity);
        floorList.Remove(spawnPosition); // remove the position from the list to avoid spawning multiple enemies at the same position
    }
}

private bool IsBorderTile(Vector2Int pos, HashSet<Vector2Int> floor) {
    // Check the tiles around the position. If any of them is not a floor tile, then it's a border tile.
    return !floor.Contains(pos + Vector2Int.up) || !floor.Contains(pos + Vector2Int.down) || !floor.Contains(pos + Vector2Int.left) || !floor.Contains(pos + Vector2Int.right);
}

  private HashSet<Vector2Int> CreateCorridor(Vector2Int currentRoomCenter, Vector2Int destination){
    HashSet<Vector2Int> corridor = new HashSet<Vector2Int>();
    var position = currentRoomCenter;
    corridor.Add(position);
    while(position.y != destination.y){
      if(destination.y > position.y){
        position += Vector2Int.up;
      }else if( destination.y < position.y){
        position += Vector2Int.down;
      }
      corridor.Add(position);
    }
    while(position.x != destination.x){
      if(destination.x > position.x){
        position += Vector2Int.right;
      }else if(destination.x < position.x){
        position += Vector2Int.left;
      }
    corridor.Add(position);
    }
    return corridor;
  }

  private Vector2Int FindClosestPointTo(Vector2Int currentRoomCenter, List<Vector2Int> roomCenters){
    Vector2Int closest = Vector2Int.zero;
    float distance = float.MaxValue;
    foreach(var position in roomCenters){
      float currentDistance = Vector2.Distance(position, currentRoomCenter);
      if(currentDistance < distance){
        distance = currentDistance;
        closest = position;
      }
    }
    return closest;
  }

  private Vector2Int PlacePlayer(Vector2Int currentRoomCenter){

    if (player != null)
    {
        Debug.Log("Pindahkan karakter pemain ke posisi closestRoomCenter") ;
        player.transform.localPosition = currentRoomCenter + Vector2.one*0.5f;
         playerPosition = currentRoomCenter;
    }
    else
    {
        Debug.LogError("Player object not found!");
    }

    return playerPosition;
  }

  private void PlacePortal(HashSet<Vector2Int> roomCenters){

    List<Vector2Int> roomCentersList = new List<Vector2Int>(roomCenters);
    // Pilih posisi tengah ruangan secara acak
    var portalPosition = roomCentersList[Random.Range(0, roomCentersList.Count)];
    
    // Pastikan portal tidak ditempatkan di posisi yang sama dengan pemain
    while(portalPosition == playerPosition){
        portalPosition = roomCentersList[Random.Range(0, roomCentersList.Count)];
    }
    
    // Tempatkan portal di posisi yang dipilih
    portal.transform.position = new Vector3(portalPosition.x, portalPosition.y, 0);
    portal.SetActive(false);
}


  private HashSet<Vector2Int> CreateSimpleRooms(List<BoundsInt> roomList){
    HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
    foreach (var room in roomList){
        for(int col =offset; col < room.size.x - offset; col++){
            for(int row = offset; row < room.size.y - offset; row++){
                Vector2Int position = (Vector2Int)room.min + new Vector2Int(col,row);
                floor.Add(position);

            }
        }
    }
    return floor;
  }


}
