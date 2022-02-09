using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour 
{
    /*
        If there is a module where the user's hand can interact, (continue to operate the function). 

        (continue to operate the function):
        - Detect and store exactly which object the user's hand operates on.
        - Call on the corresponding script of any given object to operate on it.
    */
    private float startTime = 0f;
    private float endTime = 0f;
    public virtual void Interact() {
        Debug.Log("Interacting with " + gameObject.name);

        if (Input.GetMouseButtonDown(0))
            startTime = Time.time;

        if (Input.GetMouseButtonUp(0))
            endTime = Time.time;
        
        if (endTime - startTime > 0.5) {
            startTime = 0;
            endTime = 0;
        }
    }

    public virtual void InteractHold() {

    }
}
