using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealCheck : MonoBehaviour
{
    float time;
    bool healcheck;


    // Start is called before the first frame update
    void Start()
    {
        time = 0;   
        healcheck = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(healcheck == true)
        {
            time += Time.deltaTime;
            if(time >= 2.5)
            {
                Debug.Log("회복 시키는 중");
                time = 0;
            }
        }
        Debug.Log(healcheck);
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.tag == "EnemyHeal")
        {
            healcheck = true;
        }
        else
            healcheck = false;
    }
}
