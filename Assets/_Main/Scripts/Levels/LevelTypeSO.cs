using UnityEngine;

namespace Levels
{
    public enum GameType
    {
        Normal,
        Hard
    }
    
    [CreateAssetMenu(menuName = "LevelType", fileName = "levelType")]
    public class LevelTypeSo : ScriptableObject
    {
        private GameType _gameType;
        
        public GameType ReturnGameType()
        {
            return _gameType;
        }
    }
}