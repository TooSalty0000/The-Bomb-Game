using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmongUsButton : Interactable
{
    [SerializeField]
    private AmongUsModule module;
    private Animator animator;
    [SerializeField]
    private int index;
    // Start is called before the first frame update
    void Start()
    {
        animator = transform.parent.GetComponent<Animator>();
    }

    public override void Interact()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("idle"))
        {
            animator.SetTrigger("Pressed");
            module.pressed(index);
        }
    }
}
