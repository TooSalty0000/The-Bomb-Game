using UnityEngine;
using System.Collections;

[System.Serializable]
public class GridLayout
{
    [System.Serializable]
    public struct gridrow {
        public bool[] row;
    }

    public gridrow[] rows = new gridrow[5];
}
