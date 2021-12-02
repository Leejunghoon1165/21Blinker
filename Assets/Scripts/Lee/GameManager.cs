using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static float player_hp;
    public GameManager gamemanager;
    private void Awake()
    {
        DontDestroyOnLoad(gamemanager);
        List<Dictionary<string, object>> data_Dialog = CSVReader.Read("playerdata_csv");
        player_hp = float.Parse((data_Dialog[0]["player_hp"].ToString()));
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
