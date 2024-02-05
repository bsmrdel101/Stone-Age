using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [Header("Actions")]
    public static Action<Item, int> AddItemAction;
    public static Action<Item> EquipWeaponAction;
    public static Action<Item> EquipToolAction;
    public static Action<List<InventoryItem>> UpdateInventoryAction;

    [Header("Inventory")]
    public List<InventoryItem> Inventory = new List<InventoryItem>();
    public Item selectedWeapon;
    public Item selectedTool;

    [Header("References")]
    [SerializeField] private TMP_Text _woodAmountText;
    [SerializeField] private TMP_Text _rockAmountText;
    [SerializeField] private TMP_Text _ropeAmountText;
    [SerializeField] private TMP_Text _grassAmountText;
    [SerializeField] private Image _selectedWeaponSprite;
    [SerializeField] private Image _selectedToolSprite;

    
    private void OnEnable()
    {
        AddItemAction += AddItem;
        EquipWeaponAction += EquipWeapon;
        EquipToolAction += EquipTool;
        UpdateInventoryAction += UpdateInventory;
    }

    private void OnDisable()
    {
        AddItemAction -= AddItem;
        EquipWeaponAction -= EquipWeapon;
        EquipToolAction -= EquipTool;
        UpdateInventoryAction -= UpdateInventory;
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
        CraftingManager.ReloadCraftingRecipeAction();
    }

    private void UpdateInventory(List<InventoryItem> inventory)
    {
        Inventory = inventory;
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
            if (inventoryItem.Item.Name == "Stick") _woodAmountText.text = inventoryItem.Quantity.ToString();
            if (inventoryItem.Item.Name == "Rock") _rockAmountText.text = inventoryItem.Quantity.ToString();
            if (inventoryItem.Item.Name == "Rope") _ropeAmountText.text = inventoryItem.Quantity.ToString();
            if (inventoryItem.Item.Name == "Grass") _grassAmountText.text = inventoryItem.Quantity.ToString();
        }
    }

    private void EquipWeapon(Item item)
    {
        selectedWeapon = item;
        _selectedWeaponSprite.sprite = item.Sprite;
    }

    private void EquipTool(Item item)
    {
        selectedTool = item;
        _selectedToolSprite.sprite = item.Sprite;
    }

    public int GetItemQty(InventoryItem item)
    {
        return Inventory.Find((inventoryItem) => inventoryItem.Item.Name == item.Item.Name).Quantity;
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
