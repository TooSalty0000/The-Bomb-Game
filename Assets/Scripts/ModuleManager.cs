using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
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
    private GameObject Menu;
    [SerializeField]
    private Animator doorAnimator;
    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(this);
        }
    }
    public List<Module> modules = new List<Module>();
    [SerializeField]
    private List<Module> possibleModules = new List<Module>();

    public GameObject[] modualSpawners;
    public float timeLimit = 180;
   
    public TimeModule timeModule;

    public GameObject MenuCamera;

    public bool YouWin = false;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
      audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {   
        if(modulesSolved >= 5 || YouWin == true){

           MenuCamera.gameObject.SetActive(true);

           winText.text = "You Defused The Bomb!";
        }

        if(hasExploded == true){
            StartCoroutine(Explode());
            hasExploded = false;
            //return to main menu
        }
    }

    IEnumerator Explode(){
        MenuCamera.SetActive(true);
        yield return new WaitForSeconds(2);
        foreach(GameObject g in destroyThese){
            Destroy(g);
        }
        ParticleSystem ps = GetComponentInChildren<ParticleSystem>();
        ps.Play();
        audioSource.Play();
        doorAnimator.SetTrigger("Close");
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(0);
    }

    public void startGame() {
        winText.text = "";

        timeModule.timer = timeLimit;
        Menu.SetActive(false);

        // make sure that MenuCamera starts as inactive
        MenuCamera.SetActive(false);
        doorAnimator.SetTrigger("Open");
        
        for(int i = 0; i < 5; i++){
            Module newModule = Instantiate(possibleModules[Random.Range(0, possibleModules.Count)]);
            newModule.transform.position = modualSpawners[i].transform.position;
            newModule.transform.localScale = modualSpawners[i].transform.localScale;
            newModule.transform.rotation = modualSpawners[i].transform.rotation;
            newModule.transform.parent = modualSpawners[i].transform;
            modules.Add(newModule);
        }
        timeModule.startTimer();
    }
}
