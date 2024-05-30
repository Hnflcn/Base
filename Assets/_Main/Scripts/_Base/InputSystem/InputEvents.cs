namespace _Main.Scripts._Base.InputSystem
{
    public static class InputEvents

    {
        public delegate void DownEvent();
    
        public static event DownEvent OnDownEvent;
    
        public delegate void UpdateEvent();
    
        public static event UpdateEvent OnUpdateEvent;
    
        public delegate void UpEvent();
    
        public static event UpEvent OnUpEvent;
    
        public static void TriggerOnDownEvent()
        {
            OnDownEvent?.Invoke();
        }
    
        public static void TriggerOnUpdateEvent()
        {
            OnUpdateEvent?.Invoke();
        }
        public static void TriggerOnUpEvent()
        {
            OnUpEvent?.Invoke();
        }
    }
}