using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ResourceType
{
    Tree,
    Stone
}

public class Resource : MonoBehaviour
{
    [Header("Resource")]
    public ResourceType Type;
    public Item Item;
    public int ResourcePool;
    public int Tier = 1;


    public void DepleteResourcePool(int amount)
    {
        ResourcePool -= amount;
        if (ResourcePool <= 0) Destroy(this.gameObject);
    }
}