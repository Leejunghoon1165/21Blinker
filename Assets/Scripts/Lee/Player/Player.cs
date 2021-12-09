using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
   
    public GameObject playerCanvas;
    public GameObject[] weapons;
    public ParticleSystem DashParticle;
  
    public bool[] hasWeapons;
    

    public float speed;


    float hAxis;
    float vAxis;
    int weaponIndex = -1;

    bool DashDown;
    bool Attk;
    public bool ItemGet;
    bool isDash;
    bool sWeapon1;
    bool sWeapon2;
    bool sWeapon3;
    bool isFireReady;
    bool doDie;
    public bool Item_Use;
    bool Player_skill1;
    bool Player_skill2;
    bool skillready;

    Vector3 moveVec;

    Rigidbody rigid;
    Animator anim;
    //MeshRenderer[] meshs;
    Enemy StrAtk;
    GameObject nearobject;
    Weapon earlyWeapon;
    int equiWeaponIndex = -1;
    public float fireDelay=0.01f;
    float PlayerHP;
    float CurrentHP;



    private void Start()
    {
        sWeapon3 = true;
        
        //earlyWeapon = weapons[2].GetComponent<Weapon>();
        // player = ReInput.players.GetPlayer(PlayerID);
        GameManager.Instance.Time_start = true;
        GameManager.Instance.Time_count = true;
        PlayerHP = GameManager.Instance.player_hp; 
        CurrentHP = PlayerHP;
       //Debug.Log(CurrentHP);
        //PlayerHP = PlayerHpBar.maxHp;
        //CurrentHP = PlayerHpBar.currentHp;
        //Debug.Log(PlayerHP);
        
    }

    void Awake()
    {   
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();  //애니메이션
        //meshs = GetComponentsInChildren<MeshRenderer>();
       
    }

    void Update()
    {
        if (sWeapon3 == true) Check();
        GetInput();
        Move();
        Turn();
        Dash();
        GetItem();
        Attack();
        Swap();
        Die();
        Player_Skill();
        TimeOver();
    }
    void Check()
    {
        //if (sWeapon3) weaponIndex = 2;
        //Debug.Log(weaponIndex);
        hasWeapons[2] = true;
        Swap();
        
    }

   
    void GetInput()  //입력받기 
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        DashDown = Input.GetButtonDown("Dash");
        Attk = Input.GetButton("Shoot");
        ItemGet = Input.GetButtonDown("Get");
        sWeapon1 = Input.GetButtonDown("Swap1");
        sWeapon2 = Input.GetButtonDown("Swap2");
        sWeapon3 = Input.GetButtonDown("Swap3");
        Item_Use = Input.GetButtonDown("ItemUse");

        Player_skill1 = Input.GetButtonDown("Skill1");
        Player_skill2 = Input.GetButtonDown("Skill2");

    }

    void Move()  //플레이어 움직임
    {
        
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;  //normalized : 방향 값이 1로 보정된 벡터
            if (doDie)
                moveVec = Vector3.zero;
        transform.position += moveVec * speed * Time.deltaTime;
        anim.SetBool("isWalk", moveVec != Vector3.zero);
        
        
    }

    void Turn() //대각선 움직임
    {
        transform.LookAt(transform.position + moveVec);
    }

    void Dash()  //대시 기능 
    {
        if(DashDown &&!isDash) /*&& moveVec != Vector3.zero && isDash == false && !doDie*/  //대시버튼이 눌림, 제자리에 서있지 않음, isDash가 거짓일 경우
        {
            DashParticle.Play();
            anim.SetTrigger("doDash");   //대시 애니메이션 동작
            isDash = true;             //isDash true로 변경
            Invoke("DashOff", 5f);    //1초 뒤 DashOff 함수 실행
        }
    }

    void DashOff()    //isDash 거짓으로 변경
    {
        isDash = false;
    }

    void Swap()
    {
        if (sWeapon1 && (!hasWeapons[0] || equiWeaponIndex == 0))
            return;
        if (sWeapon2 && (!hasWeapons[1] || equiWeaponIndex == 1))
            return;
        if (sWeapon3 && (!hasWeapons[2] || equiWeaponIndex == 2))
            return;

        if (sWeapon1) weaponIndex = 0;
        if (sWeapon2) weaponIndex = 1;
        if (sWeapon3) weaponIndex = 2;
        //if (sWeapon3) weaponIndex = 2;
      
        if (sWeapon1 || sWeapon2 || sWeapon3)
        {
            if (earlyWeapon != null)
                earlyWeapon.gameObject.SetActive(false);

            equiWeaponIndex = weaponIndex;
            earlyWeapon = weapons[weaponIndex].GetComponent<Weapon>();
            earlyWeapon.gameObject.SetActive(true);
        }
    }

    void GetItem()
    {
        if(ItemGet && nearobject != null)
        {
            if(nearobject.tag =="weapon")
            {
                FieldItemData item = nearobject.GetComponent<FieldItemData>();
                int weaponIndex = item.value;
                hasWeapons[weaponIndex] = true;

                Destroy(nearobject);
            }

            
            if(nearobject.tag =="Item")
            {
                
                FieldItemData item = nearobject.GetComponent<FieldItemData>();
                PlayerItem_Data item_Data = GameObject.Find("Player").GetComponent<PlayerItem_Data>();
                int value = (int) item.type;
                if(value == 0) // 그레네이드
                {
                    
                    item_Data.hasGrenades++;
                    item_Data.Grenadehasstate = true;
                    
                    item_Data.hasHealingPotion=0;
                    item_Data.heallingPotionhasstate=false;

                    item_Data.hasStimulant=0;
                    item_Data.stimulanthasstate = false;
                    if(item_Data.hasGrenades >= item_Data.MaxhasGrenades)
                    {
                        item_Data.hasGrenades = item_Data.MaxhasGrenades;
                    }
                }
                else if(value ==1) // 힐링포션
                {
                    
                    item_Data.hasHealingPotion++;
                    item_Data.heallingPotionhasstate = true;
                    
                    item_Data.hasGrenades=0;
                    item_Data.Grenadehasstate=false;

                    item_Data.hasStimulant=0;
                    item_Data.stimulanthasstate = false;
                    if(item_Data.hasHealingPotion >= item_Data.MaxHealingPotion)
                    {
                        item_Data.hasHealingPotion = item_Data.MaxHealingPotion;
                    }


                }
                else if(value ==2) // 각성제
                {
                    
                     item_Data.hasStimulant++;
                    item_Data.stimulanthasstate = true;
                   
                    item_Data.hasHealingPotion=0;
                    item_Data.heallingPotionhasstate=false;

                    item_Data.hasGrenades=0;
                    item_Data.Grenadehasstate = false;
                    if(item_Data.hasStimulant >= item_Data.MaxStimulant)
                    {
                        item_Data.hasStimulant = item_Data.MaxStimulant;
                    }
                }
                


                Destroy(nearobject);
            }
        }
        else if(nearobject != null)
        {
            FieldItemData item = nearobject.GetComponent<FieldItemData>();
            PlayerItem_Data item_Data = GameObject.Find("Player").GetComponent<PlayerItem_Data>();
            int value = (int) item.type;
            if(value == 5)
            {
                //GameManager.instamce.hascon += item.value;
                item_Data.hasCoin += item.value;
                Destroy(nearobject);
            }
        }
    }

    


    void Attack()
    {
        if (earlyWeapon == null)
            return;
        fireDelay += Time.deltaTime;
        isFireReady = earlyWeapon.rate < fireDelay;

        if(Attk && isFireReady && !isDash)
        {
            if(equiWeaponIndex == 0)
            {
                earlyWeapon.Use();
                //TestSound.Gunsounds();
                anim.SetTrigger("doShoot");
                fireDelay = 0;
            }
            else if(equiWeaponIndex == 1 && moveVec == Vector3.zero)
            {
                earlyWeapon.Use();
                anim.SetTrigger("doShootMinigun");
                fireDelay = 0;
            }
            else if(equiWeaponIndex == 2)
            {
                earlyWeapon.Use();
                 anim.SetTrigger("doShoot");
                fireDelay = 0;
            }
            
        }
        
        
    }

    public void Damage()
    {
        //Enemy 공격력 가져옴
        // HP = HP - enemyStr.Str;
            CurrentHP = PlayerHpBar.currentHp;
            StartCoroutine(OnDamage());
      
        
    }
    IEnumerator OnDamage()
    {
        anim.SetTrigger("doDamage");
        yield return new WaitForSeconds(2f);

    }


    void Die()
    {
        if(GameManager.Instance.Die == true)
        {
            doDie = true;
            anim.SetTrigger("doDie");
            Time.timeScale = 0f;
            GameManager.Instance.EndingCanvas.SetActive(true);
            GameManager.Instance.Ending_DIE.SetActive(true);
        }
        //if (CurrentHP <= 0)
        //{
        //    doDie = true;
        //    anim.SetTrigger("doDie");
        //    Time.timeScale = 0f;
        //    GameManager.Instance.EndingCanvas.SetActive(true);
        //    GameManager.Instance.Ending_DIE.SetActive(true);
        //    Destroy(gameObject, 2f);
            
            
        //}
        //IEnumerator DODIE()
        //{
        //    anim.SetTrigger("doDie");
        //    doDie = true;
        //    yield return new WaitForSeconds(2f);
        //    gameObject.SetActive(false);
        //}

    }
    
    void Player_Skill()
    {
        if (Player_skill1)
        {
            TestSkill player_skill1 = GetComponent<TestSkill>();
            if (player_skill1 && TestSkill.skill1flag == false)
                player_skill1.lightskill();
            else
                Debug.Log("불가능");

        }
        if (Player_skill2 && !skillready)
        {
            skillready = true;
            TestSkill.healskill();
            playerCanvas.GetComponent<PlayerHpBar>().heal();
            Invoke("skilloff()", 15f);
            //TestSkill player_skill2 = GetComponent<TestSkill>();
            
        }
    }

    void skilloff()
    {
        skillready = false;
    }

    void TimeOver()
    {
        if(GameManager.Instance.End == true)
        {
            Time.timeScale = 0f;
            GameManager.Instance.EndingCanvas.SetActive(true);
            GameManager.Instance.Ending_DIE.SetActive(true);
        }
    }


    private void OnCollisionEnter(Collision other)
    {
        //if (other.gameObject.tag == "EnemyAttack")
        //{
        //    PlayerHpBar.Dmg();
        //   // playerCanvas.GetComponent<PlayerHpBar>().Dmg();
        //    Damage();
        //}
        if(other.gameObject.tag == "EnemyBullet")
        {
            PlayerHpBar.Dmg2();
            //playerCanvas.GetComponent<PlayerHpBar>().Dmg2();
        }        
    }
    /*
     private void OnTriggerEnter(Collider other)
     {
         if (other.tag == "Enemy")
         {
             playerCanvas.GetComponent<PlayerHpBar>().Dmg();
             Damage();

         }

     }*/

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag =="EndZone")
        {
            Time.timeScale = 0f;
            GameManager.Instance.EndingCanvas.SetActive(true);
            GameManager.Instance.Ending_Win.SetActive(true);
        }
        if (other.gameObject.tag == "EnemyBullet")
        {
            PlayerHpBar.Dmg2();
            //playerCanvas.GetComponent<PlayerHpBar>().Dmg2();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "weapon")
            nearobject = other.gameObject;
        if(other.tag == "Item")
            nearobject = other.gameObject;
    }

    
    private void OnTriggerExit(Collider other)
    {
        nearobject = null;
    }

}
