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
    public bool backHpHit = false;
    // Start is called before the first frame update
    void Start()
    {
        
       
    }

    // Update is called once per frame
    void Update()
    {
        maxHp = Player.PlayerHP;   //최대 체력은 플레이어 스크립트에서 받아온다.
        currentHp = Player.CurrentHP;  //currenthp 또한 플레이어 스크립트에서 받아옴
        Debug.Log(maxHp);
        Debug.Log(currentHp);
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
    }

    public void Dmg()   //피해를 받을때
    {
        Enemy enemyStr = GameObject.Find("ZombieA").GetComponent<Enemy>();
        currentHp -= enemyStr.Str;
        Invoke("BackHpFun", 0.5f);
;    }

    public void Dmg2()
    {
        Enemy enemyStr = GameObject.Find("ZombieB").GetComponent<Enemy>();
        currentHp -= enemyStr.Str;
        Invoke("BackHpFun", 0.5f);
    }


    void BackHpFun()
    {
        backHpHit = true;
    }
}
