using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GrenadeData : MonoBehaviour
{
    
    public Renderer Meshobj; 
    public GameObject Effectobj;    //  이펙트 오브젝트
    public Rigidbody Gr_rigid;     
    public int Damage = 50;
    public float Range = 5.0f;
    
    public float Exp_Time = 2.0f; // 터지는 타이머
   
    void Start() 
    {
        StartCoroutine(Explosion());        
    }

    IEnumerator Explosion()
    {
        yield return new WaitForSeconds(Exp_Time);
        Gr_rigid.velocity = Vector3.zero;
        Gr_rigid.angularVelocity = Vector3.zero;
        Effectobj.SetActive(true);  // 이펙트 켜기

        //Meshobj.SetActive(false);  //렌더링 꺼버리기
        //Meshobj.enabled = false;   

        //int layerMask = 1 << LayerMask.NameToLayer("Enemy");
        
        //RaycastHit[] rayHits = Physics.SphereCastAll(transform.position, 30f, Vector3.up, 0f, 1 << LayerMask.NameToLayer("Enemy"));
        // Debug.DrawRay(transform.position, Vector3.up ,Color.red);
        // Debug.Log(rayHits);

        // foreach(RaycastHit hitobj in rayHits)
        // {
        //     Debug.Log(hitobj.collider.name);
        //     hitobj.transform.GetComponent<Enemy>().HitByGrenade();           
        // }

        int layerMask = 1 << 9;
        Collider[] rayHits = Physics.OverlapSphere(transform.position, Range, layerMask);
        Debug.Log(rayHits);
        foreach(Collider hitobj in rayHits)
        {    
             Debug.Log(hitobj.name);
             hitobj.GetComponent<Enemy>().HitByGrenade();           
        }
        

        Destroy(gameObject, 3);

    }

   
    // private void OnTriggerEnter(Collider other) {

    //     Debug.Log("수류탄과의 물체트리거충돌발생");
    //     Gr_rigid.velocity = Vector3.zero;
    //     Gr_rigid.angularVelocity = Vector3.zero;
        
    //     //Meshobj.SetActive(false);  //렌더링 꺼버리기
    //     Meshobj.enabled = false;
    //     Effectobj.SetActive(true);  // 이펙트 켜기
     
    //     int layerMask = 1 << 9;
    //     Collider[] rayHits = Physics.OverlapSphere(transform.position, Range, layerMask);
    //     Debug.Log(rayHits);
    //     foreach(Collider hitobj in rayHits)
    //     {    
    //             Debug.Log(hitobj.name);
    //             hitobj.GetComponent<Enemy>().HitByGrenade();           
    //     }
        

    //     Destroy(gameObject, 5);
        
    // }
    
    


    private void OnCollisionEnter(Collision collision)
    {
        
            Debug.Log("수류탄과의 물체콜라이전엔터충돌발생");
            Gr_rigid.velocity = Vector3.zero;
            Gr_rigid.angularVelocity = Vector3.zero;
            Effectobj.SetActive(true);  // 이펙트 켜기
            //Meshobj.SetActive(false);  //렌더링 꺼버리기
            //Meshobj.enabled = false;

            int layerMask = 1 << 9;
            Collider[] rayHits = Physics.OverlapSphere(transform.position, Range, layerMask);
            Debug.Log(rayHits);
            foreach(Collider hitobj in rayHits)
            {    
                Debug.Log(hitobj.name);
                hitobj.GetComponent<Enemy>().HitByGrenade();           
            }
        

        Destroy(gameObject, 3);

            
    }
    
   

    

   

    




}
