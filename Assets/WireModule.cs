using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WireModule : Module
{
    [SerializeField]
    private Renderer[] wires;
    public Color[] colors = new Color[5];
    private List<int> cuttableWires = new List<int>();
    private Color[] possibleColors = new Color[] {
        Color.red,
        Color.blue,
        Color.green,
        Color.yellow,
        Color.black
    };
    // Start is called before the first frame update
    void Start() {
        // set the wire colors to random colors
        for (int i = 0; i < wires.Length; i++) {
            Color tempColor = possibleColors[Random.Range(0, possibleColors.Length)];
            wires[i].material.color = tempColor;
            colors[i] = tempColor;
        }
        checkAnswer();
        foreach (var wire in cuttableWires)
        {
            Debug.Log(wire);
        }
        
    }

    // Update is called once per frame
    void Update() {
        
    }
    
    private void checkAnswer() {
        if (colors[4] == Color.red) {
            for (int i = 0; i < wires.Length; i++) {
                if (wires[i].material.color == Color.red) {
                    cuttableWires.Add(i);
                }
            }
            return;
        }
        // if order is Red, Yellow, Green, Black, Blue
        if (colors[0] == Color.red && colors[1] == Color.yellow && colors[2] == Color.green && colors[3] == Color.black && colors[4] == Color.blue) {
            cuttableWires.Add(0);
            cuttableWires.Add(1);
            cuttableWires.Add(2);
            cuttableWires.Add(3);
            cuttableWires.Add(4);
            return;
        }
        //If Red is left of Yellow cut red
        for (int i = 0; i < colors.Length -1 ; i++) {
            if (colors[i] == Color.red && colors[(i + 1)] == Color.yellow) {
                cuttableWires.Add(i);
            }
        }
        //If Yellow is Right of black
        for (int i = 0; i < colors.Length -1 ; i++) {
            if (colors[i] == Color.black && colors[(i + 1)] == Color.yellow) {
                cuttableWires.Add(i);
            }
        }
        //If Green is left of Blue Cut green
        for (int i = 0; i < colors.Length -1 ; i++) {
            if (colors[i] == Color.green && colors[(i + 1)] == Color.blue) {
                cuttableWires.Add(i);
            }
        }
        //a blue wire is not in the list Cut Yellow
        if (cuttableWires.All(x => colors[x] != Color.blue)) {
            for (int i = 0; i < colors.Length; i++) {
                if (colors[i] == Color.yellow) {
                    cuttableWires.Add(i);
                }
            }
        }
        //If Black needs to be cut cut blue
        if (cuttableWires.All(x => colors[x] == Color.black)) {
            for (int i = 0; i < colors.Length; i++) {
                if (colors[i] == Color.blue) {
                    cuttableWires.Add(i);
                }
            }
        }
        //If Blue and Yellow are next to each other do not cut blue or yellow
        for (int i = 0; i < colors.Length -1 ; i++) {
            if (colors[i] == Color.blue && colors[(i + 1)] == Color.yellow) {
                cuttableWires.Remove(i);
                cuttableWires.Remove(i + 1);
            }
        }
        //If Green is in the middle do not cut green
        if (colors[2] == Color.green) {
            cuttableWires.Remove(2);
        }

    }

    public void tryCut(int index) {

    }

}
