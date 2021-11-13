using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Button()
    {
        Invoke("startgame", 0.2f);
    }
    private void startgame()
    {
        SceneManager.LoadScene("Loading");
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
