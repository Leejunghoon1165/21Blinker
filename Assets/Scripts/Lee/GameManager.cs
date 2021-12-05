using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public GameObject Gold_record;
    public float Gold = 0;
    public  float player_hp;
    private static GameManager _instance;
    public bool Time_start;   //남은 시간 타이머
    public bool Time_count;  //플레이 시간 측정
    public Text[] text_time;  //남은 시간 배열
    public Text[] text_PlayeTime;  //플레이 시간 배열
    public float time;  //남은 시간 
    public float time2;  //플레이 시간

    public bool Survival = true;

    public bool Pause_Bt = false;


    public GameObject EndingCanvas;
    public GameObject Ending_DIE;
    public GameObject Ending_Win;


    public static GameManager Instance
    {
        get
        {
            if(!_instance)
            {
                _instance = FindObjectOfType(typeof(GameManager)) as GameManager;

                if (_instance == null)
                    Debug.Log("no Singleton obj");
            }
            return _instance;
        }
    }
    
   
    
  
    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }
        else if(_instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(Gold_record);
        List<Dictionary<string, object>> data_Dialog = CSVReader.Read("playerdata_csv");
        player_hp = float.Parse((data_Dialog[0]["player_hp"].ToString()));
        StartEndScene();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TimeTest();
        EndScene();
    }
    void StartEndScene()
    {
        EndingCanvas.SetActive(false);
    }

    void EndScene()
    {
        
        //if(Survival)
        //{
        //    EndingCanvas.SetActive(true);
        //    Ending_DIE.SetActive(false);
            
        //}
        //else if(!Survival)
        //{
        //    EndingCanvas.SetActive(true);
        //    Ending_Win.SetActive(false);
        //}
    }

    void TimeTest()
    {
        if(Time_start)
        {
            time -= Time.deltaTime;
            text_time[0].text = ((int)time / 60 % 60).ToString();
            text_time[1].text = ((int)time % 60).ToString();
            text_time[2].text = ((int)time / 60 % 60).ToString();
            text_time[3].text = ((int)time % 60).ToString();

        } 
        //플레이어가 죽었을 경우 시간 나타냄
        else if(!Time_start)
        {
            text_time[0].text = ((int)time / 60 % 60).ToString();
            text_time[1].text = ((int)time % 60).ToString();
            text_time[2].text = ((int)time / 60 % 60).ToString();
            text_time[3].text = ((int)time % 60).ToString();
        }

        if(Time_count)
        {
            time2 += Time.deltaTime;
            
        }
        else if(!Time_count)
        {
            text_PlayeTime[0].text = ((int)time2 / 60 % 60).ToString();
            text_PlayeTime[1].text = ((int)time2 % 60).ToString();
        }     
        
    }

}
