using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public GameObject playerCanvas;
    public GameObject[] weapons;
    public bool[] hasWeapons;


    public float speed;
    public float HP;


    float hAxis;
    float vAxis;


    bool DashDown;
    bool Attk;
    bool ItemGet;
    bool isDash;
    bool sWeapon1;
    bool sWeapon2;
    bool isFireReady;

    
    

    Vector3 moveVec;

    Rigidbody rigid;
    Animator anim;
    MeshRenderer[] meshs;

    Enemy StrAtk;
    GameObject nearobject;
    Weapon earlyWeapon;
    int equiWeaponIndex = -1;
    float fireDelay;
    
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();  //애니메이션
        meshs = GetComponentsInChildren<MeshRenderer>();
       
    }

    void Update()
    {
        GetInput();
        Move();
        Turn();
        Dash();
        GetItem();
        Attack();
        Swap();
        Die();


         Physics.IgnoreLayerCollision(LayerMask.NameToLayer("player"), LayerMask.NameToLayer("monster"),true);
        

    }

    void GetInput()  //입력받기 
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        DashDown = Input.GetButtonDown("Dash");
        Attk = Input.GetButtonDown("Shoot");
        ItemGet = Input.GetButtonDown("Get");
        sWeapon1 = Input.GetButtonDown("Swap1");
        sWeapon2 = Input.GetButtonDown("Swap2");

    }

    void Move()  //플레이어 움직임
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;  //normalized : 방향 값이 1로 보정된 벡터

        transform.position += moveVec * speed * Time.deltaTime;

        anim.SetBool("isWalk", moveVec != Vector3.zero);
    }

    void Turn() //대각선 움직임
    {
        transform.LookAt(transform.position + moveVec);
    }

    void Dash()  //대시 기능 
    {
        if(DashDown && moveVec != Vector3.zero && isDash == false)   //대시버튼이 눌림, 제자리에 서있지 않음, isDash가 거짓일 경우
        {
            anim.SetTrigger("doDash");   //대시 애니메이션 동작
            isDash = true;             //isDash true로 변경
            Invoke("DashOff", 1f);    //1초 뒤 DashOff 함수 실행
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


        int weaponIndex = -1;
        if (sWeapon1) weaponIndex = 0;
        if (sWeapon2) weaponIndex = 1;

        if(sWeapon1 || sWeapon2)
        {
            if(earlyWeapon != null)
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
                Item item = nearobject.GetComponent<Item>();
                int weaponIndex = item.value;
                hasWeapons[weaponIndex] = true;

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
            earlyWeapon.Use();
            anim.SetTrigger("doShoot");
            fireDelay = 0;
        }
        
        
    }

    void Damage()
    {
           //Enemy 공격력 가져옴
       // HP = HP - enemyStr.Str;
        StartCoroutine(OnDamage());
    }
    IEnumerator OnDamage()
    {
        anim.SetTrigger("doDamage");
        yield return new WaitForSeconds(2f);

    }


    void Die()
    {
        if (HP == 0)
        {
            anim.SetTrigger("doDie");
            Destroy(gameObject,2);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            playerCanvas.GetComponent<PlayerHpBar>().Dmg();
            Damage();

        }
            
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "weapon")
            nearobject = other.gameObject;
       
    }
    

    private void OnTriggerExit(Collider other)
    {
        nearobject = null;
    }

}
