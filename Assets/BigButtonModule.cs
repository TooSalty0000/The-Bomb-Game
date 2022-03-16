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
    private BigButton button;
    [SerializeField]
    private MeshRenderer buttonRenderer;
    int colorId = 0;
    int symbolId = 0;


    void Start() {
        setButton();

    }

    private void setButton() {
        symbolId = Random.Range(0, symbols.Length);
        buttonRenderer.material.mainTexture = symbols[symbolId];
        colorId = Random.Range(0, colors.Length);
        buttonRenderer.material.color = colors[colorId];
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
                if (button.holdTime <= 1) {
                    solved();
                }
            } else if (colorId == 1) {
                if (ModuleManager.instance.modules.Count(x => x.isSolved) >= 2) {
                    solved();
                }
            } else if (colorId == 2) {
                if (ModuleManager.instance.modules.Count(x => x.isSolved) == 1) {
                    solved();
                }
            }
        } else if (symbolId == 2) {
            if (colorId == 0) {
                if (Mathf.FloorToInt(button.holdTime) == 3) {
                    solved();
                }
            } else if (colorId == 1) {
                if (Mathf.FloorToInt(button.holdTime) == 5) {
                    solved();
                }
            } else if (colorId == 2) {
                if (Mathf.FloorToInt(button.holdTime) >= 10) {
                    solved();
                }
            }
        } else if (symbolId == 3) {
            if (colorId == 0) {
                if (button.holdTime <= 1) {
                    solved();
                }
            } else if (colorId == 1) {
                if (button.holdTime >= 3) {
                    solved();
                }
            } else if (colorId == 2) {
                if (button.holdTime >= 5) {
                    solved();
                }
            }
        }

        if (!isSolved) {
            fail();
        }
    }

    // Update is called once per frame
    void Update() {
        
    }
}
