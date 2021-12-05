using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sound_Slider : MonoBehaviour
{
    public static Slider Bg_Slider;
    // Start is called before the first frame update
    void Start()
    {
        Bg_Slider.value = SoundManager_Bg.instance.AudS.volume;
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
