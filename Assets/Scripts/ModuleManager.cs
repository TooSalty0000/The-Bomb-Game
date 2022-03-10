using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleManager : MonoBehaviour
{
    public static ModuleManager instance;

    public GameObject[] destroyThese;

    [SerializeField]
    private ParticleSystem explosionPrefab;
    public bool hasExploded = false;

    public int modulesSolved = 0;
    

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(this);
        }
    }
    public List<Module> modules = new List<Module>();
    public float timeLimit = 180;
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


        if(modulesSolved >= 5){

            Debug.Log("You Win!");
        }

        if(hasExploded == true){
            
            
            foreach(GameObject g in destroyThese){
                Destroy(g);
            }
             ParticleSystem ps = GetComponentInChildren<ParticleSystem>();
             ps.Play();
            StartCoroutine(WaitForExplosionStop());
             

             //return to main menu
        }
    }

    IEnumerator WaitForExplosionStop(){
        yield return new WaitForSeconds(1);
         ParticleSystem ps = GetComponentInChildren<ParticleSystem>();
             ps.Stop();
             hasExploded = false;
    }
}
