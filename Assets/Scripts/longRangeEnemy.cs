using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class longRangeEnemy : MonoBehaviour
{
    Transform target;

    Animator anim;
    
    NavMeshAgent nav;

    public float Str;
    public int MAXHP = 10;
    public int CurHP;

    private Transform PlayerTransform;
    private Transform Enemytransform;
    float Dist;
    public float AttackDist = 1.35f;

    private void Awake()
    {

        anim = GetComponent<Animator>();

        CurHP = MAXHP;

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
            this.nav.velocity = Vector3.zero;
        }
        else
        {
            nav.SetDestination(target.transform.position);

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

}
