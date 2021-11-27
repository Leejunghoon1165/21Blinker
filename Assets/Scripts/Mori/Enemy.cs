using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public enum Type { A, B, C}

    public Type enemyType;
    Transform target;

    Animator anim;
    
    NavMeshAgent nav;
    BoxCollider boxCollider;
    Rigidbody rigid;

    public float Str;
    public int MAXHP = 10;
    public int CurHP;

    private Transform PlayerTransform;
    private Transform Enemytransform;

    float Dist;
    public float AttackDist;

    float time;
    float attacktime;

    public Transform bulletPos;
    public GameObject bullet;

    public GrenadeData grenadeData;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        attacktime = 0;
        time = 0;
        
        CurHP = MAXHP;

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

        Dist = Vector3.Distance(Enemytransform.position, PlayerTransform.position);
        nav.SetDestination(target.position);

        Attack();

        if(CurHP <= 0){
            this.nav.velocity = Vector3.zero;
            anim.SetTrigger("DoDie");
            Destroy(gameObject, 1);
        }
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
        transform.LookAt(PlayerTransform);
        if(AttackDist >= Dist)
            attacktime += Time.deltaTime;
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
        }
        if(attacktime >= 2.72){
            attacktime = 0;
            time = 0;
        }
    }

    IEnumerator Shot()
    {
        GameObject intantBullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
        Rigidbody bulletRigid = intantBullet.GetComponent<Rigidbody>();
        bulletRigid.velocity = transform.forward * 15;

        yield return null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet_001")
            CurHP -= 4;
    }



    
    public void HitByGrenade()
    {
        Debug.Log("monster a!!");
        //체력 - 수류탄 데미지;
        CurHP -= grenadeData.Damage;


        //모리의 피격 로직StartCoroutine();

    }
    


}
