using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GrenadeData : MonoBehaviour
{
     public GameObject Meshobj; 
    public GameObject Effectobj;    //  이펙트 오브젝트
    public Rigidbody Gr_rigid;     
    public int Damage;
    public int Range;
    
    public float Exp_Time = 3.0f; // 터지는 타이머
   
    void Start() {
        StartCoroutine(Explosion());
    }

    void Update() {
               
       

    }

    IEnumerator Explosion()
    {
        yield return new WaitForSeconds(Exp_Time);
        Gr_rigid.velocity = Vector3.zero;
        Gr_rigid.angularVelocity = Vector3.zero;
        Effectobj.SetActive(true);
        //Meshobj.SetActive(false);  렌더링 꺼버리기

        Destroy(gameObject);

        RaycastHit[] rayHits = Physics.SphereCastAll(transform.position, 15, Vector3.up, 0f, LayerMask.GetMask("Enemy"));

        foreach(RaycastHit hitobj in rayHits)
        {
            //hitobj.transform.GetComponent<Enemy>().HitByGrenade(transform.position);
            //hitobj.transform.GetComponent<Enemy2>().HitByGrenade(transform.position);
        }

    }

    public void HitByGrenade (Vector3 explosionPos)
    {
        //체력 - 수류탄 데미지;
        //모리의 피격 로직StartCoroutine();

    }




}
