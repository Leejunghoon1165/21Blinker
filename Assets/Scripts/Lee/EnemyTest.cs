using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : MonoBehaviour
{
    public GameObject a;
    // Start is called before the first frame update
    void Start()
    {
        a.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LON()
    {
        a.SetActive(true);
    }

    public void LOFF()
    {
        a.SetActive(false);
    }
}
