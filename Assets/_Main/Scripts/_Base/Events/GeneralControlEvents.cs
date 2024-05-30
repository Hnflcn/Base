namespace _Main.Scripts._Base.Events
{
    public static class GeneralControlEvents
    {
        public delegate void EventAction<T>(T item);
        public delegate void EventAction2<T1, T2>(T1 item1, T2 item2);
        public delegate void SimpleEventAction(); 
        
        public static void TriggerEvent<T>(EventAction<T> eventAction, T item)
        {
            eventAction?.Invoke(item);
        }
        
        public static void TriggerEvent<T1, T2>(EventAction2<T1,T2> eventAction, T1 item, T2 item2)
        {
            eventAction?.Invoke(item, item2);
        }

        public static void TriggerSimpleEvent(SimpleEventAction eventAction)
        {
            eventAction?.Invoke();
        }
    }
}