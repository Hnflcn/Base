using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace _Main.Scripts._Base.SaveSystem
{
    public static class SaveJson
    {
        public static void SaveOpenZoneList(List<int> saved)
        {
            SaveIntListToJson(saved);
        }

        public static List<int> GetOpenZoneList()
        {
            return LoadIntListFromJson();
        }
        
        
        [System.Serializable]
        public class IntListWrapper
        {
            public List<int> intList = new List<int>();
        }
    
        private static void SaveIntListToJson(List<int> intList)
        {
            var wrapper = new IntListWrapper();
            wrapper.intList = intList;
    
            var json = JsonUtility.ToJson(wrapper);
            File.WriteAllText(Application.persistentDataPath + "/zoneList.json", json);
        }
    
        private static List<int> LoadIntListFromJson()
        {
            if (File.Exists(Application.persistentDataPath + "/zoneList.json"))
            {
                var json = File.ReadAllText(Application.persistentDataPath + "/zoneList.json");
                var wrapper = JsonUtility.FromJson<IntListWrapper>(json);
                return wrapper.intList;
            }
            return new List<int>();
    }

    }
}