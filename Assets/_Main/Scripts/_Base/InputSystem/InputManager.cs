
using _Main.Scripts.Managers;
using UnityEngine;

namespace _Main.Scripts._Base.InputSystem
{
    public class InputManager : MonoBehaviour
    {
        private bool _canDrag;
        private GameplayReferences _references;

        private void Awake()
        {
            _references = GameplayReferences.Instance;
        }

        private void Update()
        {
            if (_references.gameState != GameState.OnGame) return;
            if (Input.GetMouseButtonDown(0))
            {
                _canDrag = true;
                InputEvents.TriggerOnDownEvent();
            }
            else if (Input.GetMouseButton(0))
            {
                if (_canDrag)
                {
                    InputEvents.TriggerOnUpdateEvent();
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                _canDrag = false;
                InputEvents.TriggerOnUpEvent();
            }
        }
        
    }
}