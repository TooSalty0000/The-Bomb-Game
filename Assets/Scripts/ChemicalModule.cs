using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChemicalModule : Module
{
    [SerializeField]
    private Renderer chemicalRenderer;

    [SerializeField]
    private TextMeshPro chemicalText;

    [SerializeField]
    private Color[] possibleChemicalColors;

    [SerializeField]
    private string[] possibleChemicalNames;
    
    private int colorIndex = -1;
    private int nameIndex = -1;
    private int targetPressCount = 0;
    private int pressCount = 0;

    private int[][] chemicalPressCounts = new int[][] {
        new int[] {2, 3, 4, 1, 1, 1, 1}, 
        new int[] {1, 2, 3, 4, 5, 5, 1}, 
        new int[] {1, 4, 2, 3, 4, 5, 1}, 
        new int[] {1, 5, 4, 2, 3, 4, 1}, 
        new int[] {1, 5, 5, 4, 2, 3, 2}, 
        new int[] {1, 1, 1, 1, 1, 2, 3}
    };

    // Start is called before the first frame update
    void Start() {
        setAnswer();
    }

    void setAnswer() {
        // set random chemical color and set colorIndex
        colorIndex = Random.Range(0, possibleChemicalColors.Length);
        chemicalRenderer.material.color = possibleChemicalColors[colorIndex];
        // set random chemical name and set nameIndex
        nameIndex = Random.Range(0, possibleChemicalNames.Length);
        chemicalText.text = possibleChemicalNames[nameIndex];

        // set targetPressCount
        targetPressCount = chemicalPressCounts[colorIndex][nameIndex];
    }

    public void press() {
        pressCount++;
    }

    public void submit() {
        if (pressCount == targetPressCount) {
            solved();
        } else {
            fail();
            pressCount = 0;
        }
    }
}
