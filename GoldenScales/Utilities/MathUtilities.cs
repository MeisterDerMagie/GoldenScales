﻿//(c) copyright by Martin M. Klöckener
namespace ConsoleAdventure.Utilities;

public static class MathUtilities
{
    //remap for float
    public static float Remap(float value, float low1, float high1, float low2, float high2)
    {
        return low2 + (value - low1) * (high2 - low2) / (high1 - low1);
    }
}