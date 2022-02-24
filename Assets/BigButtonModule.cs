using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BigButtonModule : Module
{
    // Start is called before the first frame update
    [SerializeField]
    private Texture2D[] symbols;
    [SerializeField]
    private Color[] colors;
    [SerializeField]
    private GameObject button;
    int colorId = 0;
    int symbolId = 0;


    void Start() {
        setButton();

    }

    private void setButton() {
        int symbolId = Random.Range(0, symbols.Length);
        button.GetComponent<Renderer>().material.mainTexture = symbols[symbolId];
        colorId = Random.Range(0, colors.Length);
        button.GetComponent<Renderer>().material.color = colors[colorId];
    }

    public void checkAnswer() {
        if (symbolId == 0) {
            float currentSeconds = ModuleManager.instance.timeModule.timer % 60;
            if (colorId == 0) {
                if (currentSeconds < 30) {
                    solved();
                }
            } else if (colorId == 1) {
                if (currentSeconds > 30) {
                    solved();
                }
            } else if (colorId == 2) {
                if (currentSeconds == 30) {
                    solved();
                }
            }
        } else if (symbolId == 1) {
            if (colorId == 0) {
                if (button.GetComponent<BigButton>().holdTime <= 1) {
                    solved();
                }
            } else if (colorId == 1) {
                if (ModuleManager.instance.modules.Count(x => x.isSolved) >= 2) {
                    solved();
                }
            } else if (colorId == 2) {
                if (ModuleManager.instance.modules.Count(x => x.isSolved) >= 1) {
                    solved();
                }
            }
        } else if (symbolId == 2) {
            if (button.GetComponent<BigButton>().holdTime >= 10) {
                solved();
            }
        } else if (symbolId == 3) {
            if (colorId == 0) {
                if (button.GetComponent<BigButton>().holdTime <= 1) {
                    solved();
                }
            } else if (colorId == 1) {
                if (button.GetComponent<BigButton>().holdTime >= 3) {
                    solved();
                }
            } else if (colorId == 2) {
                if (button.GetComponent<BigButton>().holdTime >= 5) {
                    solved();
                }
            }
        }
    }

    // Update is called once per frame
    void Update() {
        
    }
}
