﻿namespace x3e.etc
{
    public static class FloatExtension
    {
        public static float Percent(this float value, float percent) => value / (1f / (percent / 100));
    }
}