using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{

    public GameObject Object;
    public GameObject effectobj;
    public Rigidbody rigid;





    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Explosion());
    }

    // Update is called once per frame
    
    IEnumerator Explosion()
    {
        yield return new WaitForSeconds(3f);
        rigid.velocity = Vector3.zero;   // 속도 삭제
        rigid.angularVelocity = Vector3.zero; // 회전 삭제
        effectobj.SetActive(true); // 이펙트펑

        


        RaycastHit[] rayHits = Physics.SphereCastAll(transform.position, 15, Vector3.up, 0f, LayerMask.GetMask("monster"));

        foreach(RaycastHit hitobj in rayHits)
        {
            hitobj.transform.GetComponent<Enemy>().HitByGrenade(transform.position);
        }
        
        yield return new WaitForSeconds(0.5f);
        Destroy(this);
        Destroy(Object);
        
    }
}
