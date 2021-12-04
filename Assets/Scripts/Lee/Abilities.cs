using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Abilities : MonoBehaviour
{
    [Header("Ability 1")]
    public Image AbilityImage1;
    public float cooldown1 = 5;
    bool iscooldown = false;
    public KeyCode ability1;

    [Header("Ability 1")]
    public Image AbilityImage2;
    public float cooldown2 = 15;
    bool iscooldown2 = false;
    public KeyCode ability2;
    // Start is called before the first frame update
    void Start()
    {
        AbilityImage1.fillAmount = 0;
        AbilityImage2.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Ability1();
        Ability2();
    }
    
    void Ability1()
    {
        if(Input.GetKey(ability1) && iscooldown ==false)
        {
            iscooldown = true;
            AbilityImage1.fillAmount = 1;
        }

        if(iscooldown)
        {
            AbilityImage1.fillAmount -= 1 / cooldown1 * Time.deltaTime;

            if(AbilityImage1.fillAmount <= 0)
            {
                AbilityImage1.fillAmount = 0;
                iscooldown = false;
            }
        }
    }

    void Ability2()
    {
        if (Input.GetKey(ability2) && iscooldown2 == false)
        {
            iscooldown2 = true;
            AbilityImage2.fillAmount = 1;
        }

        if (iscooldown2)
        {
            AbilityImage2.fillAmount -= 1 / cooldown2 * Time.deltaTime;

            if (AbilityImage2.fillAmount <= 0)
            {
                AbilityImage2.fillAmount = 0;
                iscooldown2 = false;
            }
        }
    }
}
