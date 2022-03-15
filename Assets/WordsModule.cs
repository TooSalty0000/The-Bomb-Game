using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WordsModule : Module
{
    private int level = 1;
    [SerializeField]
    private TextMeshPro valueText;
    private TextMeshPro problemText;
    private string[] words = new string[] {
        "BOMB", "CUT", "CHEM", "TEMP", "WIRE", "EXPLODE", "DROP", "COLOR"
    };

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
        switch(level){
            case 1:
                randomIndex = Random.Range(0, words.Length);
                answerValue = randomIndex;
                break;
            case 2:
                answerValue = words[randomIndex].Length - 3;
                break;
            case 3:
                if(randomIndex == 2 || randomIndex == 7 || randomIndex == 3 || randomIndex == 4){
                    answerValue = randomIndex;
                }else if(randomIndex == 1 || randomIndex == 6 || randomIndex == 5){
                    answerValue = 3;
                }else if(randomIndex == 0){
                    answerValue = 9;
                }
                break;
        }
    }

    void checkAnswer() {
        if (answerValue == value) {
            if (level < 2) {
                level++;
            } else {
                solved();
            }
        } else {
            fail();
        }
    }

}