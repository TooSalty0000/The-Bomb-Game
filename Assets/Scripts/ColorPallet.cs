using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ColorPallet
{
    [System.Serializable]
    public struct ColorValue
    {
        public Color color;
        public int value;
    }
    public ColorValue[] possibleColors = new ColorValue[5];
}
