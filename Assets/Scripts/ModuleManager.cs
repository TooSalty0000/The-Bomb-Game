using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public Module[] modules = new Module[0];
    [SerializeField]
    private List<Module> possibleModules = new List<Module>();

    public GameObject[] modualSpawners;
    [SerializeField]
    private float[] timeLimit = new float[] {5 * 60, 5 * 60, 4 * 60};
   
    public TimeModule timeModule;

    public GameObject MenuCamera;

    public bool YouWin = false;
    
    [SerializeField]
    AudioSource explode;
    [SerializeField]
    AudioSource doorClosing;

    // Start is called before the first frame update

    private bool animated = false;
    // Update is called once per frame
    void Update()
    {   
        if(modules.Count(x => x.isSolved) >= 5 && !animated){
           StartCoroutine(victory());
           animated = true;
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
        explode.Play();
        doorAnimator.SetTrigger("Close");
        doorClosing.Play();
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(0);
    }

    IEnumerator victory () {
        timeModule.activited = false;
        MenuCamera.SetActive(true);
        yield return new WaitForSeconds(2);
        doorAnimator.SetTrigger("Close");
        doorClosing.Play();
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(0);
    }

    public void startGame(int level) {

        modules = new Module[5];
        Menu.SetActive(false);

        for(int i = 0; i < 5; i++){
            Module newModule;
            if (level == 0) {
                newModule = Instantiate(possibleModules[Random.Range(0, 4)]);
            } else {
                if (i < 3) {
                    newModule = Instantiate(possibleModules[Random.Range(0, 5)]);
                } else {
                    newModule = Instantiate(possibleModules[Random.Range(5, 9)]);
                }
            }  
            newModule.transform.position = modualSpawners[i].transform.position;
            newModule.transform.localScale = modualSpawners[i].transform.localScale;
            newModule.transform.rotation = modualSpawners[i].transform.rotation;
            newModule.transform.parent = modualSpawners[i].transform;
            modules[i] = newModule;
        }

        timeModule.timer = timeLimit[level];
        if (level == 2) {
            timeModule.xNumber = 1;
        }

        // make sure that MenuCamera starts as inactive
        MenuCamera.SetActive(false);
        doorAnimator.SetTrigger("Open");
        timeModule.startTimer();
    }
}
