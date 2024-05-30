
using _Main.Scripts.Managers;
using AssetKits.ParticleImage;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using VP.Nest;

namespace VPNest.UI.Scripts.InGameUI
{
    public class InGameUI : UIPanel
    {
        private GameplayReferences _references;
        private GameplayManager _manager;
        
        [SerializeField]private GameObject successPanel;
        [SerializeField]private GameObject moneyBG;
        [SerializeField]private GameObject failPanel;
        [SerializeField]private ParticleImage successCoinParticle;

        [SerializeField]private TextMeshProUGUI levelText;
        [SerializeField]private TextMeshProUGUI endMoneyText;

        public FillBar fillBar;

        private bool isStarted, isContinue, isRetry, isTyringLoadScene;

        public UnityAction OnLevelStart;
        public FillBar FillBar => fillBar;

        internal override void Initialize()
        {
            _references = GameplayReferences.Instance;
            base.Initialize();
            successPanel.SetActive(false);
            failPanel.SetActive(false);
            levelText.text = "LEVEL "+PlayerPrefKeys.CurrentLevel;
        }


        
        
        
        
        internal void OpenSuccessPanel()
        {
            var money = 100;
            endMoneyText.text = money.ToString();
            successPanel.SetActive(true);
            moneyBG.SetActive(false);
        }

        internal void OpenFailPanel()
        {
            failPanel.SetActive(true);
            moneyBG.SetActive(false);
        }

        public void TapToContinue()
        {
            if (!isTyringLoadScene)
            {
                isTyringLoadScene = true;

            }
        }

        public void TapToRetry()
        {
            if (!isTyringLoadScene)
            {
                isTyringLoadScene = true;

            }
        }

        public void TapToStart()
        {
            if (!isStarted)
            {
                isStarted = true;
                OnLevelStart?.Invoke();
                LevelManager.StartLevel();
            }
        }
    }
}