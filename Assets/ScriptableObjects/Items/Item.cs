using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemType
{
    Resource,
    Tool,
    Weapon
}

[CreateAssetMenu(fileName = "Item", menuName = "Item")]
public class Item : ScriptableObject
{
    public string Name;
    public Sprite Sprite;
    public ItemType ItemType;
    public int Tier = 1;
    public int Damage;
    public float AttackDelay;
}
