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

    public void Use()
    {
        if(type == Type.Range)
        {
            StartCoroutine("Shot");
        }
    }




    IEnumerator Shot()
    {
        Flash.Play();
        GameObject intantBullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
        Rigidbody bulletRigid = intantBullet.GetComponent<Rigidbody>();
        bulletRigid.velocity = bulletPos.forward * 50;

        yield return null;
    }
}
