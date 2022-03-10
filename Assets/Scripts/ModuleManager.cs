using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ModuleManager : MonoBehaviour
{
    public static ModuleManager instance;

    public GameObject[] destroyThese;

    [SerializeField]
    private ParticleSystem explosionPrefab;
    public bool hasExploded = false;

    public int modulesSolved = 0;

    public TextMeshProUGUI winText; 
    
[SerializeField]
    private GameObject VirtualCamera;
    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(this);
        }
    }
    public List<Module> modules = new List<Module>();
    public float timeLimit = 180;
   
    public TimeModule timeModule;

    public GameObject MenuCamera;

    public bool YouWin = false;

    // Start is called before the first frame update
    void Start()
    {
     winText.text = "";

        timeModule.timer = timeLimit;

        // make sure that MenuCamera starts as inactive
        MenuCamera.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {   


        if(modulesSolved >= 5 || YouWin == true){

           MenuCamera.gameObject.SetActive(true);

           winText.text = "You Defused The Bomb!";
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
