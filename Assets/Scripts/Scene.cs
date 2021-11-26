using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    [SerializeField]
    Image progressBar;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadSceneAsync());
        StartCoroutine(LoadSceneAsync2());
        StartCoroutine(LoadSceneAsync3());
        
        //SceneManager.LoadScene("Player", LoadSceneMode.Additive);  //동기
        // SceneManager.LoadScene("Enemy", LoadSceneMode.Additive);
        //SceneManager.LoadScene("Item", LoadSceneMode.Additive);
    }
    IEnumerator LoadSceneAsync()
    {
        AsyncOperation AsyncLoad = SceneManager.LoadSceneAsync("Player");
        AsyncLoad.allowSceneActivation = false;
        float timer = 0f;
        while (!AsyncLoad.isDone)
        {
            yield return null;
            if(AsyncLoad.progress < 0.9f)
            {
                progressBar.fillAmount = AsyncLoad.progress;
            }
            else
            {
                timer += Time.unscaledDeltaTime;
                progressBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);
                if (progressBar.fillAmount >= 1f)
                {
                    AsyncLoad.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
    IEnumerator LoadSceneAsync2()  //비동기
    {
        AsyncOperation AsyncLoad = SceneManager.LoadSceneAsync("Test", LoadSceneMode.Additive);
        while (!AsyncLoad.isDone)
        {
            yield return null;
        }
        

        //안되면 주석지우셈
        /*AsyncOperation*/  /*AsyncLoad = SceneManager.LoadSceneAsync("Item", LoadSceneMode.Additive);
       while (!AsyncLoad.isDone)
        {
            yield return null;
        }
        Debug.Log(AsyncLoad.progress);*/
    }

    IEnumerator LoadSceneAsync3()  //비동기
    {
        AsyncOperation AsyncLoad = SceneManager.LoadSceneAsync("Enemy", LoadSceneMode.Additive);
        while (!AsyncLoad.isDone)
        {
            yield return null;
        }
        Debug.Log(AsyncLoad.progress);
    }

    IEnumerator LoadSceneAsync3()  //비동기
    {
        AsyncOperation AsyncLoad = SceneManager.LoadSceneAsync("Item", LoadSceneMode.Additive);
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
