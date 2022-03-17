using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordButton : Interactable
{
    [SerializeField]
    private WordsModule module;
    private Animator animator;
    private void Start() {
        animator = transform.parent.GetComponent<Animator>();
    }
    public override void Interact() {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle")) {
            animator.SetTrigger(gameObject.name);
            module.pressed(gameObject.name);
        }
    }
}
