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
    private Color[] Colors = new Color[4];
    public Color[] code {private set; get;} = new Color[4];
    private Color[] answer = new Color[4];
    [SerializeField]
    private Color[] enteredCode = new Color[4];
    private int index = 0;


    // Start is called before the first frame update
    void Start()
    {
        answerLight.material.color = Color.white;
        answer = new Color[4];
        setAnswer();
        index = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //blueprint for later
    //method startPattern
    //select random number then depending on number and problem digit then make color blink specific pattern of colors in intervals of 1 second per color.
    //Alternative: press button to make simon says sequence play once.

    private void setAnswer()
    {
        // set code to random colors
        code = new Color[4];
        for (int i = 0; i < code.Length; i++)
        {
            code[i] = Colors[UnityEngine.Random.Range(0, Colors.Length)];
        }

        // set answer based on these rules:
//         First Color:
//          If Red: Press the third color
//          If Blue: Press the second color
//          If Green or Yellow: Press the fourth color
//         Second Color:
//          If Red or Blue: Press the first color
//          If Green or Yellow: Press the fourth color
//         Third Color: 
//          Press the third color
//         Fourth Color:
//          If Red: Press Blue
//          If Green: Press Red
//          If Blue or Yellow: Press Yellow
        if (code[0] == Color.red)
        {
            answer[0] = code[2];
        } else if (code[0] == Color.blue)
        {
            answer[0] = code[1];
        } else
        {
            answer[0] = code[3];
        }
        if (code[1] == Color.red || code[1] == Color.blue)
        {
            answer[1] = code[0];
        } else
        {
            answer[1] = code[3];
        }
        answer[2] = code[2];
        if (code[3] == Color.red)
        {
            answer[3] = Color.blue;
        } else if (code[3] == Color.green)
        {
            answer[3] = Color.red;
        } else
        {
            answer[3] = new Color(1f, 1f, 0);
        }

    }

    private bool checkAnswer() {
        // if enteredCode matches answer then executed solved()
        if (enteredCode.SequenceEqual(answer))
        {
            solved();
            return true;
        }
        return false;
    }

    public async void enterColor(int colorCode) {
        enteredCode[index] = Colors[colorCode];
        // // debug.log all colors in enteredCode
        // for (int i = 0; i < enteredCode.Length; i++)
        // {
        //     Debug.Log("Entered color " + i + ": " + enteredCode[i]);
        //     Debug.Log("Answer: " + answer[i]);
        //     Debug.Log(enteredCode[i] == answer[i]);
        // }
        index++;
        if (index >= 4) {
            if (!checkAnswer()) {
                index = 0;
                enteredCode = new Color[4];
            }
        }
    }

    
}
