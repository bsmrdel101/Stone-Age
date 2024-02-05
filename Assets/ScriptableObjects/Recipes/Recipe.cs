using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "Recipe")]
public class Recipe : ScriptableObject
{
    public string Name;
    public InventoryItem[] Ingredients;
    public Item CraftedItem;
    public int Quantity = 1;
}
