using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpBar : MonoBehaviour
{
    public Slider hpBar;
    public Slider BackHpSlider;

    public Transform player;
    public float maxHp = 100;
    public float currentHp = 100;
    public bool backHpHit = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position + new Vector3(0, 3.5f,0);
        hpBar.value = Mathf.Lerp( hpBar.value, currentHp / maxHp ,Time.deltaTime * 5f) ;
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

    public void Dmg()
    {
        Debug.Log("DD");
        Enemy enemyStr = GameObject.Find("ZombieA(Clone)").GetComponent<Enemy>();
        currentHp -= enemyStr.Str;
        Invoke("BackHpFun", 0.5f);
;    }

    public void Dmg2()
    {
        Debug.Log("원거리");
        Enemy2 enemyStr = GameObject.Find("ZombieB(Clone)").GetComponent<Enemy2>();
        currentHp -= enemyStr.Str;
        Invoke("BackHpFun", 0.5f);
        ;
    }


    void BackHpFun()
    {
        backHpHit = true;
    }
}
