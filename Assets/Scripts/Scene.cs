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
        //StartCoroutine(LoadSceneAsync4());

        //SceneManager.LoadScene("Player", LoadSceneMode.Additive);  //동기
        // SceneManager.LoadScene("Enemy", LoadSceneMode.Additive);
        //SceneManager.LoadScene("Item", LoadSceneMode.Additive);
    }
    IEnumerator LoadSceneAsync()
    {
        
        AsyncOperation AsyncLoad = SceneManager.LoadSceneAsync("Map2");
        AsyncLoad.allowSceneActivation = false;
        float timer = 0;
        while (!AsyncLoad.isDone)
        {
            yield return null;
            if (AsyncLoad.progress < 0.9f)
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
        //AsyncLoad.allowSceneActivation = false;
        //float timer = 0f;
        //while (!AsyncLoad.isDone)
        //{
        //    yield return null;
        //    if (AsyncLoad.progress < 0.9f)
        //{
        //    progressBar.fillAmount = AsyncLoad.progress;
        //}
        //else
        //{
        //    timer += Time.unscaledDeltaTime;
        //    progressBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);
        //    if (progressBar.fillAmount >= 1f)
        //    {
        //        AsyncLoad.allowSceneActivation = true;
        //        yield break;
        //    }

        //}
        //}

    }
    IEnumerator LoadSceneAsync2()  //비동기
    {
        AsyncOperation AsyncLoad2 = SceneManager.LoadSceneAsync("Player", LoadSceneMode.Additive);
        //AsyncLoad2.allowSceneActivation = false;
        //float timer = 0f;
        while (!AsyncLoad2.isDone)
        {
            yield return null;
            //if (AsyncLoad2.progress < 0.9f)
            //{
            //    progressBar.fillAmount = AsyncLoad2.progress;
            //}
            //else
            //{
            //    timer += Time.unscaledDeltaTime;
            //    progressBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);
            //    if (progressBar.fillAmount >= 1f)
            //    {
            //        AsyncLoad2.allowSceneActivation = true;
            //        yield break;
            //    }

            //}

        }
        
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

    //IEnumerator LoadSceneAsync4()  //비동기
    //{
    //    AsyncOperation AsyncLoad = SceneManager.LoadSceneAsync("Item", LoadSceneMode.Additive);
    //    while (!AsyncLoad.isDone)
    //    {
    //        yield return null;
    //    }
    //    Debug.Log(AsyncLoad.progress);
    //}

    // Update is called once per frame
    void Update()
    {
        
    }
}
