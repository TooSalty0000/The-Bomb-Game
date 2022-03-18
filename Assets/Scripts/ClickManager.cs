using UnityEngine;
using TMPro;

public class ClickManager : MonoBehaviour
{
    [SerializeField]
    public Camera camera;
    [SerializeField]
    public TextMeshProUGUI text;

    private Interactable currentInteractable;

    private bool isPressing = false;
    [SerializeField]
    private GameObject mouseParticle;
    [SerializeField]
    private bool useMouse = false;
    private HandMotionDetection handMotion;

    // Start is called before the first frame update
    void Start()
    {
        handMotion = GetComponent<HandMotionDetection>();
        ChangeUseMouse();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray;
        if (useMouse) {
            ray = camera.ScreenPointToRay(Input.mousePosition);
        } else {    
            ray = camera.ScreenPointToRay(handMotion.handPosition);
        }
        if (Physics.Raycast(ray, out RaycastHit hitx)) {
            Instantiate(mouseParticle, hitx.point, Quaternion.identity);
        }
        if ((!useMouse && handMotion.pressed) || (useMouse && Input.GetMouseButton(0))) {
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

    public void ChangeUseMouse() {
        useMouse = !useMouse;
        if (useMouse) {
            text.text = "Mouse";
        } else {
            text.text = "Hand";
        }
    }
    public void quitApp() {
        Application.Quit();
    }
}
