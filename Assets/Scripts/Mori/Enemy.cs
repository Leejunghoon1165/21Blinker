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
    float attacktime;
    public Transform bulletPos;
    public GameObject bullet;
    GrenadeData grenadeData;
    public int MAXHP;
    public int CurHP;
    bool HealDlay;
    public GameObject HealCheck;
    public ParticleSystem HealFX;
    public ParticleSystem RecoverFX;
    public ParticleSystem bomb1FX;
    public ParticleSystem bomb2FX;
    Renderer rend;
    MeshRenderer[] meshs;
    float playerHP;
    bool RLAttack;
    bool BombZomColorChange;
    bool Ondamage;
    bool DoDie;
    bool Hited;

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
        anim.SetBool("IsWalk", true);
        BombZomColorChange = false;
        playerHP = PlayerHpBar.currentHp;
        RLAttack = false;
        CurHP = MAXHP;
        HealDlay = false;
        attacktime = 0;
        time = 0;
        Ondamage = false;
        DoDie = false;
        Hited = false;

        nav = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        
        //몬스터 위치
        Enemytransform = this.gameObject.GetComponent<Transform>();
        //플레이어 위치
        PlayerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();

        RecoverFX.Pause();

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
        Dist = Vector3.Distance(Enemytransform.position, PlayerTransform.position);
        nav.SetDestination(target.position);

        Attack();
        
        if(CurHP <= 0 && !DoDie){
            Die();
        }
        
        if(CurHP >= MAXHP)
        {
            CurHP = MAXHP;
            RecoverFX.Stop();
        }
        //Debug.Log(CurHP);
    }

    void Die()
    {
        this.nav.velocity = Vector3.zero;
        anim.SetTrigger("DoDie");
        Destroy(gameObject, .66f);
        DoDie = true;
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
            anim.SetBool("IsAttack", true);
            this.nav.velocity = Vector3.zero;
            time = 0;
        }
        else //쫓기
        {
            anim.SetBool("IsAttack", false);
            time += Time.deltaTime;
            if(time <= 1.25 && time >= 0.8)
                this.nav.velocity = Vector3.zero;
        }
        transform.LookAt(PlayerTransform);
    }
    void AttackMotion_B() //LR_Enemy
    {
        if(AttackDist >= Dist && !RLAttack && !Hited)
        {
            StartCoroutine(Shot());
        }
        transform.LookAt(PlayerTransform);
        
        if(AttackDist <= Dist)
            anim.SetBool("IsWalk", true);
        else
            anim.SetBool("IsWalk", false);

    }
    void AttackMotion_C() //HealEnemy
    {
        if(AttackDist >= Dist)
        {
            anim.SetBool("IsWalk", false);
            this.nav.velocity = Vector3.zero;
            HealCheck.gameObject.SetActive(true);
            HealFX.Play();
        }
        else
        {
            anim.SetBool("IsWalk", true);
            HealCheck.gameObject.SetActive(false);
            HealFX.Pause();
        }
        transform.LookAt(PlayerTransform);
    }
    void AttackMotion_D() //BombEnemy
    {
        anim.SetBool("IsWalk", true);
        if(AttackDist >= Dist && !Hited)
        {
            anim.SetTrigger("DoDie");
            bomb1FX.Play();
            bomb2FX.Play();
            this.nav.velocity = Vector3.zero;
            Destroy(gameObject, 0.5f);
            playerHP -= Str;
        }
        else
            transform.LookAt(PlayerTransform);
        if(Dist <= 15 && !BombZomColorChange)
            StartCoroutine(ReadyToBomb());
    }

    private void OnTriggerStay(Collider col)
    {
        //힐범위와 충돌하면(피가10미만일때) 2초마다 체력2를 회복한다.
        if (col.gameObject.tag == "EnemyHeal" && HealDlay == false && CurHP <=10) 
        {
            CurHP += 2;
            RecoverFX.Play();
            HealDlay = true;
            StartCoroutine(RecoverDelay());
        }
        //총알에 맞으면 피가 닳는다.
        if (col.gameObject.tag == "Bullet_001")
            CurHP -= 4;
    }

    //2초동안 힐을 다시 못받게 딜레이 시킨다.
    IEnumerator RecoverDelay()
    {
        yield return new WaitForSeconds(2f);
        HealDlay = false;
        RecoverFX.Stop();
    }

    private void FixedUpdate() {
        targerting();
    }
    //근접공격함수
    void targerting()
    {
        float targetRaius = 1f;
        float targetRange = 1.5f;

        RaycastHit[] rayHits = Physics.SphereCastAll(transform.position, targetRaius, transform.forward, targetRange, LayerMask.GetMask("player"));
        StartCoroutine(Bite());
    }
    IEnumerator Bite()
    {
        anim.SetBool("IsAttack", true);
        playerHP -= Str;
        // public으로 고쳐주면 실행 GameObject.Find("Player").GetComponent<Player>().Damage();
        yield return new WaitForSeconds(2.01f);
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

    //총알에 맞았을 떄
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet_001") {
            CurHP -= 4;
            if(!Hited)
                StartCoroutine(hited());
            //if(!Ondamage)
            //    StartCoroutine(OnDamage());
        }
    }
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
        Hited = true;

        anim.SetBool("IsHit", true);
        this.nav.velocity = Vector3.zero;

        foreach(MeshRenderer meshs in meshs){
            meshs.material.color = Color.red;
        }

        yield return new WaitForSeconds(0.2f);

        anim.SetBool("IsHit", false);

        yield return new WaitForSeconds(0.1f);

        if(CurHP > 0){
            foreach(MeshRenderer meshs in meshs) {
            meshs.material.color = Color.white;
            }
        }
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
    
    public void HitByGrenade()
    {
        Debug.Log("monster a!!");
        //체력 - 수류탄 데미지;
        CurHP -= 100;

        Debug.Log("monster b!!");
        //모리의 피격 로직StartCoroutine();
    }
}
