using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageFailScene : MonoBehaviour
{
    public GameObject Text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StageSelect_2_3_Button()
    {
        StartCoroutine(NoStage());
    }
    IEnumerator NoStage()
    {
        Text.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        Text.SetActive(false);
    }

}
