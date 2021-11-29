using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test2 : MonoBehaviour
{
    public static float player_hp;
    // Start is called before the first frame update

    private void Awake()
    {
        List<Dictionary<string, object>> data_Dialog = CSVReader.Read("playerdata_csv");
        player_hp = float.Parse((data_Dialog[0]["player_hp"].ToString()));
    }
    void Start()
    {    
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void test()
    {
        
    }
}
