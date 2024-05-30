using UnityEngine;

namespace _Main.Scripts._Base.InputSystem
{
    public abstract class InputController : MonoBehaviour
    {
        private void OnEnable()
        {
            InputEvents.OnDownEvent += OnDownEvent;
            InputEvents.OnUpEvent += OnUpEvent;
            InputEvents.OnUpdateEvent += OnUpdateEvent;
        }
        private void OnDisable()
        {
            InputEvents.OnDownEvent -= OnDownEvent;
            InputEvents.OnUpEvent -= OnUpEvent;
            InputEvents.OnUpdateEvent -= OnUpdateEvent;
        }
        

        protected abstract void OnDownEvent();
        protected abstract void OnUpdateEvent();
        protected abstract void OnUpEvent();


    }
}