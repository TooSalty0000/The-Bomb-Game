using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChemicalSubmit : Interactable
{
    [SerializeField]
    private ChemicalModule module;
    private Animator animator;

    private void Start() {
        animator = GetComponent<Animator>();
    }
    
    public override void Interact() {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("SubmitIdle")) {
            module.submit();
            animator.ResetTrigger("Pressed");
            animator.SetTrigger("Pressed");
        }
    }
}
