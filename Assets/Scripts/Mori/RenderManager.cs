using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject body;
    public static bool render;

    void Start()
    {
        hide();
        render = false;
    }
    /*
    void update()
    {
        if(render)
            show();
        else
            hide();
    }
    */

    public void show()
    {
        body.SetActive(true);
    }
    public void hide()
    {
        body.SetActive(false);
    }
}