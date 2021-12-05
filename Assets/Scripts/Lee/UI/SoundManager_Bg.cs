using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager_Bg : MonoBehaviour
{
    public static SoundManager_Bg instance;
    public AudioSource AudS;
    #region singleton
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
    #endregion singleton
    // Start is called before the first frame update
    void Start()
    {
        Sound_Slider.Bg_Slider.value = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
