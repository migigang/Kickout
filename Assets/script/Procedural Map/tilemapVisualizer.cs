using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class tilemapVisualizer : MonoBehaviour
{
    [SerializeField]
    private Tilemap floorTilemap, wallTilemap;
    [SerializeField]
    private TileBase floorTile,wallTop,wallSideRight, wallSideLeft, wallBottom, wallFull,wallInner, 
        wallInnerCornerDownLeft, wallInnerCornerDownRight,wallDiagonalCornerDownLeft, wallDiagonalCornerDownRight,
        wallDiagonalCornerUpRight, wallDiagonalCornerUpLeft;

    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions){
        PaintTiles(floorPositions, floorTilemap, floorTile);
    }

    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile){
        foreach(var position in positions){
            PaintSingleTile(tilemap, tile, position);
        }
    }
    private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position){
        var tilePosition = tilemap.WorldToCell((Vector3Int)position);
        tilemap.SetTile(tilePosition, tile);
    }

    internal void PaintSingleBasicWall(Vector2Int position, string binaryType){
        int typeAsInt = Convert.ToInt32(binaryType, 2);
        TileBase tile = null;
        if(wallTypesHelper.wallTop.Contains(typeAsInt)){
            tile = wallTop;
        }else if(wallTypesHelper.wallSideRight.Contains(typeAsInt)){
            tile=wallSideRight;
        }else if(wallTypesHelper.wallSideLeft.Contains(typeAsInt)){
            tile=wallSideLeft;
        }else if(wallTypesHelper.wallBottm.Contains(typeAsInt)){
            tile=wallBottom;
        }else if(wallTypesHelper.wallFull.Contains(typeAsInt)){
            tile=wallFull;
        }else if(wallTypesHelper.wallInner.Contains(typeAsInt)){
            tile=wallInner;
        }

        if(tile!=null)
            PaintSingleTile(wallTilemap, tile, position);
    }


    public void Clear(){
        floorTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
    }

    internal void PaintSingleCornerWall(Vector2Int position, string binaryType){
        int typeAsInt = Convert.ToInt32(binaryType, 2);
        TileBase tile = null;
        
        if(wallTypesHelper.wallInnerCornerDownLeft.Contains(typeAsInt)){
            tile= wallInnerCornerDownLeft;
        }else if(wallTypesHelper.wallInnerCornerDownRight.Contains(typeAsInt)){
            tile= wallInnerCornerDownRight;
        }else if(wallTypesHelper.wallDiagonalCornerDownLeft.Contains(typeAsInt)){
            tile= wallDiagonalCornerDownLeft;
        }else if(wallTypesHelper.wallDiagonalCornerDownRight.Contains(typeAsInt)){
            tile= wallDiagonalCornerDownRight;
        }else if(wallTypesHelper.wallDiagonalCornerUpLeft.Contains(typeAsInt)){
            tile= wallDiagonalCornerUpLeft;
        }else if(wallTypesHelper.wallDiagonalCornerUpRight.Contains(typeAsInt)){
            tile= wallDiagonalCornerUpRight;
        }else if(wallTypesHelper.wallFullEightDirections.Contains(typeAsInt)){
            tile= wallFull;
        }else if(wallTypesHelper.wallBottmEightDirections.Contains(typeAsInt)){
            tile= wallBottom;
        }


        if(tile !=null)
            PaintSingleTile(wallTilemap, tile, position);
    }
}
