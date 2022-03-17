using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BigButton : Interactable
{
    [SerializeField]
    private BigButtonModule module;
    private Animator animator;
    public float holdTime = 0f;
    public TextMeshPro text;

    private void Start() {
        animator = GetComponent<Animator>();
    }

    private void Update() {
        text.text = Mathf.FloorToInt(holdTime).ToString("0");
    }
    public override void Interact() {
        animator.SetBool("Pressed", true);
        holdTime = 0f;
    }

    public override void InteractHold() {
        holdTime += Time.deltaTime;
    }

    public override void InteractEnd() {
        animator.SetBool("Pressed", false);
        module.checkAnswer();
        holdTime = 0f;
    }
}
