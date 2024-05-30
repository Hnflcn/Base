using System;
using System.Collections.Generic;
using _Main.Scripts._Base.Enums;
using _Main.Scripts.Pool;
using Array2DEditor;
using UnityEngine;

namespace _Main.Scripts._Base.GridSystem
{
    public static class GridGenerator 
    {
        public static T[,] GeneratePrefabInt<T>(GameObject parent, Array2DInt array, ObjeType type, 
            float offsetX, float offsetY, ObjectPool pool, Action<int, int, int,(bool, T)> generate)
        {
            var gridSizeX = array.GridSize.x;
            var gridSizeY = array.GridSize.y;
            
            var matrix = new T[gridSizeX, gridSizeY];
            
            for (var y = 0; y < gridSizeY; y++)
            for (var x = 0; x < gridSizeX; x++)
            {
                
              //  if (array.GetCell(x, gridSizeY - y - 1) != 0)
              //  {
                    var value = array.GetCell(x, gridSizeY - y - 1); 
                    var prefab = InstantiatePrefabObje<T>(x, y, parent, type, offsetX, offsetY, pool);
                    generate(value, x, y, prefab);
                    matrix[x, y] = prefab.Item2;
              //  }
            }

            return matrix;
        }


        
        public static void GeneratePrefabInt(GameObject parent, Array2DInt array, float offsetX, float offsetY,
            Action< GameObject, int, int> generate)
        {
            var gridSizeX = array.GridSize.x;
            var gridSizeY = array.GridSize.y;
            
            for (var y = 0; y < gridSizeY; y++)
            for (var x = 0; x < gridSizeX; x++)
                if (array.GetCell(x, gridSizeY - y - 1) != 0)
                {
                    //var value = array.GetCell(x, gridSizeY - y - 1);
                    generate(parent, x, y);
                }
        }
        
        public static void GeneratePrefabBool<T>(GameObject parent, Array2DBool array, 
            ObjeType type, float offsetX, float offsetY, ObjectPool pool,
            Action<(bool,T), int ,int> generate)
        {
            var gridSizeX = array.GridSize.x;
            var gridSizeY = array.GridSize.y;
            
            for (var y = 0; y < gridSizeY; y++)
            for (var x = 0; x < gridSizeX; x++)
                if (array.GetCell(x, gridSizeY - y - 1))
                {
                    var prefab = InstantiatePrefabObje<T>(x, y, parent, type, offsetX, offsetY, pool);
                    generate(prefab, x, y);
                }
        }
 
        
        public static (bool, T) InstantiatePrefabObje<T>(int x, int y, GameObject parent, 
            ObjeType prefabType, float offsetX, float offsetY, ObjectPool pool)
        {
            var prefabGO =  pool.Spawn(prefabType, parent.transform);
            var posX = x * (1 + offsetX);
            var posY = y * (1 + offsetY);
            prefabGO.transform.localPosition = new Vector3(posX, posY, 0);
            return prefabGO.TryGetComponent(out T scr) ? (true, scr) : (false, scr);
        }
        
       // public static (bool, T) InstantiatePrefabBlock<T>(GameObject parent,
       // BlockType prefabType,  BlockPool pool)
       // {
       //     var prefabGO =  pool.Spawn(prefabType, parent.transform);
       //     prefabGO.transform.localPosition = Vector3.zero;
       //     return prefabGO.TryGetComponent(out T scr) ? (true, scr) : (false, scr);
       // }


    }

}