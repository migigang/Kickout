using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class abstrakDungeonGenerator : MonoBehaviour
{
   [SerializeField]
   protected tilemapVisualizer tileMapVisualizer = null;
   [SerializeField]
   protected Vector2Int startPosition = Vector2Int.zero;


   public void GenerateDungeon(){
    tileMapVisualizer.Clear();
    RunProceduralGeneration();
   }

   protected abstract void RunProceduralGeneration();
}
