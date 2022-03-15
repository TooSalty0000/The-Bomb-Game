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

    private int xNumber = 0;

    public GameObject[] x;
    public bool activited = false;
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < x.Length; i++)
        {
            x[i].GetComponent<TextMeshPro>().color = Color.white;
        }


        
    }

    // Update is called once per frame
    void Update()
    {
        if (activited) {
            timer -= Time.deltaTime * speedmultiplyer;
        }
        //display time as minutes:seconds and store it in timerText
        timerText.text = Mathf.Floor(timer / 60).ToString("00") + ":" + Mathf.Floor(timer % 60).ToString("00");
        if(timer <= 0){
            // Reload scene
           ModuleManager.instance.hasExploded = true;
        }
    }

    public void addSpeedMultiplyer(float speedmultiplyer){
        this.speedmultiplyer += speedmultiplyer;
         
         //play fail sound

        if(xNumber < 2){
            
          x[xNumber].GetComponent<TextMeshPro>().color = Color.red;

           xNumber++;
          
        }
        else 
        {
            ModuleManager.instance.hasExploded = true;
           
        }

       
        
    }
}
