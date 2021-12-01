using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFinder : MonoBehaviour
{
    public MeshRenderer render2;
  //  public SkinnedMeshRenderer render;
    //public MeshRenderer render;
    // Start is called before the first frame update
    void Start()
    {
        render2 = GetComponent<MeshRenderer>();
        if(render2==null)
        {
            Debug.Log("빔");
        }
        //render = GetComponent<SkinnedMeshRenderer>();
        render2.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Look()
    {
        render2.enabled = true;
    }

    public void UnLook()
    {
        render2.enabled = false;
    }
}
