using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    public enum Type {A, B, C, D}
    public Type enemyType;
    Transform target;
    Animator anim;
    NavMeshAgent nav;
    BoxCollider boxCollider;
    Rigidbody rigid;
    public float Str;
    private Transform PlayerTransform;
    private Transform Enemytransform;
    float Dist;
    public float AttackDist;
    float time;
    public Transform bulletPos;
    public GameObject bullet;
    GrenadeData grenadeData;
    public int MAXHP;
    public int CurHP;
    bool HealDlay;
    bool IsSpawn;
    public GameObject HealCheck;
    public ParticleSystem HealFX;
    public ParticleSystem RecoverFX;
    public ParticleSystem bomb1FX;
    public ParticleSystem bomb2FX;
    public ParticleSystem Blood;
    Renderer rend;
    MeshRenderer[] meshs;
    float playerHP;
    bool RLAttack;
    bool BombZomColorChange;
    bool Hited;
    bool LoosHP;
    bool basic_attack;
    bool bomb_attack;
    bool bombFX;
    bool bombcount;
    Vector3 lookrotation;
    public Transform spawnpoint1;
    public Transform spawnpoint2;
    public Transform spawnpoint3;
    public Transform spawnpoint4;
    public float RamdomNum;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        rend = GetComponent<Renderer>();
        meshs = GetComponentsInChildren<MeshRenderer>();
        grenadeData = GetComponent<GrenadeData>();
    }
    // Start is called before the first frame update
    void Start()
    {
        BombZomColorChange = false;
        playerHP = PlayerHpBar.currentHp;
        RLAttack = false;
        CurHP = MAXHP;
        HealDlay = false;
        time = 0;
        Hited = false;
        LoosHP = false;
        basic_attack = false;
        bomb_attack = false;
        bombFX = false;
        bombcount = false;
        IsSpawn = false;
        nav = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        
        //????????? ??????
        Enemytransform = this.gameObject.GetComponent<Transform>();
        //???????????? ??????
        PlayerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();

        RecoverFX.Pause();
        Blood.Pause();
        //?????? ????????? ???????????? ??????
        switch (enemyType){
            case Type.C:
                HealFX.Pause();
                HealCheck.gameObject.SetActive(false);
                break;
            case Type.D:
                bomb1FX.Pause();
                bomb2FX.Pause();
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        //??????????????? enemy??? ????????????
        Dist = Vector3.Distance(Enemytransform.position, PlayerTransform.position);
        //???????????? ??????
        nav.SetDestination(target.position);
        //???????????? ???????????? ????????? ??????????????? ???????????? ?????? ?????? ???????????? ??????
        lookrotation = nav.steeringTarget-transform.position;
        
        if(CurHP <= 0)
            StartCoroutine(DieAndRecoverHP());
        else
            Attack();
        
        //Debug.Log(CurHP);
        
        
        if(CurHP >= MAXHP)
        {
            CurHP = MAXHP;
            RecoverFX.Stop();
        }
        //Debug.Log(time);
    }
    void Attack()
    {
        switch (enemyType){
            case Type.A:
                AttackMotion_A();
                transform.LookAt(PlayerTransform);
                break;
            case Type.B:    
                AttackMotion_B();
                transform.LookAt(PlayerTransform);
                break;
            case Type.C:
                AttackMotion_C();
                transform.LookAt(PlayerTransform);
                break;
            case Type.D:
                AttackMotion_D();
                break;
        }
    }
    void AttackMotion_A() //Basic_Enemy
    {
        if(AttackDist >= Dist && !Hited) //??????
        {
            this.nav.velocity = Vector3.zero;
            time = 0;
            targerting();
        }
        else //??????
        {
            anim.SetBool("IsWalk", true);
            anim.SetBool("IsAttack", false);
            time += Time.deltaTime;
            if(time <= 1.25 && time >= 0.8)
                this.nav.velocity = Vector3.zero;
            transform.LookAt(PlayerTransform);
        }
    }
    void AttackMotion_B() //LR_Enemy
    {
        if(AttackDist >= Dist && !RLAttack && !Hited)
        {
            StartCoroutine(Shot());
            this.nav.velocity = Vector3.zero;
            transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(lookrotation), 1*Time.deltaTime);
        }
        if(AttackDist <= Dist) {
            anim.SetBool("IsWalk", true);
            transform.LookAt(PlayerTransform);
        }
        else
        {
            anim.SetBool("IsWalk", false);
            this.nav.velocity = Vector3.zero;
        }
    }
    void AttackMotion_C() //HealEnemy
    {
        if(AttackDist >= Dist)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(lookrotation), 1*Time.deltaTime);
            anim.SetBool("IsWalk", false);
            this.nav.velocity = Vector3.zero;
            HealCheck.gameObject.SetActive(true);
            HealFX.Play();
        }
        else
        {
            transform.LookAt(PlayerTransform);
            anim.SetBool("IsWalk", true);
            HealCheck.gameObject.SetActive(false);
            HealFX.Clear();
        }
    }
    void AttackMotion_D() //BombEnemy
    {
        if(Dist <= 5f && !bombFX)
        {
            bombcount = true;
        }
        if(Dist <= 1)
            this.nav.velocity = Vector3.zero;
        if(bombcount)
        {
            time += Time.deltaTime;
            if(!BombZomColorChange)
                StartCoroutine(ReadyToBomb());
        }
        if(time >= 4 && !bombFX)
            {
                StartCoroutine(BombFX());
                if(AttackDist >= Dist && !bomb_attack)
                    BombDamage();
            }
        if(time >= 4.1)
            {
                bombcount = false;
                bomb_attack = false;
                time = 0;
            }
        transform.LookAt(PlayerTransform);
        if(CurHP == MAXHP)
            anim.SetBool("IsWalk", true);
    }

    private void OnTriggerStay(Collider col)
    {
        //???????????? ????????????(?????? MAXHP????????????) 2????????? ??????2??? ????????????.
        if (col.gameObject.tag == "EnemyHeal" && !HealDlay && CurHP <= MAXHP) 
        {
            CurHP += 2;
            RecoverFX.Play();
            HealDlay = true;
            StartCoroutine(RecoverDelay());
        }
        //????????? ????????? ?????? ?????????.
        else if (col.gameObject.tag == "Bullet_001" && !LoosHP)
        {
            CurHP -= 30;
            StartCoroutine(loosHP());
            if(!Hited)
                StartCoroutine(hited());
        }
        else if (col.gameObject.tag == "SGBullet" && !LoosHP)
        {
            CurHP -= 24;
            StartCoroutine(loosHP());
            if(!Hited)
                StartCoroutine(hited());
        }
        
    }
    //2????????? ?????? ?????? ????????? ????????? ?????????.
    IEnumerator RecoverDelay()
    {
        yield return new WaitForSeconds(2f);
        HealDlay = false;
        RecoverFX.Stop();
    }

    IEnumerator loosHP(){
        LoosHP = true;
        CurHP -= 4;
        yield return new WaitForSeconds(.15f);
        LoosHP = false;
    }
    /*
    private void FixedUpdate() {
        targerting();
    }
    */
    //??????????????????
    void targerting()
    {
        float targetRaius = 1f;
        float targetRange = 1.5f;
        RaycastHit[] rayHits = Physics.SphereCastAll(transform.position, targetRaius, transform.forward, targetRange, LayerMask.GetMask("player")); 
        if(!basic_attack)
            StartCoroutine(Bite());
    }
    void BombDamage()
    {
        bomb_attack = true;
        PlayerHpBar.Dmg3();
        //Debug.Log("?????????");
    }

    IEnumerator BombFX()
    {
        bombFX = true;
        bomb1FX.Play();
        bomb2FX.Play();
        anim.SetTrigger("DoDie");
        this.nav.velocity = Vector3.zero;
        CurHP = 0;
        yield return new WaitForSeconds(.1f);
        bombFX = false;
        yield return new WaitForSeconds(1f);
        bomb1FX.Clear();
        bomb2FX.Clear();
    } 
    IEnumerator Shot()//????????? ????????????
    {
        RLAttack = true;
        anim.SetBool("IsLRAttack", true);
        yield return new WaitForSeconds(0.85f);
        if(!Hited) {
            GameObject intantBullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
            Rigidbody bulletRigid = intantBullet.GetComponent<Rigidbody>();
            bulletRigid.velocity = transform.forward * 15;
        }
        yield return new WaitForSeconds(0.67f);
        anim.SetBool("IsLRAttack", false);
        RLAttack = false;
    }

    IEnumerator Bite()
    {
        basic_attack = true;
        anim.SetBool("IsAttack", true);
        PlayerHpBar.Dmg();
        //Debug.Log("?????????");
        yield return new WaitForSeconds(2.01f);
        basic_attack = false;
    }
    /*
    //????????? ????????? ???
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet_001") {
            CurHP -= 4;
            if(!Hited)
                StartCoroutine(hited());
            if(!LoosHP)
                StartCoroutine(loosHP());
        }
        else if (collision.gameObject.tag == "SGBullet") {
            CurHP -= 8;
            StartCoroutine(loosHP());
            if(!Hited)
                StartCoroutine(hited());
             if(!LoosHP)
                StartCoroutine(loosHP());
        }
    }
    */
    /*
    IEnumerator OnDamage()//???????????? ?????? ??? ?????? ????????? ??????
    {
        Ondamage = true;
        //????????? ?????????
        foreach(MeshRenderer meshs in meshs){
            meshs.material.color = Color.red;
        }
        yield return new WaitForSeconds(0.3f);
        //?????? 0???????????? ?????? ??????????????? ?????????
        if(CurHP > 0){
            foreach(MeshRenderer meshs in meshs) {
            meshs.material.color = Color.white;
            }
        }
        Ondamage = false;
    }
    */
    //0.3????????? ???????????? ??????????????? ???????????? ??????
    IEnumerator hited() {
        this.nav.velocity = Vector3.zero;
        Blood.Play();
        Hited = true;
        anim.SetBool("IsHit", true);
        yield return new WaitForSeconds(0.2f);
        anim.SetBool("IsHit", false);
        yield return new WaitForSeconds(0.1f);
        Hited = false;
    }
    IEnumerator ReadyToBomb()//???????????? ??????????????? ??????????????? ??????
    {
        BombZomColorChange = true;
        foreach(MeshRenderer meshs in meshs){
            meshs.material.color = Color.red;
        }
        yield return new WaitForSeconds(0.3f);
        foreach(MeshRenderer meshs in meshs) {
            meshs.material.color = Color.white;
        }
        yield return new WaitForSeconds(0.3f);
        BombZomColorChange = false;
    }
    IEnumerator DieAndRecoverHP()
    {
        anim.SetTrigger("DoDie");
        this.nav.velocity = Vector3.zero;
        yield return new WaitForSeconds(.5f);
        if(!IsSpawn)
            StartCoroutine(SpawnRamdom());
        anim.SetTrigger("GoIdle");
        yield return new WaitForSeconds(.2f);
        anim.SetBool("IsWalk", true);
        CurHP = MAXHP;
    }

    IEnumerator SpawnRamdom()
    {
        IsSpawn = true;
        RamdomNum = Random.Range(0, 4);
        if(RamdomNum == 0)
        {
            Enemytransform.position = spawnpoint1.position ;
        }
        else if(RamdomNum == 1)
        {
            Enemytransform.position = spawnpoint2.position;
        }
        else if(RamdomNum == 2)
        {
            Enemytransform.position = spawnpoint3.position;
        }
        else if(RamdomNum == 3)
        {
            Enemytransform.position = spawnpoint4.position;
        }
        yield return new WaitForSeconds(2f);
        IsSpawn = false;
    }

    public void HitByGrenade()
    {
        //Debug.Log("monster a!!");
        //?????? - ????????? ?????????;
        CurHP -= 100;

        //Debug.Log("monster b!!");
        //????????? ?????? ??????StartCoroutine();
    }
}
