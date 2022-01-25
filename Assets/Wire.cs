using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : Interactable
{
    [SerializeField]
    private WireModule module;
    [SerializeField]
    private int index;
    public override void Interact()
    {
        module.tryCut(index);
    }
}
