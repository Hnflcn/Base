using System;
using System.Collections.Generic;

namespace _Main.Scripts._Base.Remote
{
    [Serializable]
    public class LevelEndMoney
    {
        public List<LevelEndMoneyData> TutorialLevels = new List<LevelEndMoneyData>();
        public List<LevelEndMoneyData> Levels = new List<LevelEndMoneyData>();
    }

    [Serializable]
    public class LevelEndMoneyData
    {
        public int Level;
        public int EndPrice;

        public LevelEndMoneyData(int level, int endPrice)
        {
            this.Level = level;
            this.EndPrice = endPrice;
        }
    }
}