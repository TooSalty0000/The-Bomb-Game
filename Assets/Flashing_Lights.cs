using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashing_Lights : MonoBehaviour
{

    
    public List<int> Color_Order = new List<int>();
    [SerializeField]
    private Color[] Colors = new Color[4];
    // Start is called before the first frame update
    void Start()
    {
    //  Random.Range(Color_Order , 0, 4);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
