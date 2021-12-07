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
        hide();
    }

    public void show()
    {
        body.SetActive(true);
    }
    public void hide()
    {
        body.SetActive(false);
    }

}
