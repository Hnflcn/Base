using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

namespace _Main.Scripts.Remote
{
    [Serializable]
    public class ZonePrice
    {
        public List<LevelPriceData> TutorialLevels;
        public List<LevelPriceData> Levels;
    }

    [Serializable]
    public class ZonePriceData
    {
        public ZonePrice data;
    }

    [Serializable]
    public class LevelPriceData
    {
        public int Level;
        public int ZonePriceLeft;
        public int ZonePriceRight;
    }
}