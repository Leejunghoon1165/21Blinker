using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject body;

    float time;
    void Start()
    {
        body.SetActive(false);

        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time >= 3)
            body.SetActive(true);
        Debug.Log(time);
    }
}
