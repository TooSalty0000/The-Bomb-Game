using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorButton : Interactable
{
    [SerializeField]
    private ColorModule module;
    [SerializeField]
    private Animator animator;
    public override void Interact()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Normal")) {
            module.enterColor(gameObject.name);
            animator.SetTrigger(gameObject.name);
        }
        
    }
}
