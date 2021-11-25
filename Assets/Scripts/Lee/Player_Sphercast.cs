using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Sphercast : MonoBehaviour
{
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
        RaycastHit[] rayHits = Physics.SphereCastAll(transform.position, 6, Vector3.up, 0f, LayerMask.GetMask("Enemy"));

        if (rayHits.Length > 0)
        {
            for (int i = 0; i < rayHits.Length; i++)
            {
                print(rayHits[i].collider.gameObject.name + "" + i);
            }
        }


    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 6);
        //Gizmos.DrawSphere(transform.position, 10);
    }
}
