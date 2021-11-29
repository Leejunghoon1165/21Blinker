using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test2 : MonoBehaviour
{
    public static float PlayerHP;
    // Start is called before the first frame update

    void Start()
    {
        List<Dictionary<string, object>> data_Dialog = CSVReader.Read("playerdata_csv");
        PlayerHP = float.Parse((data_Dialog[2]["player_hp"].ToString()));
        //Debug.Log(PlayerHP);
        //for (int i = 0; i < data_Dialog.Count; i++)
        //{
        //    data_Dialog[i]["player_hp"].ToString();
        //}
        
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void test()
    {
        
    }
}
