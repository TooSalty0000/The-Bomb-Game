using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Module : MonoBehaviour
{
    [SerializeField]
    private Renderer Identifier;
    public bool isSolved = false;

    public void solved() {
        Identifier.material.color = Color.green;
        Identifier.material.SetColor("_EmissionColor", Color.green);
        isSolved = true;
        enabled = false;
       
    }

    public void fail () {
        if (!isSolved) {
            ModuleManager.instance.timeModule.addSpeedMultiplyer(0.5f);
        }
    }

}
