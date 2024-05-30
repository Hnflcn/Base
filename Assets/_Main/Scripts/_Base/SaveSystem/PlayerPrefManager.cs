using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Main.Scripts._Base.SaveSystem
{
    public class PlayerPrefManager : MonoBehaviour
    {
        private int[,] _grid;

        #region Save Grid

        private void ConvertGridBoolToInt(bool[,] gridBool)
        {
            _grid = new int[gridBool.GetLength(0), gridBool.GetLength(1)];

            for (var y = 0; y < gridBool.GetLength(0); y++)
            {
                for (var x = 0; x < gridBool.GetLength(1); x++)
                {
                    var newStatus = !gridBool[x, y];
                    if (newStatus)
                    {
                        _grid[x, y] = 1;
                    }
                    else
                    {
                        _grid[x, y] = 0;
                    }
                } 
            }
        }
        
        
        private bool[,] ConvertGridIntToBool()
        {
            var gridBool = new bool[LoadGrid().GetLength(0), LoadGrid().GetLength(1)];
            for (var y = 0; y < gridBool.GetLength(0); y++)
            {
                for (var x = 0; x < gridBool.GetLength(1); x++)
                {
                    var newStatus = LoadGrid()[x, y];
                    if (newStatus == 0)
                    {
                        gridBool[x, y] = true;
                    }
                    else
                    {
                        gridBool[x, y] = false;
                    }
                } 
            }

            return gridBool;
        }

       
        private void SaveGrid()
        {
            var jsonGrid = JsonUtility.ToJson(new SerializationGrid<int>(_grid));
            PlayerPrefs.SetString("SavedGrid", jsonGrid);
            PlayerPrefs.Save();
        }

        private int[,] LoadGrid()
        {
            var jsonGrid = PlayerPrefs.GetString("SavedGrid");
            _grid = JsonUtility.FromJson<SerializationGrid<int[,]>>(jsonGrid).ToArray();
            return _grid;
        }


        #endregion


        #region Save Tuple

        public void SaveTileData(List<(int,int)> intList)
        {
            var jsonTile = JsonUtility.ToJson(intList);
            PlayerPrefs.SetString("TileData", jsonTile);
        }


        public List<(int,int)> GetTileData()
        {
            var loadedJson = PlayerPrefs.GetString("TileData", "");
            var loadedTupleList = JsonUtility.FromJson<List<(int,int)>>(loadedJson).ToList();
            return loadedTupleList;
        }


        #endregion

    }

   
        
    
}