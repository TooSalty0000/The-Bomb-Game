using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasswordSubmit : Interactable
{
    [SerializeField]
    private PasswordModule module;
    private Animator animator;
    private void Start() {
        animator = GetComponent<Animator>();
    }

    public override void Interact()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle")) {
            animator.SetTrigger("Submit");
            module.submit();
        }
    }
}
