using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LRAttackConfirm : MonoBehaviour
{

    public bool ball;
    
    // Start is called before the first frame update
    void Start()
    {
        ball = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void AttackConfirm(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ball = true;
            Debug.Log("공격중중중ㅈ우");
        }
        else
        {
            ball = false;
             Debug.Log("공격안함");
        }
    }
}
