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
        
        //몬스터 위치
        Enemytransform = this.gameObject.GetComponent<Transform>();
        //플레이어 위치
        PlayerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();

        RecoverFX.Pause();
        Blood.Pause();
        //처음 시작시 이펙트를 끄기
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
        //플레이어와 enemy의 거리계산
        Dist = Vector3.Distance(Enemytransform.position, PlayerTransform.position);
        //플레이어 쫓기
        nav.SetDestination(target.position);
        //플레이어 바라보는 속도를 프레임마다 계산해서 보다 빨리 돌아보게 만듦
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
        if(AttackDist >= Dist && !Hited) //공격
        {
            this.nav.velocity = Vector3.zero;
            time = 0;
            targerting();
        }
        else //쫓기
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
        //힐범위와 충돌하면(피가 MAXHP미만일때) 2초마다 체력2를 회복한다.
        if (col.gameObject.tag == "EnemyHeal" && !HealDlay && CurHP <= MAXHP) 
        {
            CurHP += 2;
            RecoverFX.Play();
            HealDlay = true;
            StartCoroutine(RecoverDelay());
        }
        //총알에 맞으면 피가 닳는다.
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
    //2초동안 힐을 다시 못받게 딜레이 시킨다.
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
    //근접공격함수
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
        //Debug.Log("공격중");
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
    IEnumerator Shot()//원거리 공격함수
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
        //Debug.Log("공격중");
        yield return new WaitForSeconds(2.01f);
        basic_attack = false;
    }
    /*
    //총알에 맞았을 떄
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
    IEnumerator OnDamage()//데미지를 입을 때 마다 색깔이 바뀜
    {
        Ondamage = true;
        //맞으면 빨간색
        foreach(MeshRenderer meshs in meshs){
            meshs.material.color = Color.red;
        }
        yield return new WaitForSeconds(0.3f);
        //피가 0이상이면 다시 원래색으로 돌아옴
        if(CurHP > 0){
            foreach(MeshRenderer meshs in meshs) {
            meshs.material.color = Color.white;
            }
        }
        Ondamage = false;
    }
    */
    //0.3초동안 제자리에 서있으면서 피격애니 실행
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
    IEnumerator ReadyToBomb()//폭발좀비 터지기전에 깜빡거리에 하기
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
        //체력 - 수류탄 데미지;
        CurHP -= 100;

        //Debug.Log("monster b!!");
        //모리의 피격 로직StartCoroutine();
    }
}
