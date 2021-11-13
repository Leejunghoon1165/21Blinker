using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingEnemy : MonoBehaviour
{
    Rigidbody rigid;
    Vector3 moveVec;
    float speed = 25;
    // Start is called before the first frame update
    void Start()
    {

        rigid = GetComponent<Rigidbody>();
        moveVec = new Vector3(1, 0, 0).normalized;
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += moveVec * speed * Time.deltaTime;
    }
}
