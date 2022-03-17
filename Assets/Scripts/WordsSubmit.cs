using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordsSubmit : Interactable
{
    [SerializeField]
    private WordsModule module;
    private Animator animator;
    private void Start() {
        animator = GetComponent<Animator>();
    }
    public override void Interact() {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Submit Idle")) {
            animator.SetTrigger("Pressed");
            module.submit();
        }
    }
}
