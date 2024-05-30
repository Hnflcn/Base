namespace _Main.Scripts._Base.HelperFunctions
{
    public static class TimeFormatter
    {
        public static string ConvertSecondToMinute(byte second)
        {
            var minutes = second / 60;
            var remainingSecond = second % 60;
            return $"{minutes:D2}:{remainingSecond:D2}";
        }
    }
}