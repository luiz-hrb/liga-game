using System;

namespace LigaGame.ExtensionMethods
{
    public static class FloatExtensions
    {
        public static string FromSecondsToTime(this float floatSeconds)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(floatSeconds);
            return $"{timeSpan.TotalMinutes:00}:{timeSpan.Seconds:00}";
        }
    }
}
