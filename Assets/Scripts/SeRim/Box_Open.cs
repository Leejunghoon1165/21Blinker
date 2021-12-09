using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box_Open : MonoBehaviour
{
    public GameObject Box_Head;

    bool open;

    float time;

    // Start is called before the first frame update
    void Start()
    {
        open = false;
        time =  0;
    }

    // Update is called once per frame
    void Update()
    {
        if(time <= 15)
            time += Time.deltaTime;
        if(!open && time >= 10)
            StartCoroutine(BoxOpen());
        

    }

    IEnumerator BoxOpen()
    {
        open = true;
        
        Box_Head.transform.localEulerAngles = new Vector3(135,0,0);
        yield return new WaitForSeconds(.1f);
        Box_Head.transform.localEulerAngles = new Vector3(130,0,0);
        yield return new WaitForSeconds(.1f);
        Box_Head.transform.localEulerAngles = new Vector3(120,0,0);
        yield return new WaitForSeconds(.1f);
        Box_Head.transform.localEulerAngles = new Vector3(110,0,0);
        yield return new WaitForSeconds(.1f);
        Box_Head.transform.localEulerAngles = new Vector3(100,0,0);
        yield return new WaitForSeconds(.1f);
        Box_Head.transform.localEulerAngles = new Vector3(90,0,0);
        yield return new WaitForSeconds(.1f);
        Box_Head.transform.localEulerAngles = new Vector3(80,0,0);
        yield return new WaitForSeconds(.1f);
        Box_Head.transform.localEulerAngles = new Vector3(70,0,0);
        yield return new WaitForSeconds(.1f);
        Box_Head.transform.localEulerAngles = new Vector3(60,0,0);
        yield return new WaitForSeconds(.1f);
        Box_Head.transform.localEulerAngles = new Vector3(50,0,0);
        yield return new WaitForSeconds(.1f);
        Box_Head.transform.localEulerAngles = new Vector3(40,0,0);
        yield return new WaitForSeconds(.1f);
        Box_Head.transform.localEulerAngles = new Vector3(30,0,0);
        yield return new WaitForSeconds(.1f);
        Box_Head.transform.localEulerAngles = new Vector3(20,0,0);
        yield return new WaitForSeconds(.1f);
        Box_Head.transform.localEulerAngles = new Vector3(10,0,0);
        yield return new WaitForSeconds(.1f);
        Box_Head.transform.localEulerAngles = new Vector3(0,0,0);
        yield return new WaitForSeconds(.1f);

    }
}
