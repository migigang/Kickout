using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class wallGenerator 
{
    public static void CreateWalls(HashSet<Vector2Int> floorPositions, tilemapVisualizer TileMapVisualizer){
        var basicWallPositions = FindWallsInDirections(floorPositions, Direction2D.cardinalDirectionList);
        var cornerWallPositions = FindWallsInDirections(floorPositions, Direction2D.diagonalDirectionList);
        CreateBasicWall(TileMapVisualizer, basicWallPositions, floorPositions);
        CreateCornerWalls(TileMapVisualizer, cornerWallPositions, floorPositions);
    }
    
    private static void CreateCornerWalls(tilemapVisualizer TileMapVisualizer, HashSet<Vector2Int> cornerWallPositions, HashSet<Vector2Int> floorPositions)
    {
        foreach(var position in cornerWallPositions){
            string neighboursBinaryType = ""; 
            foreach(var direction in Direction2D.eightDirectionList){
                var neighbourPosition = position + direction;
                if(floorPositions.Contains(neighbourPosition)){
                    neighboursBinaryType +="1";
                }else{
                    neighboursBinaryType+="0";
                }
            }
            TileMapVisualizer.PaintSingleCornerWall(position, neighboursBinaryType);
        }
    }

    private static void CreateBasicWall(tilemapVisualizer TileMapVisualizer, HashSet<Vector2Int> basicWallPositions, HashSet<Vector2Int> floorPositions){
        foreach(var position in basicWallPositions){
            string neighboursBinaryType = "";
            foreach(var direction in Direction2D.cardinalDirectionList){
                var neighbourPosition = position + direction;
                if(floorPositions.Contains(neighbourPosition)){
                    neighboursBinaryType +="1";
                }else{
                    neighboursBinaryType+="0";
                }
            }
            TileMapVisualizer.PaintSingleBasicWall(position, neighboursBinaryType);
        }
    }


private static HashSet<Vector2Int> FindWallsInDirections(HashSet<Vector2Int> floorPositions, List<Vector2Int> directionList){
    HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();
    foreach(var position in floorPositions){
        foreach(var direction in directionList){
            var neighbourPosition = position + direction;
            if(floorPositions.Contains(neighbourPosition) == false)
                wallPositions.Add(neighbourPosition);
        }
    }
    return wallPositions;
}

}