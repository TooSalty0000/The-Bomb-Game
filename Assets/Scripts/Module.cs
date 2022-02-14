using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Module : MonoBehaviour
{
    [SerializeField]
    private Renderer Identifier;

    public void solved() {
        Identifier.material.color = Color.green;
        enabled = false;
    }

}
