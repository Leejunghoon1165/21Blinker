using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundButton_1 : MonoBehaviour
{
    public GameObject SoundOn_bg;
    public GameObject SoundOff_bg;
    public GameObject SoundOn_effect;
    public GameObject SoundOff_effect;

    public GameObject SoundBg_Value;
    public GameObject SoundEfeect_Value;


    // Start is called before the first frame update
    void Start()
    {
        SoundEfeect_Value.SetActive(false);
        SoundBg_Value.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SoundOn_Effect()
    {
        SoundOff_effect.SetActive(true);
        SoundOn_effect.SetActive(false);
    }

    public void SoundOff_Effect()
    {
        SoundOn_effect.SetActive(true);
        SoundOff_effect.SetActive(false);
    }

    public void SoundOn_Bg()
    {
        SoundOff_bg.SetActive(true);
        SoundManager_Bg.instance.AudS.volume = 0; 
        SoundOn_bg.SetActive(false);
    }

    public void SoundOff_Bg()
    {
        SoundOn_bg.SetActive(true);
        SoundManager_Bg.instance.AudS.volume = 1;
        SoundOff_bg.SetActive(false);
    }

    public void SoundBg_Click()
    {
        SoundEfeect_Value.SetActive(false);
        SoundBg_Value.SetActive(true);
    }

    public void SoundEffect_Click()
    {
        SoundBg_Value.SetActive(false);
        SoundEfeect_Value.SetActive(true);

    }
}
