using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldItemData : MonoBehaviour
{
    // 필드 아이템의 타입을 결정하고 획득 수량을 결정함
    public enum Type { Grenade,HealingPotion,Stimulant, ee, weapon, coin, ammo };
    public Type type;
    public int value;


}
