using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleManager : MonoBehaviour
{
    public static ModuleManager instance;
    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(this);
        }
    }
    public List<Module> modules = new List<Module>();
    public float timeLimit = 300;
    [SerializeField]
    public TimeModule timeModule;


    // Start is called before the first frame update
    void Start()
    {
        timeModule.timer = timeLimit;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
