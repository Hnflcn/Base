
using Zenject;

namespace _Main.Scripts._Base.ZenjectControl
{
    public class BindControl<T> : MonoInstaller
    {
        public T reference;
        
        public override void InstallBindings()
        {
            Container.BindInstance(reference).AsSingle().NonLazy();
            Container.BindInstance(reference).AsSingle();
        }
    }
}