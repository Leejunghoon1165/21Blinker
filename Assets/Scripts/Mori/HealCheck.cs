using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealCheck : MonoBehaviour
{
    float time;
    bool healcheck;

    public GameObject heal;

    void Start()
    {
        time = 0;   
        healcheck = false;
    }

    void Update()
    {
        if(healcheck == true)
        {
            time += Time.deltaTime;

            if(time >= 1)
            {
                heal.SetActive(true);
            }

            if(time >= 3f)
            {
                time = 0;
                heal.SetActive(false);
            }
        }
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
