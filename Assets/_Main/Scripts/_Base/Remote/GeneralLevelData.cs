using _Main.Scripts.Remote;
using UnityEditor;
using UnityEngine;

namespace _Main.Scripts._Base.Remote
{
    [CreateAssetMenu(fileName = "GeneralLevelData", menuName = "ScriptableObjects/GeneralLevelData", order = 0)]
    public class GeneralLevelData : ScriptableObject
    {
        public ZonePriceData zonePrice;


#if UNITY_EDITOR
        [ContextMenu("CopyJsonData")]
        public void CopyJsonData()
        {
            string json = JsonUtility.ToJson(this);
            EditorGUIUtility.systemCopyBuffer = json;
        }

#endif
    }
}