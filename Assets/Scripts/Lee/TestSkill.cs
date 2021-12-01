using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSkill : MonoBehaviour
{
    public static float PlayermaxHPskill;
    public static float PlayercurrentHP;
    public static float PlayerhealHP;
    public static bool skill1flag;
    public static bool skill2flag;
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
        //StartCoroutine(Ontime(4f));
    }
    IEnumerator Ontime(float cool)
    {
        while(cool > 1.0f)
        {
            cool -= Time.deltaTime;
            Debug.Log(cool);
        }
        yield return null;
    
    }
    
    IEnumerator OnSkill()
    {
        skill1flag = true;
        GameObject[] Enemylist = GameObject.FindGameObjectsWithTag("Enemy");
        //Debug.Log("실행중");
        //랜덤탐색후 배열에 담음
        //GameObject B = GameObject.Find("Plane");
        //B.SetActive(false);
        if (Enemylist != null)
        {
            for (int i = 0; i < Enemylist.Length; i++)
            {
                Enemylist[i].GetComponent<EnemyFinder>().Look();
            }
        }
        yield return new WaitForSeconds(2f);
        //B.SetActive(true);
        if (Enemylist != null)
        {
            for (int i = 0; i < Enemylist.Length; i++)
            {
                Enemylist[i].GetComponent<EnemyFinder>().UnLook();
            }
        }
        skill1flag = false;
    }

    public static void healskill()
    {
        skill2flag = true;
        PlayercurrentHP = PlayerHpBar.currentHp;
        PlayercurrentHP += PlayermaxHPskill;
        skill2flag = false;
    }
    

}
