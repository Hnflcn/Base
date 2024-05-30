using System;
using System.Collections.Generic;
using _Main.Scripts.GamePlay.GridSystem;
using Array2DEditor;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Main.Scripts._Base.GridSystem.SO
{
    [CreateAssetMenu(menuName = "LevelDataGrid", fileName = "LevelGrids")]
    public class LevelGridData : ScriptableObject
    {
        [Header("--- Grids ---")]
        [Space]
        public Array2DInt gridsTile;
        public Array2DInt gridsZone;
        
        [Space]
        [Header("--- Grids Properties ---")]
        [Space]
        
        public List<TileGridType> tileGridType = new List<TileGridType>();
        public List<ZoneGridType> zoneGridType = new List<ZoneGridType>();

        [Space]
        [Header("--- Random Datas ---")]
        [Space]
        
        public bool IsRandom;
        [ShowIfGroup("IsRandom")] 
        public List<Randoms> randomsData = new List<Randoms>();
        
        
        [Button]
        public void SetIntGridTile(int val)
        {
            gridsTile.SetCells(val); 
        }
        [Button]
        public void SetIntZoneTile(int val)
        {
            gridsZone.SetCells(val); 
        }
        
    }

    [Serializable]
    public class Randoms
    {
        public int blockLevel;
        [Range(0, 10)] 
        public int LevelRandomRange;
    }

    [Serializable]
    public class TileGridType
    {
        public int tileDataID;
        public bool isClosed;
        public bool isIce;
    }
    [Serializable]
    public class ZoneGridType
    {
        public int zoneDataID;
        public bool isActive;
    }
}