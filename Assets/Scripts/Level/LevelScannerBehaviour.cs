/*
using System;
using System.Collections;
using UnityEngine;
using Color = UnityEngine.Color;

public class LevelScannerBehaviour : MonoBehaviour
{
    [SerializeField] private Renderer targetObjectRenderer;
    private Color newColor;
    private Renderer _renderer;
    private bool isIgnored = false;
    
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
        ColorUtility.TryParseHtmlString(HueColourValue(selecteColorByName), out newColor);
        ColorUtility.ToHtmlStringRGB(newColor);
        newColor.a = 0.5019608f;
        _renderer.material.color = newColor;
    }

    private void Update()
    {
        if (CompareColorsRGB(targetObjectRenderer.material.color, _renderer.material.color) && !isIgnored)
        {
            gameObject.layer = LayerMask.NameToLayer ("Ignore Raycast");
            isIgnored = true;
        }
        else if (!CompareColorsRGB(targetObjectRenderer.material.color, _renderer.material.color) && isIgnored)
        {
            isIgnored = false;
            gameObject.layer = LayerMask.NameToLayer ("Default");
        }
    }

    public static String HueColourValue(ColorNames color)
    {
        return (String)HueColour[color];
    }

    public bool CompareColorsRGB(Color aColor, Color bColor)
    {
        if (aColor.r == bColor.r && aColor.g == bColor.g && aColor.b == bColor.b) return true;
        else return false;
    }
}
*/
