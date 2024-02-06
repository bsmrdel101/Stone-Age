using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Resource : MonoBehaviour
{
    [Header("Resource")]
    public Item Item;
    public int ResourcePool;
    public int Tier = 1;
    public int DmgThreshold = 0;
    public int CurrentDmgLevel = 0;

    [Header("Particles")]
    public ParticleSystem ResourceBreakParticles;

    [Header("References")]
    [SerializeField] private Texture2D cursor;


    public void DepleteResourcePool()
    {
        Destroy(gameObject);
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    public void OnMouseEnter()
    {
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
    }
    
    public void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
