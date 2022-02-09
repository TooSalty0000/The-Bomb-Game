using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorButton : Interactable
{
    [SerializeField]
    private ColorModule module;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private int colorId;
    public override void Interact()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Normal") && module.enabled) {
            module.enterColor(colorId);
            animator.SetTrigger(gameObject.name);
        }
        
    }
}
