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
    public GrenadeData grenadeData;
    public int MAXHP = 10;
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

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        rend = GetComponent<Renderer>();
        meshs = GetComponentsInChildren<MeshRenderer>();

    }

    // Start is called before the first frame update
    void Start()
    {
        playerHP = PlayerHpBar.currentHp;
        RLAttack = false;
        CurHP = MAXHP;
        HealDlay = false;
        attacktime = 0;
        time = 0;

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
        anim.SetBool("IsWalk", true);

        Dist = Vector3.Distance(Enemytransform.position, PlayerTransform.position);
        nav.SetDestination(target.position);

        Attack();

        if(CurHP <= 0){
            this.nav.velocity = Vector3.zero;
            anim.SetTrigger("DoDie");
            Destroy(gameObject, 1);
        }
        if(CurHP >= 10)
        {
            CurHP = 10;
            RecoverFX.Stop();
        }

        //Debug.Log(CurHP);
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

    void AttackMotion_A()
    {
        if(AttackDist >= Dist)
        {
            anim.SetBool("IsWalk", false);
            this.nav.velocity = Vector3.zero;
            time = 0;
        }
        else
        {
            anim.SetBool("IsWalk", true);
            anim.SetBool("IsAttack", false);
            time += Time.deltaTime;
            if(time <= 1.5 && time >= 0.8)
                this.nav.velocity = Vector3.zero;
        }
        transform.LookAt(PlayerTransform);
    }
    void AttackMotion_B()
    {
        if(AttackDist >= Dist && RLAttack == false)
        {
            StartCoroutine(Shot());
        }
        transform.LookAt(PlayerTransform);
        if(AttackDist <= Dist)
            anim.SetBool("IsWalk", true);
        else
            anim.SetBool("IsWalk", false);

    }
    void AttackMotion_C()
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
    void AttackMotion_D()
    {
        if(AttackDist >= Dist)
        {
            anim.SetBool("IsWalk", false);
            anim.SetTrigger("DoDie");
            bomb1FX.Play();
            bomb2FX.Play();
            this.nav.velocity = Vector3.zero;
            Destroy(gameObject, 0.75f);
            playerHP -= Str;
        }
        else
            transform.LookAt(PlayerTransform);

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
        yield return new WaitForSeconds(2.01f);
    }

    IEnumerator Shot()//원거리 공격함수
    {
        RLAttack = true;
        anim.SetBool("IsLRAttack", true);
        yield return new WaitForSeconds(0.65f);

        GameObject intantBullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
        Rigidbody bulletRigid = intantBullet.GetComponent<Rigidbody>();
        bulletRigid.velocity = transform.forward * 15;

        yield return new WaitForSeconds(0.87f);
        anim.SetBool("IsLRAttack", false);
        RLAttack = false;
    }
    IEnumerator OnDamage()//데미지를 입을 때 마다 색깔이 바뀜
    {
        foreach(MeshRenderer meshs in meshs){
            meshs.material.color = Color.red;
        }

        yield return new WaitForSeconds(0.1f);

        if(CurHP > 0){
            foreach(MeshRenderer meshs in meshs) {
            meshs.material.color = Color.white;
            }
        }
        else {
            foreach(MeshRenderer meshs in meshs) {
            meshs.material.color = Color.grey;
            }
            anim.SetTrigger("doDie");
            nav.enabled = false;
        }
    }
    //총알에 맞았을 떄
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet_001") {
            CurHP -= 4;
            StartCoroutine(OnDamage());
        }
    }
    
    public void HitByGrenade()
    {
        Debug.Log("monster a!!");
        //체력 - 수류탄 데미지;
        CurHP -= grenadeData.Damage;
        //모리의 피격 로직StartCoroutine();
    }
}
