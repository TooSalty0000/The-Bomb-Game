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
    private MeshRenderer renderer;

    private void Start() {
        renderer = GetComponent<MeshRenderer>();
        renderer.material.EnableKeyword("_EMISSION");
    }

    private void Update() {
        if (renderer.material.GetColor("_EmissionColor") != Color.black) {
            renderer.material.SetColor("_EmissionColor", Color.Lerp(renderer.material.GetColor("_EmissionColor"), Color.black, Time.deltaTime * 2));
        }
    }

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
            renderer.material.SetColor("_EmissionColor", Color.white);
        } else {
            module.fail();
        }
    }
}
