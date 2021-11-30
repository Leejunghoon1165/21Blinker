using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSkill : MonoBehaviour
{
    public static float PlayermaxHPskill;
    public static float PlayercurrentHP;
    public static float PlayerhealHP;
    public static bool flag;
    // Start is called before the first frame update
    void Start()
    {
        PlayermaxHPskill = Test2.player_hp * 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        PlayercurrentHP = PlayerHpBar.currentHp;
    }

    public void lightskill()
    {
        StartCoroutine(OnSkill());
    }
    IEnumerator OnSkill()
    {
        EnemyFinder CHeck = GameObject.Find("Cube").GetComponent<EnemyFinder>();
        GameObject A = GameObject.Find("Plane");
        A.SetActive(false);
        CHeck.Look();
        yield return new WaitForSeconds(2f);
        A.SetActive(true);
        CHeck.UnLook();
    }

    public static void healskill()
    {
        flag = true;
        //Debug.Log(PlayercurrentHP);
       // Debug.Log(flag);
        PlayercurrentHP += PlayermaxHPskill;
        //Debug.Log(PlayercurrentHP);
        //Debug.Log(flag);
        //Debug.Log(PlayerHPskill);     
    }
    

}
