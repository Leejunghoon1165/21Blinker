using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public Transform spawnpoint;
    public Transform spawnpoint1;
    public Transform spawnpoint2;
    public Transform spawnpoint3;

    public int MaxEnemy = 20;
    public int CurEnemy;
    public GameObject EnemyA;

    public GameObject EnemyB;

    float RamdomNum;

    float SpawnTime;


    private void Awake()
    {
        CurEnemy = 0;
        SpawnTime = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void SpawnRamdom()
    {
         RamdomNum = Random.Range(0, 4);
            if(RamdomNum == 0)
            {
                Instantiate(EnemyA, spawnpoint.position, spawnpoint.rotation);
                Instantiate(EnemyB, spawnpoint.position, spawnpoint.rotation);
                CurEnemy += 2;
            }

            if(RamdomNum == 1)
            {
                Instantiate(EnemyA, spawnpoint1.position, spawnpoint1.rotation);
                Instantiate(EnemyB, spawnpoint1.position, spawnpoint1.rotation);
                CurEnemy += 2;
            }

            if(RamdomNum == 2)
            {
                Instantiate(EnemyA, spawnpoint2.position, spawnpoint2.rotation);
                Instantiate(EnemyB, spawnpoint2.position, spawnpoint2.rotation);
                CurEnemy += 2;
            }

            if(RamdomNum == 3)
            {
                Instantiate(EnemyA, spawnpoint3.position, spawnpoint3.rotation);
                Instantiate(EnemyB, spawnpoint3.position, spawnpoint3.rotation);
                CurEnemy += 2;
            }
            
    }

    



    // Update is called once per frame
    void Update()
    {
        
        if(CurEnemy <= MaxEnemy)
        {
            SpawnTime += Time.deltaTime;
            Debug.Log(SpawnTime);

            if(SpawnTime >= 5 || SpawnTime <= 5.25)
            {
                SpawnRamdom();
            }
        }

    }


}
