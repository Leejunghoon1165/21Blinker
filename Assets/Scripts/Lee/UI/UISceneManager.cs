using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UISceneManager : MonoBehaviour
{

    [SerializeField]
    private string Button_sd;
    private void Awake()
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
        // Invoke("BackGameLobby",0.1f);
        SceneManager.LoadScene("GamelobbyScene");
    }
    
    public void PauseButton()
    {
        SoundManager_Bg.instance.PlaySE(Button_sd);
        GameManager.Instance.Pause_Bt = true;
    }

    private void BackGameLobby()
    {
        
        SoundManager_Bg.instance.PlaySE(Button_sd);
        
    }

    private void startgame()
    {
        SceneManager.LoadScene("GamelobbyScene");
    }

    private void StageSelect()
    {
        SoundManager_Bg.instance.PlaySE(Button_sd);
        SceneManager.LoadScene("StageSelect");
    }

    private void Loadinggame()
    {
        SoundManager_Bg.instance.PlaySE(Button_sd);
        SceneManager.LoadScene("Loading");
    }
}
