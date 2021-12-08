using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class System_Button : MonoBehaviour
{
    public GameObject SystemPanel;
    public bool System_Bt = false;
    [SerializeField]
    private string Button_sd2;
    // Start is called before the first frame update
    void Start()
    {
        SystemPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void System_ButtonCheck()
    {
        if (!System_Bt)
        {
            System_Panel_On();
        }
        else
        {
            System_Panel_Off();
        }
            
    }

    public void System_Panel_On()
    {
        SoundManager_Bg.instance.PlaySE(Button_sd2);
        System_Bt = true;
        SystemPanel.SetActive(true);
    }
    public void System_Panel_Off()
    {
        SoundManager_Bg.instance.PlaySE(Button_sd2);
        System_Bt = false;
        SystemPanel.SetActive(false);
    }
}
