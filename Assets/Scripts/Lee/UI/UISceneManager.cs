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
        SceneManager.LoadScene("GamelobbyScene");
    }

    public void LobbyStageButton()
    {
        SoundManager_Bg.instance.PlaySE(Button_sd);
        SceneManager.LoadScene("StageSelect");
    }

    public void StageSelect_1Button()
    {
        SoundManager_Bg.instance.PlaySE(Button_sd);
        SceneManager.LoadScene("Loading");
        
    }

    public void BackLobbyStageButton()
    {
        // Invoke("BackGameLobby",0.1f);
        SoundManager_Bg.instance.PlaySE(Button_sd);
        SceneManager.LoadScene("GamelobbyScene");
    }
    
    public void PauseButton()
    {
        SoundManager_Bg.instance.PlaySE(Button_sd);
        GameManager.Instance.Pause_Bt = true;
    }

    private void startgame()
    {
        
    }

}
