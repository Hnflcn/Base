using System;
using System.Collections.Generic;
using _Main.Scripts.Managers;
using UnityEngine;
using UnityEngine.Events;
using VP.Nest;
using VP.Nest.UI.Currency;

namespace VPNest.UI.Scripts
{
    public class UIManager : Singleton<UIManager>
    {
        [Header("CommonUIs")]
        [SerializeField] private CurrencyUI currencyUI;
        [SerializeField] private InGameUI.InGameUI inGameUI;

        [Space] [SerializeField] private List<UIPanel> focusablePanels;

        private List<bool> _defaultCanvasValues = new List<bool>();
        private bool _isFocusingPanel = false;
        private Type _focusingPanelType = null;

        public UnityAction<Type> OnPanelFocus;
        public UnityAction<Type> OnPanelUnfocus;

        #region EncapsulationMethods

        public InGameUI.InGameUI InGameUI => inGameUI;

        public CurrencyUI CurrencyUI => currencyUI;

        public bool IsFocusingPanel => _isFocusingPanel;

        #endregion

        #region UnityEventFunctions

        private void Awake()
        {
            Initialize();
        }

        #endregion

        #region InitializationMethods

        private void Initialize()
        {
            InGameUI.Initialize();
            CurrencyUI.Initialize();

            var focusablePanelCount = focusablePanels.Count;
            for (int i = 0; i < focusablePanelCount; i++)
            {
                _defaultCanvasValues.Add(focusablePanels[i].IsCanvasEnable);
            }
        }

        #endregion

        #region PanelMethods

        public void FocusPanel(Type focusingPanelType)
        {
            var uiPanelsCount = focusablePanels.Count;

            for (int i = 0; i < uiPanelsCount; i++)
            {
                var currentPanel = focusablePanels[i];
                currentPanel.SetCanvasEnable(currentPanel.GetType() == focusingPanelType);
            }

            _isFocusingPanel = true;
            _focusingPanelType = focusingPanelType;
            OnPanelFocus?.Invoke(focusingPanelType);
        }

        public void UnfocusPanel()
        {
            var uiPanelsCount = focusablePanels.Count;

            for (int i = 0; i < uiPanelsCount; i++)
            {
                focusablePanels[i].SetCanvasEnable(_defaultCanvasValues[i]);
            }

            _isFocusingPanel = false;
            OnPanelUnfocus?.Invoke(_focusingPanelType);
            _focusingPanelType = null;
        }

        #endregion

        private bool isContinueOrFailed = false;

        public void SuccessGame()
        {
            GameplayReferences.Instance.gameState = GameState.OnFinish;
            if (!isContinueOrFailed)
            {
                LevelManager.InitLevelComplete();
                isContinueOrFailed = true;
                InGameUI.OpenSuccessPanel();
            }
        }

        public void FailGame()
        {
            GameplayReferences.Instance.gameState = GameState.OnFinish;
            if (!isContinueOrFailed)
            {
                LevelManager.InitLevelFail();
                isContinueOrFailed = true;
                InGameUI.OpenFailPanel();
            }
        }
    }
}