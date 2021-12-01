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
    private void Awake()
    {
       
    }
    // Update is called once per frame
    void Update()
    {
       
    }

    public void lightskill()
    {
        StartCoroutine(OnSkill());
    }
    IEnumerator OnSkill()
    {
        //랜덤탐색후 배열에 담음
        GameObject[] Enemylist = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject B = GameObject.Find("Plane");
        B.SetActive(false);
        for (int i = 0; i < Enemylist.Length; i++)
        {
            Enemylist[i].GetComponent<EnemyFinder>().Look();
        }
        yield return new WaitForSeconds(2f);
        B.SetActive(true);
        for (int i = 0; i < Enemylist.Length; i++)
        {
            Enemylist[i].GetComponent<EnemyFinder>().UnLook();
        }
    }

    public static void healskill()
    {
        PlayercurrentHP = PlayerHpBar.currentHp;
        flag = true;
        PlayercurrentHP += PlayermaxHPskill;
    }
    

}
