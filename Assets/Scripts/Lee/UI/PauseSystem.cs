using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseSystem : MonoBehaviour
{
    // Start is called before the first frame update
    
    public GameObject Pause_Panel;
    void Start()
    {
        Pause_Panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Button_Check();
    }

    public void Button_Check()
    {
        if(!GameManager.Instance.Pause_Bt)
        {
            PauseOn_Check();
        }
        else
        {
            PauseExit_Check();
        }
           
    }

    public void PauseOn_Check()
    {
        GameManager.Instance.Pause_Bt = true;
        Pause_Panel.SetActive(true);
        Time.timeScale = 0f;

    }
    public void PauseExit_Check()
    {
        GameManager.Instance.Pause_Bt = false;
        Pause_Panel.SetActive(false);
        Time.timeScale = 1f;
    }

  


}
