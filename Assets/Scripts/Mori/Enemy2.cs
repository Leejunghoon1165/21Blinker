using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy2 : MonoBehaviour
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
    public float AttackDist = 7.5f;

    float time;
    float attacktime;

    public Transform bulletPos;
    public GameObject bullet;

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
        attacktime = 0;

        time = 0;

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
             RLAttack(true);
            //anim.SetBool("IsWalk", false);
            //anim.SetBool("IsLRAttack", true);
            

            transform.LookAt(PlayerTransform);
        }
        else
        {
            //nav.SetDestination(target.transform.position);
            anim.SetBool("IsWalk", true);
            anim.SetBool("IsLRAttack", false);
        }

        //Debug.Log(attacktime);

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
            Destroy(gameObject, 1);
        }
    }

    private void RLAttack(bool attack)
    {
        attacktime += Time.deltaTime;

        if(attack == true && attacktime <= 1.35)
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

}