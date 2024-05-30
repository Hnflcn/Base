using _Main.Scripts._Base.Pool.Types;
using _Main.Scripts.Pool;
using UnityEngine;
using Zenject;
using FloatingType = Pool.FloatingType;

namespace _Main.Scripts._Base.ZenjectControl
{
    public class ZenjectController : MonoInstaller
    {
        [SerializeField] private ObjectPool<ObjeType> objectPool;
        [SerializeField] private ObjectPool<ParticleType> particlePool;
        [SerializeField] private ObjectPool<FloatingType> floatingPool;
        [SerializeField] private ObjectPool<BlockType> blockPool;
        [SerializeField] private ObjectPool<SoundType> audioPool;
        public override void InstallBindings()
        {
            Container.BindInstance(objectPool).AsSingle();
            Container.BindInstance(particlePool).AsSingle();
            Container.BindInstance(floatingPool).AsSingle();
            Container.BindInstance(audioPool).AsSingle();
            Container.BindInstance(blockPool).AsSingle();
        }
    }
}
