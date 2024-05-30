using _Main.Scripts._Base.Events;


namespace _Main.Scripts.GamePlay.Events
{
    public static class GameControlEvents
    {
        public static event GeneralControlEvents.SimpleEventAction OnSuccessEvent; 
        public static event GeneralControlEvents.SimpleEventAction OnFailEvent; 
        public static event GeneralControlEvents.SimpleEventAction OnFinishControlEvent;
        
        

        public static void TriggerOnFinishControlEvent()
        {
            GeneralControlEvents.TriggerSimpleEvent(OnFinishControlEvent);
        }
        public static void TriggerOnFailEvent()
        {
            GeneralControlEvents.TriggerSimpleEvent(OnFailEvent);
        }

        public static void TriggerOnSuccessEvent()
        {
            GeneralControlEvents.TriggerSimpleEvent(OnSuccessEvent);
        }
    }
}