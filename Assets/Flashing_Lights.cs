using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashing_Lights : Interactable
{
    [SerializeField]
    private ColorModule module;
    private Renderer renderer;
    private bool showing = false;

    private void Start() {
        renderer = GetComponent<Renderer>();
    }
    public override void Interact()
    {
        if (!showing) {
            showing = true;
            StartCoroutine(showCode(module.code));
        }
    }

    private IEnumerator showCode(Color[] code) {
        for (int i = 0; i < code.Length; i++) {
            renderer.material.color = code[i];
            yield return new WaitForSeconds(0.3f);
            renderer.material.color = Color.white;
            yield return new WaitForSeconds(1f);
        }
        renderer.material.color = Color.white;
        showing = false;
    }

}
