using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpBar : MonoBehaviour
{
    public Slider hpBar;
    public Slider BackHpSlider;

    public Transform player;
    public static float maxHp;
    public static float currentHp;
    public static bool backHpHit = false;
    public bool backHpHealing = false;
    public static float playerhealing;
    bool flag2;
    // Start is called before the first frame update
    void Start()
    {
        maxHp = GameManager.Instance.player_hp;
        currentHp = maxHp;
       // Debug.Log(currentHp);
       
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(playerhealing);
        transform.position = player.position + new Vector3(0, 3.5f,0); //체력바의 위치 고정
        hpBar.value = Mathf.Lerp(hpBar.value, currentHp / maxHp, Time.deltaTime * 5f);  //체력바의 벨류는 maxhp/currenthp 즉 100/100 = 1
        if(backHpHit)
        {
            BackHpSlider.value = Mathf.Lerp(BackHpSlider.value, currentHp / maxHp, Time.deltaTime * 10f);
            if( hpBar.value >= BackHpSlider.value - 0.01f)
            {
                backHpHit = false;
                BackHpSlider.value = hpBar.value;
            }
        }
        if(backHpHealing)
        {

        }

    }

    public static void Dmg()   //피해를 받을때 basic좀비 근거리 호출
    {
        Enemy enemyStr = GameObject.Find("Zombie_Basic(Clone)").GetComponent<Enemy>();
        currentHp -= enemyStr.Str;
        //invoke("BackHpFun",0.5);
        BackHpFun();
    }


    public static void Dmg2()  //롱렌지 원거리좀비
    {
        Enemy enemyStr = GameObject.Find("Zombie_LongRange(Clone)").GetComponent<Enemy>();
        currentHp -= enemyStr.Str;
        BackHpFun();
    }

    public static void Dmg3()   //bomb 폭발 좀비 
    {
        Enemy enemyStr = GameObject.Find("Zombie_Bomb(Clone)").GetComponent<Enemy>();
        currentHp -= enemyStr.Str;
        //invoke("BackHpFun",0.5);
        BackHpFun();
    }


    public void heal()
    {
        //Debug.Log("체력바에서 힐적용");
        // playerhealing = TestSkill.PlayercurrentHP;
        currentHp += TestSkill.PlayermaxHPskill;
        if(currentHp >= maxHp)
        {
            currentHp = maxHp;
        }
        Invoke("BackHpHeal", 0.5f);
    }


    public static void BackHpFun()
    {
        backHpHit = true;
    }

    void BackHpHeal()
    {
        backHpHealing = true;
    }
}
