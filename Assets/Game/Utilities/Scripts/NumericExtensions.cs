using System;
using System.Diagnostics.Contracts;
using UnityEngine;

namespace RoguelikeCardSystem.Game.Utilities
{
    public static class NumericExtensions
    {
        [Pure]
        public static int RoundToInt(this float number) => Mathf.RoundToInt(number);

        [Pure]
        public static float Round(this float number, int decimalPlaces) =>
            Mathf.Round(number * Mathf.Pow(10, decimalPlaces)) / Mathf.Pow(10, decimalPlaces);

        [Pure]
        public static int FloorToInt(this float number) => Mathf.FloorToInt(number);

        [Pure]
        public static int CeilToInt(this float number) => Mathf.CeilToInt(number);

        [Pure]
        public static float Clamp(this float number, float min, float max) => Mathf.Clamp(number, min, max);

        [Pure]
        public static int Clamp(this int number, int min, int max) => Mathf.Clamp(number, min, max);

        [Pure]
        public static float Clamp01(this float number) => Mathf.Clamp01(number);

        [Pure]
        public static float Max(this float number, float max) => Mathf.Max(number, max);

        [Pure]
        public static float Min(this float number, float min) => Mathf.Min(number, min);

        [Pure]
        public static float Max(this int number, float max) => Mathf.Max(number, max);

        [Pure]
        public static float Min(this int number, float min) => Mathf.Min(number, min);

        [Pure]
        public static int Max(this int number, int max) => Mathf.Max(number, max);

        [Pure]
        public static int Min(this int number, int min) => Mathf.Min(number, min);

        [Pure]
        public static bool IsInteger(this float value)
        {
            // Check if the value is close to an integer
            return Mathf.Approximately(value, Mathf.Round(value));
        }

        [Pure]
        public static string ToFormattedTimeString(this float seconds)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(seconds);
            return $"{(int)timeSpan.TotalHours:00}:{timeSpan.Minutes:00}:{timeSpan.Seconds:00}.{timeSpan.Milliseconds:000}";
        }
    }
}