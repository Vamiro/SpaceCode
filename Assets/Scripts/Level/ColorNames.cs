using System;
using System.Collections;
using UnityEngine;

namespace Level
{
    public enum ColorNames
    {
        Red = 1,    //0
        Orange = 2, //1
        Yellow = 3, //2
        Green = 4,  //3
        Blue = 5,   //4
        Violet = 6, //5
    }

    public static class ColorChange
    {
        private static Hashtable HueColour = new Hashtable
        {
            {ColorNames.Red, "#FF0000"},
            {ColorNames.Orange, "#FF7F00"},
            {ColorNames.Yellow, "#FFFF00"},
            {ColorNames.Green, "#00FF00"},
            {ColorNames.Blue, "#0000FF"},
            {ColorNames.Violet, "#8B00FF"},
        };
    
        public static string HueColourValue(this ColorNames color)
        {
            return (string)HueColour[color];
        }
    
        public static string HueColourValue(this string color)
        {
            Enum.TryParse(color, out ColorNames newHueColor);
            return (string)HueColour[newHueColor];
        }

        public static ColorNames ColorName(this string color)
        {
            return Enum.TryParse(color, out ColorNames newHueColor) ? newHueColor : ColorNames.Blue;
        }

        public static Color ConvertColor(this ColorNames currentHueColor)
        {
            ColorUtility.TryParseHtmlString(currentHueColor.HueColourValue(), out var newColor);
            return newColor;
        }
    }
}