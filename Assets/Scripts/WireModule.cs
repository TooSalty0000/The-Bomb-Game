using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WireModule : Module
{
    [SerializeField]
    private Renderer[] wires;
    [SerializeField]
    private Renderer[] lights;
    private int[] values = new int[5];
    private int cutCount = 0;

    [SerializeField]
    private List<ColorPallet> possibleColors = new List<ColorPallet>();
    // Start is called before the first frame update
    async void Start() {
        values = new int[5];
        // select a random color pallet, then set the wire color to one of those colors, wire colors cannot repeat
        int palletIndex = Random.Range(0, possibleColors.Count);
        ColorPallet pallet = possibleColors[palletIndex];
        int[] wireColors = new int[5];
        for (int i = 0; i < 5; i++) {
            wireColors[i] = i;
        }
        // shuffle the colors
        wireColors = wireColors.OrderBy(x => Random.value).ToArray();
        for (int i = 0; i < 5; i++) {
            wires[i].material.color = pallet.possibleColors[wireColors[i]].color;
            values[i] = pallet.possibleColors[wireColors[i]].value;
        }
        int[] lightColors = new int[5];
        for (int i = 0; i < 5; i++) {
            lightColors[i] = i;
        }
        // shuffle the colors
        lightColors = lightColors.OrderBy(x => Random.value).ToArray();
        for (int i = 0; i < 5; i++) {
            lights[i].material.color = pallet.possibleColors[lightColors[i]].color;
            values[i] += pallet.possibleColors[lightColors[i]].value;
        }

        
    }

    // Update is called once per frame
    void Update() {
        if (cutCount >= 5) {
            solved();
        }
    }

    public void tryCut(int index) {
        // if the value at index in values is the maximum value, then we disable the wire renderer
        if (values[index] == values.Max()) {
            wires[index].enabled = false;
            values[index] = -1;
            cutCount++;
        } else {
            fail();
        }
    }

}
