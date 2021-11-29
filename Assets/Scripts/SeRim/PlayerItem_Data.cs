using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItem_Data : MonoBehaviour
{

     void Start() {
        
      playerHp = GameObject.Find("Canvas").GetComponent<PlayerHpBar>();

      playerState = GameObject.Find("Player").GetComponent<Player>();

       
    }

    void Update() {
               
       grenade();
       heallingPotion();
       stimulant();

    }

   
    Player playerState;
    PlayerHpBar playerHp;

    //플레이어가 배출할 오브젝트
    public GameObject grenadeObj;

    //배출할 지점
    public Transform grenadePos;

    //아이템 보유 변수
    public int hasGrenades;
    public int hasHealingPotion;
    public int hasStimulant;
    public int hasCoin;

    public int MaxhasGrenades;
    public int MaxHealingPotion;
    public int MaxStimulant;
    public int MaxCoin;

    //------아이템 계수 값-----

    //힐량
    public int heallingPotionValue = 50;

    //stimulant 값 (데미지, 스피드증폭, 공격속도증폭, 시야증폭, 지속시간)
    public int stimulantDamage = 10;
    public int stimulantMoveSpeed;

    public int stimulantAttackSpeed;

    public int stimulantSight;

    public float stimulantTime = 2f;

    //아이템 사용허가
    bool stimulantstate;

    public bool stimulanthasstate;
    public bool heallingPotionhasstate;

    public bool Grenadehasstate;

    public float Item_timer = 3f;

    //float Item_count_timer;
    
    public bool Item_Use_OK = true;

   

    void Item_off()
    {
        Item_Use_OK=true;
    }

    void grenade()
    {
        if(hasGrenades == 0)
        {
            Grenadehasstate=false;
            return;
        }
        if(Grenadehasstate && playerState.Attk && Item_Use_OK)
        {
            Item_Use_OK = false;  
            

            GameObject Cr_Grenade = Instantiate(grenadeObj, grenadePos.position, grenadePos.rotation);
            Rigidbody bulletRigid = Cr_Grenade.GetComponent<Rigidbody>();
            bulletRigid.velocity = (grenadePos.forward  + grenadePos.up ) * 50;

            
            hasGrenades--;
            Invoke("Item_off", Item_timer); 
            return;
        }

    }

    void heallingPotion()
    {
        if(hasHealingPotion == 0)
        {
            heallingPotionhasstate=false;
            return;
        }

        
        if(heallingPotionhasstate && playerState.Attk && Item_Use_OK)
        {
            
            Item_Use_OK = false;  
            //PlayerHpBar.currentHp += heallingPotionValue;       
            
            hasHealingPotion--;
            Invoke("Item_off", Item_timer); 
            return;

            
        }
        
    }

    void stimulant()
    {

        if(hasStimulant == 0)
        {
            stimulanthasstate=false;
            return;
        } 
        if(stimulantstate && playerState.Attk && Item_Use_OK)
        {
            Item_Use_OK = false; 

            playerState.speed += stimulantMoveSpeed;
            playerState.fireDelay -=stimulantAttackSpeed;
            //playerState.sight += stimulantSight;
           // PlayerHpBar.currentHp -= stimulantDamage;
            StartCoroutine(stimulantTimer());

            hasStimulant--;
            Invoke("Item_off", Item_timer); 
            return;
            
            
        }
        
    }

    IEnumerator stimulantTimer()
    {
        yield return new WaitForSeconds(stimulantTime);
        stimulantstate = false;
        playerState.speed -= stimulantMoveSpeed;
        playerState.fireDelay +=stimulantAttackSpeed;
        //playerState.sight -= stimulantSight;
      // PlayerHpBar.currentHp += stimulantDamage;
    }
   
    
    
    

}
