using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadSceneAsync());
        StartCoroutine(LoadSceneAsync2());
        
        //SceneManager.LoadScene("Player", LoadSceneMode.Additive);  //동기
        // SceneManager.LoadScene("Enemy", LoadSceneMode.Additive);
        //SceneManager.LoadScene("Item", LoadSceneMode.Additive);
    }

    IEnumerator LoadSceneAsync()  //비동기
    {
        AsyncOperation AsyncLoad = SceneManager.LoadSceneAsync("Player",LoadSceneMode.Additive);
        while (!AsyncLoad.isDone)
        {
            yield return null;
        }
        Debug.Log(AsyncLoad.progress);
        //안되면 주석지우셈
        /*AsyncOperation*/  /*AsyncLoad = SceneManager.LoadSceneAsync("Item", LoadSceneMode.Additive);
       while (!AsyncLoad.isDone)
        {
            yield return null;
        }
        Debug.Log(AsyncLoad.progress);*/
    }

    IEnumerator LoadSceneAsync2()  //비동기
    {
        AsyncOperation AsyncLoad = SceneManager.LoadSceneAsync("Enemy", LoadSceneMode.Additive);
        while (!AsyncLoad.isDone)
        {
            yield return null;
        }
        Debug.Log(AsyncLoad.progress);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
