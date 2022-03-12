using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grids : Interactable
{
    [SerializeField]
    private GridModule module;
    public bool X = false;
    public bool O = false;
    public bool activated = false;

    [SerializeField]
    private Texture2D XTexture;
    [SerializeField]
    private Texture2D OTexture;

    public void showTextures() {
        if (X) {
            GetComponent<Renderer>().material.mainTexture = XTexture;
        }
    }

    public override void Interact()
    {
        if (!activated && O) {
            activated = true;
            GetComponent<MeshRenderer>().material.mainTexture = OTexture;
            module.checkAnswer();
        } else {
            module.fail();
        }
    }
}
