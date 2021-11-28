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
    public GameObject RecoverFX;
    public int MAXHP = 10;
    public int CurHP;
    bool HealDlay;
    public ParticleSystem heal;
    public ParticleSystem bomb1;
    public ParticleSystem bomb2;
    Renderer rend;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        rend = GetComponent<Renderer>();
    }

    // Start is called before the first frame update
    void Start()
    {

        CurHP = MAXHP;
        HealDlay = false;

        RecoverFX.SetActive(false);

        attacktime = 0;
        time = 0;

        nav = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        
        //몬스터 위치
        Enemytransform = this.gameObject.GetComponent<Transform>();
        //플레이어 위치
        PlayerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }


    // Update is called once per frame
    void Update()
    {
        //플레이어를 항상 바라보게 함
        transform.LookAt(PlayerTransform);
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
            heal.Stop();
        }
        //Debug.Log(CurHP);
    }

    void Attack()
    {
        switch (enemyType){
            case Type.A:
                AttackMotion_A();
                break;
            case Type.B:    
                AttackMotion_B();
                break;
            case Type.C:
                AttackMotion_C();
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
            anim.SetBool("IsAttack", true);
            this.nav.velocity = Vector3.zero;
            time = 0;
        }
        else
        {
            anim.SetBool("IsWalk", true);
            anim.SetBool("IsAttack", false);

            time += Time.deltaTime;
            if(time <= 2 && time >= 0.8)
                this.nav.velocity = Vector3.zero;
        }
    }
    void AttackMotion_B()
    {
        if(AttackDist >= Dist)
        {
            attacktime += Time.deltaTime;
            this.nav.velocity = Vector3.zero;
        }
        if(AttackDist >= Dist && attacktime <= 1.35)
        {
            anim.SetBool("IsWalk", false);
            anim.SetBool("IsLRAttack", true);
            if(time == 0 && attacktime >= 1.3){
                StartCoroutine("Shot");
                time = 1;
            }
        }
        if(attacktime >= 1.35)
        {
            anim.SetBool("IsLRAttack", false);
            anim.SetBool("IsWalk", false);
        }
        if(attacktime >= 2.72){
            attacktime = 0;
            time = 0;
        }
    }
    void AttackMotion_C()
    {
        if(AttackDist >= Dist)
        {
            anim.SetBool("IsWalk", false);
            this.nav.velocity = Vector3.zero;
        }
        else
        {
            anim.SetBool("IsWalk", true);
        }
    }
    void AttackMotion_D()
    {
        if(AttackDist >= Dist)
        {
            anim.SetBool("IsWalk", false);
            anim.SetTrigger("DoDie");
            bomb1.Play();
            bomb2.Play();
            this.nav.velocity = Vector3.zero;
            Destroy(gameObject, 2f);
        }
    }

    private void OnTriggerStay(Collider col)
    {
        //힐범위와 충돌하면(피가10미만일때) 2초마다 체력2를 회복한다.
        if (col.gameObject.tag == "EnemyHeal" && HealDlay == false && CurHP <=10)
        {
            CurHP += 2;
            heal.Play();
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
        heal.Stop();
    }

    //원거리적 공격함수
    IEnumerator Shot()
    {
        GameObject intantBullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
        Rigidbody bulletRigid = intantBullet.GetComponent<Rigidbody>();
        bulletRigid.velocity = transform.forward * 15;

        yield return null;
    }

}
