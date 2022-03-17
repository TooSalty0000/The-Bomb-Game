using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterCube : Interactable
{
    // Start is called before the first frame update
    [SerializeField]
    private PasswordModule module;
    private bool isSpinning = false;
    public int index = 0;
    public float currentRotation = 0;

    private void Start() {
        transform.localEulerAngles = new Vector3(index * 90, 0, 0);

    }
    private void Update() {
        if (isSpinning) {
            currentRotation += Time.deltaTime * 90;
            transform.localEulerAngles = new Vector3(currentRotation, 0, 0);
            if ((index) * 90 - currentRotation <= 5f) {
                currentRotation = (index) * 90;
                transform.localEulerAngles = new Vector3(index * 90, 0, 0);
                if (currentRotation >= 360) {
                    currentRotation = 0;
                }
                if (index == 4) {
                    index = 0;
                }
                isSpinning = false;
            }
        }
    }

    public override void Interact()
    {
        if (!isSpinning) {
            module.addIndex(int.Parse(gameObject.name));
            index++;
            isSpinning = true;
        }

    }
}
