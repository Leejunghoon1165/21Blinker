using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFinder : MonoBehaviour
{
    public SkinnedMeshRenderer render;
    //public MeshRenderer render;
    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<SkinnedMeshRenderer>();
        render.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Look()
    {
        render.enabled = true;
    }
}
