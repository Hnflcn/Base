using _Main.Scripts._Base.InputSystem;
using _Main.Scripts.GamePlay.Interfaces;
using UnityEngine;

namespace _Main.Scripts.GamePlay.BlockSystem
{
    public class Block : MonoBehaviour, IMergeable, ISelectable
    {
        public GameObject blockModel;
        public int BlockLevel { get; set; }
        public void Merge()
        {
            
        }
    }
}