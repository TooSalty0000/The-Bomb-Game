using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WordsModule : Module
{
    private int value = 1;
    [SerializeField]
    private TextMeshPro valueText;
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        valueText.text = value.ToString();
    }

    public void pressed(string direction) {
        if (direction == "Up") {
            if (value < 9) {
                value++;
            }
        } else if (direction == "Down") {
            if (value > 1) {
                value--;
            }
        }
    }
}
