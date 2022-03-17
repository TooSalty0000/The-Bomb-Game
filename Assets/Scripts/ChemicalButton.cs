using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChemicalButton : Interactable
{
    [SerializeField]
    private ChemicalModule module;
    private Animator animator;
    private void Start() {
        animator = GetComponent<Animator>();
    }
    public override void Interact() {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle")) {
            module.press();
            animator.ResetTrigger("Pressed");
            animator.SetTrigger("Pressed");
        }
    }
}
