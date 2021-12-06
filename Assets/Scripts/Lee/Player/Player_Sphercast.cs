﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Sphercast : MonoBehaviour
{
    public float radius = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, radius, Vector3.up, 0f, LayerMask.GetMask("Enemy"));
        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                hits[i].collider.GetComponent<RenderManager>().show();
                //print(hits[i].collider.gameObject.name + " " + i);
                //hits[i].collider.GetComponent<EnemyTest>().LON();    
               // hits[i].collider.gameObject.GetComponent<EnemyTest>().LON();
            }
        }
        else
        {
            Debug.Log("하하");
        }


    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
        //Gizmos.DrawSphere(transform.position, 10);
    }
}
