using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    [SerializeField]
    public Camera camera;

    private Interactable currentInteractable;

    private bool isPressing = false;
    [SerializeField]
    private GameObject mouseParticle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitx)) {
            Instantiate(mouseParticle, hitx.point, Quaternion.identity);
        }
        if (Input.GetMouseButton(0)) {
            if (!isPressing) {
                if (Physics.Raycast(ray, out RaycastHit hit)) {
                    Interactable interactable = hit.collider.GetComponent<Interactable>();
                    if (interactable != null) {
                        currentInteractable = interactable;
                        interactable.Interact();
                        isPressing = true;
                    }
                }
            } else {
                if (currentInteractable != null) {
                    currentInteractable.InteractHold();
                }
            }
        } else {
            if (isPressing) {
                isPressing = false;
                if (currentInteractable != null) {
                    currentInteractable.InteractEnd();
                    currentInteractable = null;
                }
            }
        }
    }
}
