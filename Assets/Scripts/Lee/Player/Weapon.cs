using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum Type { Melee, Range, Shot };
    public Type type;
    public int damage;
    public float rate;
    public TrailRenderer trailEffect;
    public Transform bulletPos;
    public GameObject bullet;
    public ParticleSystem Flash;

    //샷건
    public int Count;   //샷건 탄 발수 
    public float Angle;  //각도
    List<Quaternion> pellets;  //쿼터니언 담을 배열

    public void Awake()
    {
    
        pellets = new List<Quaternion>(Count);   
        for (int i = 0; i < Count; i++)
        {
            pellets.Add(Quaternion.Euler(Vector3.zero));
            //quaternion.euler : z축 주위로 z,x축 주위로 x,y축 주위로 y각도 만큼 회전한 rotation을 반환하는것 
        }
    }

    public void Use()
    {
        if(type == Type.Range)
        {
            StartCoroutine("Shot");
        }
        else if(type == Type.Shot)
        {
            StartCoroutine("Shot2");
           // fire();
        }

    }
    //일반 다른 무기
    IEnumerator Shot()
    {
        Flash.Play();
        GameObject intantBullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
        Rigidbody bulletRigid = intantBullet.GetComponent<Rigidbody>();
        bulletRigid.velocity = bulletPos.forward * 50;

        yield return null;
    }
    //샷건 무기 코르틴
    IEnumerator Shot2()
    {

        //Rigidbody bulletRigid = intantBullet.GetComponent<Rigidbody>();
        //bulletRigid.velocity = bulletPos.forward * 50;

        Flash.Play();
        for (int i = 0; i < Count; i++)
        {
            pellets[i] = Random.rotation;   //랜덤한 방향값을 지정
            GameObject intantBullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation);   //총알 인스턴스화
            intantBullet.transform.rotation = Quaternion.RotateTowards(intantBullet.transform.rotation, pellets[i], Angle);
            intantBullet.GetComponent<Rigidbody>().AddForce(intantBullet.transform.forward * 100);   //발사 힘 
        }
        yield return null;
    }

}
