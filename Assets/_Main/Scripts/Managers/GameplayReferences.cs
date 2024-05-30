using _Main.Scripts.GamePlay.InputSystem;
using UnityEngine;

namespace _Main.Scripts.Managers
{
    public enum GameState
    {
        OnStart,
        OnGame, 
        Pause,
        AnimationWaited,
        OnFinish
    }
    public class GameplayReferences : Singleton<GameplayReferences>
    {
        [Header("Managers")] 
        public GameState gameState;
        
        public bool CanContinueGame { get; set; }

        public InputHandler InputHandler { get; set; }
        public GameplayManager GameplayManager { get; set; }

    }
}