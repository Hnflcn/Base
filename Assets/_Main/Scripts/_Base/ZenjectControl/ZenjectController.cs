using _Main.Scripts._Base.Pool;
using _Main.Scripts._Base.Pool.Types;
using _Main.Scripts.Pool;
using UnityEngine;
using Zenject;

namespace _Main.Scripts._Base.ZenjectControl
{
    public class ZenjectController : MonoInstaller
    {
        [SerializeField] private MyObjectPool objectPool;
        [SerializeField] private ParticlePool particlePool;
        [SerializeField] private FloatingPool floatingPool;
        [SerializeField] private AudioPool audioPool;
        public override void InstallBindings()
        {
            Container.BindInstance(objectPool).AsSingle();
            Container.BindInstance(particlePool).AsSingle();
            Container.BindInstance(floatingPool).AsSingle();
            Container.BindInstance(audioPool).AsSingle();
        }
    }
}
