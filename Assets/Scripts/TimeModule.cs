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
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime * speedmultiplyer;
        //display time as minutes:seconds and store it in timerText
        timerText.text = Mathf.Floor(timer / 60).ToString("00") + ":" + Mathf.Floor(timer % 60).ToString("00");
        if(timer <= 0){
            // Reload scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }
}
