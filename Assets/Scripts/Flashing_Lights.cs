using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashing_Lights : MonoBehaviour
{
    [SerializeField]
    private ColorModule module;
    private Renderer renderer;
    private bool showing = false;

    private void Start() {
        renderer = GetComponent<Renderer>();
        StartCoroutine(showCode(module.code));
    }
    

    private IEnumerator showCode(Color[] code) {
        while (!module.isSolved) {
            yield return new WaitForSeconds(Random.Range(10, 15));
            for (int i = 0; i < code.Length; i++) {
                renderer.material.color = code[i];
                yield return new WaitForSeconds(0.3f);
                renderer.material.color = Color.white;
                yield return new WaitForSeconds(1f);
            }
            renderer.material.color = Color.white;
        }
    }

}
