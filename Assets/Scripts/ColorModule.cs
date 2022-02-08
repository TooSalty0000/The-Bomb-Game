using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

public class ColorModule : Module
{
    [SerializeField]
    private Renderer answerLight;
    [SerializeField]
    private List<colorTypes> colors;
    // Start is called before the first frame update
    void Start()
    {
        answerLight.material.color = Color.red;
        colors = new List<colorTypes>();
    }

    // Update is called once per frame
    void Update()
    {
        if (colors.Count == 4) {
            checkAnswer();
            colors = new List<colorTypes>();
        }

    }

    private void checkAnswer()
    {
        if (ModuleManager.instance.modules.Any(x => x.GetComponent<CalculatorModule>())) {
            CalculatorModule[] modules = ModuleManager.instance.modules.Where(x => x.GetComponent<CalculatorModule>()).Select(x => x.GetComponent<CalculatorModule>()).ToArray();
            if (modules.Any(x => x.problemDigit == 235)) {
                // solved if the color sequence is red, green, yellow, blue
                if (colors.SequenceEqual(new List<colorTypes>() { colorTypes.red, colorTypes.green, colorTypes.yellow, colorTypes.blue })) {
                    solved();
                }
            } else if (modules.Any(x => x.problemDigit == 54)) {
                // solved if the color sequence is green, yellow, blue, red
                if (colors.SequenceEqual(new List<colorTypes>() { colorTypes.green, colorTypes.yellow, colorTypes.blue, colorTypes.red })) {
                    solved();
                }
            } else if (modules.Any(x => x.problemDigit == 419)) {
                solved();
            } else if (modules.Any(x => x.problemDigit == 303)) {
                // solved if the color sequence is green, green, green, green
                if (colors.SequenceEqual(new List<colorTypes>() { colorTypes.green, colorTypes.green, colorTypes.green, colorTypes.green })) {
                    solved();
                }
            } else if (modules.Any(x => x.problemDigit == 303)) {
                // solved if the color sequence is blue, blue, blue, blue
                if (colors.SequenceEqual(new List<colorTypes>() { colorTypes.blue, colorTypes.blue, colorTypes.blue, colorTypes.blue })) {
                    solved();
                }
            } else if (modules.Any(x => x.problemDigit == 102)) {
                // solved if the color sequence is yellow, yellow, yellow, yellow
                if (colors.SequenceEqual(new List<colorTypes>() { colorTypes.yellow, colorTypes.yellow, colorTypes.yellow, colorTypes.yellow })) {
                    solved();
                }
            } else if (modules.Any(x => x.problemDigit == 120)) {
                // solved if the color sequence is red, red, red, red
                if (colors.SequenceEqual(new List<colorTypes>() { colorTypes.red, colorTypes.red, colorTypes.red, colorTypes.red })) {
                    solved();
                }
            } 
        } // work on chemical module right after
    }

    public void enterColor(String colorName) {
        // add color to list from enum of red, blue, green, and yellow
        if (colorName == "Red") {
            colors.Add(colorTypes.red);
        } else if (colorName == "Blue") {
            colors.Add(colorTypes.blue);
        } else if (colorName == "Green") {
            colors.Add(colorTypes.green);
        } else if (colorName == "Yellow") {
            colors.Add(colorTypes.yellow);
        }
    }

    public enum colorTypes {
        red,
        blue,
        green,
        yellow
    }
}
