using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public Transform spawnpoint1;
    public Transform spawnpoint2;
    public Transform spawnpoint3;
    public Transform spawnpoint4;
    public float  RamdomNum;

   
    public void SpawnRamdom()
    {
        RamdomNum = Random.Range(0, 4);
        if(RamdomNum == 0)
        {
            transform.position = spawnpoint1.position ;
        }
        else if(RamdomNum == 1)
        {
            transform.position = spawnpoint2.position;
        }
        else if(RamdomNum == 2)
        {
            transform.position = spawnpoint3.position;
        }
        else if(RamdomNum == 3)
        {
            transform.position = spawnpoint4.position;
        }
    }

}
