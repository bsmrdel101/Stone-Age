using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [Header("Actions")]
    public static Action<Item, int> AddItemAction;
    public static Action<string, int> RemoveItemAction;

    [Header("Inventory")]
    public List<InventoryItem> Inventory = new List<InventoryItem>();

    [Header("References")]
    [SerializeField] private TMP_Text _woodAmountText;
    [SerializeField] private TMP_Text _rockAmountText;

    
    private void OnEnable()
    {
        AddItemAction += AddItem;
        RemoveItemAction += RemoveItem;
    }

    private void OnDisable()
    {
        AddItemAction -= AddItem;
        RemoveItemAction -= RemoveItem;
    }


    private void AddItem(Item item, int quantity)
    {
        if (ItemExists(item.Name))
        {
            IncreaseItemQuantity(item.name, quantity);
        } else
        {
            InventoryItem newItem = new InventoryItem(item, quantity);
            Inventory.Add(newItem);
        }
        UpdateResourceCounterUI();
    }

    private void RemoveItem(string itemName, int quantity)
    {
        
    }

    private bool ItemExists(string itemName)
    {
        return Inventory.Any((inventoryItem) => inventoryItem.Item.Name == itemName);
    }

    private void IncreaseItemQuantity(string itemName, int amount)
    {
        for (int i = 0; i < Inventory.Count; i++)
        {
            if (Inventory[i].Item.Name == itemName)
            {
                var itemCopy = Inventory[i];
                itemCopy.Quantity += amount;
                Inventory[i] = itemCopy;
                break;
            }
        }
    }

    private void UpdateResourceCounterUI()
    {
        foreach (InventoryItem inventoryItem in Inventory)
        {
            if (inventoryItem.Item.Name == "Log") _woodAmountText.text = inventoryItem.Quantity.ToString();
            if (inventoryItem.Item.Name == "Rock") _rockAmountText.text = inventoryItem.Quantity.ToString();
        }
    }
}


[Serializable]
public struct InventoryItem
{
    public Item Item;
    public int Quantity;

    public InventoryItem(Item item, int quantity)
    {
        Item = item;
        Quantity = quantity;
    }
}
