using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public Transform spawnpoint1;
    public Transform spawnpoint2;
    public Transform spawnpoint3;
    public Transform spawnpoint4;
    public GameObject Enemy_Basic;
    public GameObject Enemy_LR;
    public GameObject Enemy_Heal;
    public GameObject Enemy_Bomb;
    public float RamdomNum;
    public float RamdomEnem;
    bool IsSpawn;
    float SpawnCount;

    private void Start() {
        SpawnCount = 0;
        IsSpawn = false;
    }

    private void Update() {
        if(!IsSpawn && SpawnCount <= 30)
            StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        IsSpawn = true;
        RamdomNum = Random.Range(0, 7);
        if(RamdomNum == 0 || RamdomNum == 1 || RamdomNum == 2)
        {
            SpawnRamdom1();
        }
        else if(RamdomNum == 3 || RamdomNum == 4)
        {
            SpawnRamdom2();
        }
        else if(RamdomNum == 5)
        {
            SpawnRamdom3();
        }
        else if(RamdomNum == 6)
        {
            SpawnRamdom4();
        }
        yield return new WaitForSeconds(.2f);
        SpawnCount += 1;
        if(SpawnCount == 10 || SpawnCount == 20 || SpawnCount == 30)
            yield return new WaitForSeconds(30f);
        IsSpawn = false;
    }
   
    public void SpawnRamdom1()
    {
        RamdomEnem = Random.Range(0, 4);
        if(RamdomNum == 0)
        {
            Instantiate(Enemy_Basic, spawnpoint1.position, spawnpoint1.rotation);
        }
        if(RamdomEnem == 1)
        {
            Instantiate(Enemy_LR, spawnpoint1.position, spawnpoint1.rotation);
        }
        if(RamdomEnem == 2)
        {
            Instantiate(Enemy_Heal, spawnpoint1.position, spawnpoint1.rotation);
        }
        if(RamdomEnem == 3)
        {
            Instantiate(Enemy_Bomb, spawnpoint1.position, spawnpoint1.rotation);
        } 
    }

    public void SpawnRamdom2()
    {
        RamdomEnem = Random.Range(0, 4);
        if(RamdomNum == 0)
        {
            Instantiate(Enemy_Basic, spawnpoint2.position, spawnpoint2.rotation);
        }
        if(RamdomEnem == 1)
        {
            Instantiate(Enemy_LR, spawnpoint2.position, spawnpoint2.rotation);
        }
        if(RamdomEnem == 2)
        {
            Instantiate(Enemy_Heal, spawnpoint2.position, spawnpoint2.rotation);
        }
        if(RamdomEnem == 3)
        {
            Instantiate(Enemy_Bomb, spawnpoint2.position, spawnpoint2.rotation);
        } 
    }

    public void SpawnRamdom3()
    {
        RamdomEnem = Random.Range(0, 4);
        if(RamdomNum == 0)
        {
            Instantiate(Enemy_Basic, spawnpoint3.position, spawnpoint3.rotation);
        }
        if(RamdomEnem == 1)
        {
            Instantiate(Enemy_LR, spawnpoint3.position, spawnpoint3.rotation);
        }
        if(RamdomEnem == 2)
        {
            Instantiate(Enemy_Heal, spawnpoint3.position, spawnpoint3.rotation);
        }
        if(RamdomEnem == 3)
        {
            Instantiate(Enemy_Bomb, spawnpoint3.position, spawnpoint3.rotation);
        } 
    }

    public void SpawnRamdom4()
    {
        RamdomEnem = Random.Range(0, 4);
        if(RamdomNum == 0)
        {
            Instantiate(Enemy_Basic, spawnpoint4.position, spawnpoint4.rotation);
        }
        if(RamdomEnem == 1)
        {
            Instantiate(Enemy_LR, spawnpoint4.position, spawnpoint4.rotation);
        }
        if(RamdomEnem == 2)
        {
            Instantiate(Enemy_Heal, spawnpoint4.position, spawnpoint4.rotation);
        }
        if(RamdomEnem == 3)
        {
            Instantiate(Enemy_Bomb, spawnpoint4.position, spawnpoint4.rotation);
        } 
    }



}
