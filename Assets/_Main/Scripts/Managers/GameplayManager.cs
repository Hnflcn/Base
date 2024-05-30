using System.Collections.Generic;
using System.Linq;
using _Main.Scripts.GamePlay.Events;
using _Main.Scripts.GamePlay.GridSystem;
using _Main.Scripts.GamePlay.GridSystem.TileGeneral;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using VPNest.UI.Scripts;

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
            UIManager.Instance.InGameUI.TapToStart();
            _gameplayReferences.targetUIController.Init();
            _gameplayReferences.coinController.Initialization();
            await _gameplayReferences.zoneController.Initialization();
            _gameplayReferences.tileSelectControl.Initialization();

            await _gameplayReferences.targetUIController.TargetAnimate();
            if (_gameplayReferences.ftueManager != null)
            {
                await UniTask.Delay(200);
                _gameplayReferences.ftueManager.Init();
            }
            
            
            _gameplayReferences.gameState = GameState.OnGame;
        }

        private void OnSuccess()
        {
            if(_gameplayReferences.gameState == GameState.OnFinish) return;
            _gameplayReferences.gameState = GameState.OnFinish;
            DOVirtual.DelayedCall(1f, () =>
            {
                UIManager.Instance.SuccessGame();
            });
        }

        private void OnFail()
        {
            if(_gameplayReferences.gameState == GameState.OnFinish) return;
            _gameplayReferences.gameState = GameState.OnFinish;
            DOVirtual.DelayedCall(1f, () =>
            {
                UIManager.Instance.FailGame();
            });
        }

    }
}
