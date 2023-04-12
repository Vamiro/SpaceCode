using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using Color = UnityEngine.Color;

public class LevelScannerBehaviour : MonoBehaviour
{
    private Color newColor;
    private Renderer _renderer;
    public string currentScannerHueColor = "Red";
    
    public enum ColorNames
    {
        Red = 1,    //0
        Orange = 2, //1
        Yellow = 3, //2
        Green = 4,  //3
        Blue = 5,   //4
        Violet = 6, //5
    }
    
    [SerializeField] private ColorNames selecteColorByName;
    
    private static Hashtable HueColour = new Hashtable
    {
        {ColorNames.Red, "#FF0000"},
        {ColorNames.Orange, "#FF7F00"},
        {ColorNames.Yellow, "#FFFF00"},
        {ColorNames.Green, "#00FF00"},
        {ColorNames.Blue, "#0000FF"},
        {ColorNames.Violet, "#8B00FF"},
    };
    
    private void Awake()
    {
        _renderer = gameObject.GetComponent<Renderer>();
        ColorUtility.TryParseHtmlString(currentScannerHueColor = HueColourValue(selecteColorByName), out newColor);
        ColorUtility.ToHtmlStringRGB(newColor);
        newColor.a = 0.5019608f;
        _renderer.material.color = newColor;
    }

    public static string HueColourValue(ColorNames color)
    {
        return (string)HueColour[color];
    }
    
    public static string HueColourValue(string color)
    {
        Enum.TryParse(color, out ColorNames newHueColor);
        return (string)HueColour[newHueColor];
    }
}
