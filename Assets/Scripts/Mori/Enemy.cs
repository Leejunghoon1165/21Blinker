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

    private Transform PlayerTransform;
    private Transform Enemytransform;

    float Dist;
    public float AttackDist = 1.35f;

    float time;

    public Transform bulletPos;
    public GameObject bullet;

    private void Awake()
    {

        anim = GetComponent<Animator>();
        
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        time = 3;
        
        CurHP = MAXHP;

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
            //Debug.Log("공격중중중중중중중");
            this.nav.velocity = Vector3.zero;

            time = 0;
        }
        else
        {
            //nav.SetDestination(target.transform.position);
            anim.SetBool("IsWalk", true);
            anim.SetBool("IsAttack", false);
            //Debug.Log("찾는중");

            time += Time.deltaTime;
            if(time <= 2 && time >= 0.8)
                this.nav.velocity = Vector3.zero;
        }

        if(CurHP <= 0)
        this.nav.velocity = Vector3.zero;
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet_001")
            CurHP -= 4;

        if(CurHP <= 0)
        {   
            //gameObject.GetComponent<NavMeshAgent>().enabled = false;
            anim.SetTrigger("DoDie");
            Destroy(gameObject, 2);
        }
    }

    IEnumerator Shot()
    {
        GameObject intantBullet = Instantiate(bullet, transform.position, transform.rotation);
        Rigidbody bulletRigid = intantBullet.GetComponent<Rigidbody>();
        bulletRigid.velocity = transform.forward * 50;

        yield return null;
    }

}
