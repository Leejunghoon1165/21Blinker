using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UISceneManager : MonoBehaviour
{
    
    private void Awake()
    {
        
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartButton()
    {
        Invoke("startgame", 0.2f);
    }

    public void LobbyStageButton()
    {
        Invoke("StageSelect", 0.1f);
    }

    public void StageSelect_1Button()
    {
        Invoke("Loadinggame", 0.1f);
    }

    public void BackLobbyStageButton()
    {
        Invoke("BackGameLobby",0.1f);
    }
    private void BackGameLobby()
    {
        SceneManager.LoadScene("GamelobbyScene");
    }

    private void startgame()
    {
        SceneManager.LoadScene("GamelobbyScene");
    }

    private void StageSelect()
    {
        SceneManager.LoadScene("StageSelect");
    }

    private void Loadinggame()
    {
        SceneManager.LoadScene("Loading");
    }
}
