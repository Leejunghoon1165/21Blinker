using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_new : MonoBehaviour
{
    // Start is called before the first frame update

    public float maxHP = 10;
    public float curHP;
    Transform target;
    MeshRenderer[] meshs;
    Rigidbody rigid;
    CapsuleCollider col;
    NavMeshAgent nav;
    private Transform PlayerTransform;
    private Transform Enemytransform;
    float Dist;
    public bool isChase;
    public bool isAttck;
    Animator anim;
    bool DamageOn;
    float playerHP;

    public float STR = 10;


    private void Awake()
    {
        maxHP = curHP;
        rigid = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
        meshs = GetComponentsInChildren<MeshRenderer>();
        anim = GetComponentInChildren<Animator>();
    }

    void Start() {
        nav = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        Enemytransform = this.gameObject.GetComponent<Transform>();
        PlayerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        playerHP = PlayerHpBar.currentHp;
    }

    void Update()
    {
        if(nav.enabled){
            nav.SetDestination(target.position);
            nav.isStopped = !isChase;
        }
        
        Dist = Vector3.Distance(Enemytransform.position, PlayerTransform.position);
        if(Dist <= 1)
            this.nav.velocity = Vector3.zero;
        
        transform.LookAt(PlayerTransform);
        //Invoke("ChaseShart", 2);
        ChaseShart();
    }

    void ChaseShart()
    {
        isChase = true;
        anim.SetBool("isWalk", true);
    }

    void FixedUpdate() {
        FreezeVelocity();
        targeting();
    }

    void targeting()
    {
        float targetRaius = 1f;
        float targetRange = 1.1f;

        RaycastHit[] rayHits = Physics.SphereCastAll(transform.position, targetRaius, Vector3.forward, targetRange, LayerMask.GetMask("player"));

        if(rayHits.Length > 0 && isAttck == false) {
            StartCoroutine(Bite());
        }
    }

    IEnumerator Bite()
    {
        isChase = false;
        isAttck  = true;
        anim.SetBool("isAttack", true);
        yield return new WaitForSeconds(0.5f);
        playerHP -= 2;
        yield return new WaitForSeconds(3.51f);
        isChase = true;
        isAttck  = false;
        anim.SetBool("isAttack", false);
    }

    void FreezeVelocity()
    {
        if(isChase) {
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;
        }
    }



    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Bulle_001")
        {
            curHP -= STR;
        }
        StartCoroutine(OnDamage());
    }

    IEnumerator OnDamage() 
    {
        foreach(MeshRenderer meshs in meshs){
            meshs.material.color = Color.red;
        }

        yield return new WaitForSeconds(0.1f);

        if(curHP > 0){
            foreach(MeshRenderer meshs in meshs) {
            meshs.material.color = Color.white;
            }
        }
        else {
            foreach(MeshRenderer meshs in meshs) {
            meshs.material.color = Color.grey;
            }
            anim.SetTrigger("doDie");
            isChase = false;
            nav.enabled = false;
        }
    }

}
