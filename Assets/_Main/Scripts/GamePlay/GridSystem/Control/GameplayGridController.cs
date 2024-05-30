using _Main.Scripts._Base.GridSystem;
using _Main.Scripts._Base.GridSystem.SO;
using _Main.Scripts._Base.Pool._Main.Scripts.Pool;
using _Main.Scripts.GamePlay.ZoneSystem;
using _Main.Scripts.Managers;
using _Main.Scripts.Pool;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Main.Scripts.GamePlay.GridSystem.Control
{
    public class GameplayGridController : GridManager
    {
        private GameplayReferences _reference;
        [SerializeField] private GameObject parentGrids;
        [SerializeField] private GameObject parentZones;

        public Tile[,] tileArray;
        protected override async UniTask Generating()
        {
            _reference = GameplayReferences.Instance;
           // await GenerateGrid();
           // await GenerateZone();
           

            _reference.GameplayManager.Initialization();
        }
        private UniTask GenerateGrid()
        {
            tileArray = new Tile[levelGridData.gridsTile.GridSize.x, levelGridData.gridsTile.GridSize.y];
            GridGenerator.GeneratePrefabInt<Tile>(parentGrids, levelGridData.gridsTile, ObjeType.Tile,
                offsetGridX, offsetGridY, _objectPool, GeneratingGrid);

            return UniTask.CompletedTask;
        }

        private void GeneratingGrid(int value, int x, int y, (bool,Tile) prefab)
        {
            if (prefab.Item1)
            {
                tileArray[x, y] = prefab.Item2;
            }
        }

        private UniTask GenerateZone()
        {
            GridGenerator.GeneratePrefabInt<Zone>(parentZones, levelGridData.gridsZone, ObjeType.Zone,
                offsetGridZoneX, offsetGridZoneY, _objectPool, GeneratingZone);

            return UniTask.CompletedTask;
        }
        private void GeneratingZone(int value, int x, int y, (bool,Zone) prefab)
        {
            if (prefab.Item1)
            {
            }
        }

    }

}