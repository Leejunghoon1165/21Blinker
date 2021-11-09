using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    Transform target;

    Animator anim;
    
    NavMeshAgent nav;
    BoxCollider boxCollider;
    Rigidbody rigid;

    public float Str;
    public int MAXHP = 10;
    public int CurHP;

    public bool ischase;


    private Transform PlayerTransform;
    private Transform Enemytransform;

    public GameObject enemyprefabs;

    float Dist;
    float AttackDist = 2f;

    

    private void Awake()
    {

        anim = GetComponent<Animator>();

        CurHP = MAXHP;
        
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();

    }

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        
        //마우스몬스터 위치
        Enemytransform = this.gameObject.GetComponent<Transform>();
        //플레이어 위치
        PlayerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();

    }


    // Update is called once per frame
    void Update()
    {

        Dist = Vector3.Distance(Enemytransform.position, PlayerTransform.position);

        nav.SetDestination(target.position);
        
        if(AttackDist >= Dist)
        {
            //nav.SetDestination(Enemytransform.position);
            anim.SetBool("IsWalk", false);
            anim.SetBool("IsAttack", true);
            Debug.Log("공격중중중중중중중");
        }
        else
        {
            //nav.SetDestination(target.transform.position);
            anim.SetBool("IsWalk", true);
            anim.SetBool("IsAttack", false);
            Debug.Log("찾는중");
        }
        
    }

    private void HeatBullet(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet_001")
            CurHP -= 2;

        if(CurHP <= 0)
        {
            Destroy(gameObject, 1);
            anim.SetTrigger("DoDeath");
        }
    }

    public void HitByGrenade(Vector3 explosionPos)
    {
        CurHP -= 50;
        Vector3 reactvec = transform.position - explosionPos;
    }

}
