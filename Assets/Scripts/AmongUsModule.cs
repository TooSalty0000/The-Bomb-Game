using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AmongUsModule : Module
{
    [SerializeField]
    private Transform[] spinners = new Transform[3];
    [SerializeField]
    private MeshRenderer[] spinnerRenderers = new MeshRenderer[3];
    private bool[] isSpinning = new bool[3];
    [SerializeField]
    private Color[] colorOrders = new Color[9];

    private int[] colorOrder = new int[3];
    
    private void Start() {
        setColors();
    }

    private void setColors() {
        // select three colors without repeats
        int[] colors = new int[3];
        for (int i = 0; i < 3; i++) {
            int color = Random.Range(0, 9);
            while (colors.Contains(color)) {
                color = Random.Range(0, 9);
            }
            colors[i] = color;
        }
        for (int i = 0; i < 3; i++) {
            colorOrder[i] = colors[i];
            spinnerRenderers[i].material.color = colorOrders[colors[i]];
            spinnerRenderers[i].material.SetColor("_EmissionColor", colorOrders[colorOrder[i]]);
        }

        // set isSpinning to true
        for (int i = 0; i < 3; i++) {
            isSpinning[i] = true;
        }

        //set spinner rotation to random
        for (int i = 0; i < 3; i++) {
            spinners[i].localRotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        }
    }

    private void Update() {
        for (int i = 0; i < isSpinning.Length; i++)
        {
            if (isSpinning[i])
            {
                spinners[i].Rotate(0, 0, -Time.deltaTime * 100);
            }
        }
    }

    public void pressed(int num) {
        Debug.Log("pressed " + Mathf.Abs(spinners[num].localRotation.eulerAngles.z - 45));
        Debug.Log(colorOrder[num]);
        if (colorOrder.Max() == colorOrder[num] && Mathf.Abs(spinners[num].localRotation.eulerAngles.z - 45) < 20) {
            isSpinning[num] = false;
            colorOrder[num] = -1;
        } else {
            // play fail sound
            fail();
        }

        if (colorOrder.All(x => x == -1)) {
            solved();
        }
    }

}
