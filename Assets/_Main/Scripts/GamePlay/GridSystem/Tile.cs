using System.Collections.Generic;
using _Main.Scripts.GamePlay.BlockSystem;
using UnityEngine;

namespace _Main.Scripts.GamePlay.GridSystem
{
    public abstract class Tile : MonoBehaviour
    {
        public Vector2Int GridPosition { get; set; }
        public Block BlockInTile { get; set; }
        public bool IsAvailable { get; set; }
        public List<Tile> TileNeighboursWithSameBlock { get; set; }
        public List<Tile> TileNeighbours { get; set; }
    }
}