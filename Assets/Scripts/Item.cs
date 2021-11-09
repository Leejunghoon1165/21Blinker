using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum Type { grenade,weapon, coin, heart, ammo };
    public Type type;
    public int value;


}
