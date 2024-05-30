
using _Main.Scripts.GamePlay.Events;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace _Main.Scripts.Managers
{
    public class GameplayManager : MonoBehaviour
    {
        private GameplayReferences _gameplayReferences;

        private void OnEnable()
        {
            _gameplayReferences = GameplayReferences.Instance;

            GameControlEvents.OnSuccessEvent += OnSuccess;
            GameControlEvents.OnFailEvent += OnFail;
        }

        private void OnDisable()
        {
            GameControlEvents.OnSuccessEvent -= OnSuccess;
            GameControlEvents.OnFailEvent -= OnFail;
        }

        public async void Initialization()
        {
            _gameplayReferences.UIManager.Initialization();
            _gameplayReferences.tileSelectControl.Initialization();
            await _gameplayReferences.gridController.Initialization();
            
            _gameplayReferences.gameState = GameState.OnGame;
        }

        private void OnSuccess()
        {
            if(_gameplayReferences.gameState == GameState.OnFinish) return;
            _gameplayReferences.gameState = GameState.OnFinish;
            DOVirtual.DelayedCall(1f, () =>
            {
               
            });
        }

        private void OnFail()
        {
            if(_gameplayReferences.gameState == GameState.OnFinish) return;
            _gameplayReferences.gameState = GameState.OnFinish;
            DOVirtual.DelayedCall(1f, () =>
            {
                
            });
        }

    }
}
