using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyVer2 : MonoBehaviour
{

    Transform target;
    Animator anim;
    NavMeshAgent nav;
    BoxCollider boxCollider;
    public BoxCollider AttackArea;
    Rigidbody rigid;

    public int MAXHP = 10;
    public int CurHP;

    public bool IsChase;
    public bool IsAttack;

    MeshRenderer[] meshs;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();

        meshs = GetComponentsInChildren<MeshRenderer>();
    }


    // Start is called before the first frame update
    void Start()
    {
        CurHP = 0;
        nav = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        nav.SetDestination(target.position);

        if(CurHP <= 0)
        {   
            this.nav.velocity = Vector3.zero;
            anim.SetTrigger("DoDie");
            Destroy(gameObject, 2);
        }

    }

    void Targerting()
    {
        float targetRadius = 1.5f;
        float targetRange = 3f;

        RaycastHit[] rayHits =
            Physics.SphereCastAll(transform.position, targetRadius, transform.forward, targetRange, LayerMask.GetMask("Player"));

        if(rayHits.Length > 0 && !IsAttack){
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        IsChase = false;
        IsAttack = true;
        anim.SetBool("IsAttck", true);
        yield return new WaitForSeconds(0.2f);
        AttackArea.enabled = true;
        yield return new WaitForSeconds(1f);
        AttackArea.enabled = false;

        IsChase = true;
        IsAttack = false;
        anim.SetBool("IsAttck", false);

    }

    void FixedUpdate()
    {
        Targerting();
        FreezeVelocity();
    }


    void FreezeVelocity()
    {
        if(IsChase)
        {
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet_001")
            CurHP -= 4;
        
        foreach(MeshRenderer mesh in meshs){
            mesh.material.color = Color.red;
        }

    }
}
