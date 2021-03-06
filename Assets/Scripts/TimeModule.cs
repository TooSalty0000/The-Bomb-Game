using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class TimeModule : Module
{
    public float timer; 
    public TextMeshPro timerText;
    private float speedmultiplyer = 1;

    public int xNumber = 0;

    public GameObject[] x;
    public bool activited = false;
    
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < x.Length; i++)
        {
            x[i].GetComponent<TextMeshPro>().color = Color.white;
        }

        audioSource = GetComponent<AudioSource>();
     
        
    }

    // Update is called once per frame
    void Update()
    {
        if (activited) {
            timer -= Time.deltaTime * speedmultiplyer;
        }
        //display time as minutes:seconds and store it in timerText
        timerText.text = Mathf.Floor(timer / 60).ToString("00") + ":" + Mathf.Floor(timer % 60).ToString("00");

        if(timer <= 0 || xNumber > 2) {
            // Reload scene
           ModuleManager.instance.hasExploded = true;
           enabled = false;
        }

        for (int i = 0; i < xNumber; i++)
        {
            if (i < x.Length) {
                x[i].GetComponent<TextMeshPro>().color = Color.red;
            }
        }
    }

    public void addSpeedMultiplyer(float speedmultiplyer){
        this.speedmultiplyer += speedmultiplyer;
        xNumber++;       
        
    }
    
    public void startTimer() {
        activited = true;
        StartCoroutine(WaitForSound());
    }

    private IEnumerator WaitForSound()
    {
        while (timer > 0 && activited) {
            yield return new WaitForSeconds(1 / speedmultiplyer);
            audioSource.Play();
        }
    }
}
