using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player_Button : MonoBehaviour
{
    private float clickTime;
    public float minClickTime = 1;
    private bool isClick;

    public void ButtonDown()
    {
        isClick = true;   
    }

    public void ButtonUp()
    {
        isClick = false;
        print(clickTime);

        if(clickTime >= minClickTime)
        {
            
            
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isClick)
        {
            clickTime += Time.deltaTime;
        }
        else
        {
            clickTime = 0;
        }
        
           
    }
}
