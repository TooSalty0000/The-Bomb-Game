using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WordsModule : Module
{
    private int level = 1;
    [SerializeField]
    private TextMeshPro valueText;
    [SerializeField]
    private TextMeshPro problemText;
    [SerializeField]
    private string[] words = new string[] {
        "BOMB", "CUT", "CHEM", "TEMP", "WIRE", "EXPLODE", "DROP", "COLOR", "AMONGUS", "BUTTON"
    };

    private int savedNumber;

    private string currentWord;
    private int answerValue;
    
    private int value = 0;
    int randomIndex = 0;
    // Start is called before the first frame update
    void Start() {
        setAnswerValue();
        
    }

    public void submit() {
        
        checkAnswer();
    }

    void setAnswerValue() {
        randomIndex = Random.Range(0, words.Length);
        problemText.text = words[randomIndex];
        switch(level){
            case 1:
                answerValue = randomIndex;
                break;
            case 2:
                answerValue = words[randomIndex].Length - 3;
                break;
            case 3:
                answerValue = (words[randomIndex].Length + savedNumber) % 10;
                break;
        }
    }

    void checkAnswer() {
        if (answerValue == value) {
            if (level < 3) {
                savedNumber += answerValue;
                level++;
                setAnswerValue();
            } else {
                solved();
            }
        } else {
            fail();
        }
    }

    public void pressed(string direction) {
        if (direction == "Up" && value < 9) {
            value++;
        } else if (direction == "Down" && value > 0) {
            value--;
        }
        valueText.text = value.ToString();
    }

}