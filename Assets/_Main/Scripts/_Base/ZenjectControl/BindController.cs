using _Main.Scripts._Base.FeedBacks;
using _Main.Scripts.Managers;
using UnityEngine;
using Zenject;

namespace _Main.Scripts._Base.ZenjectControl
{
    public class BindController : MonoInstaller
    {
        [Header("Managers")] 
        public GameplayReferences reference;
        public GameplayManager gameplayManager;
        public FeedbackController feedbackController;
        public UIManager uiManager;
        
        
        public override void InstallBindings()
        {
            Container.BindInstance(reference).AsSingle().NonLazy();
            Container.BindInstance(gameplayManager).AsSingle().NonLazy();
            Container.BindInstance(uiManager).AsSingle().NonLazy();
            Container.BindInstance(feedbackController).AsSingle().NonLazy();
        }
    }
}