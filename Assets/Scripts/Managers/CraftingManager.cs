using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftingManager : MonoBehaviour
{
    [Header("Actions")]
    public static Action ToggleCraftingUIAction;
    public static Action ReloadCraftingRecipeAction;

    [Header("Crafting UI")]
    [SerializeField] private Transform _ingredientListUI;
    [SerializeField] private Recipe[] _recipeList;
    [SerializeField] private Image _itemCraftingImage;
    [SerializeField] private TMP_Text _itemCraftingName;
    [SerializeField] private Color _greenColor;
    [SerializeField] private Color _redColor;
    private Recipe _loadedRecipe;

    [Header("References")]
    [SerializeField] private GameObject _craftingUI;
    [SerializeField] private GameObject _resourceDisplayPrefab;
    [SerializeField] private InventoryManager _inventoryManager;

    
    private void OnEnable()
    {
        ToggleCraftingUIAction += ToggleCraftingUI;
        ReloadCraftingRecipeAction += ReloadCraftingRecipe;
    }

    private void OnDisable()
    {
        ToggleCraftingUIAction -= ToggleCraftingUI;
        ReloadCraftingRecipeAction -= ReloadCraftingRecipe;
    }

    private void Start()
    {
        if (!_craftingUI.activeSelf) ToggleCraftingUI();
        LoadCraftingRecipe(_recipeList[0]);
        ToggleCraftingUI();
    }

    private void ReloadCraftingRecipe()
    {
        LoadCraftingRecipe(_loadedRecipe);
        InventoryManager.UpdateInventoryAction(_inventoryManager.Inventory);
    }

    private void LoadCraftingRecipe(Recipe recipe)
    {
        UnloadCurrentRecipe();
        _loadedRecipe = recipe;
        Item item = recipe.CraftedItem;
        _itemCraftingImage.sprite = item.Sprite;
        _itemCraftingName.text = item.Name;

        foreach (InventoryItem ingredient in recipe.Ingredients)
        {
            GameObject obj = Instantiate(_resourceDisplayPrefab, Vector3.zero, Quaternion.identity);
            TMP_Text objText = obj.GetComponentInChildren<TMP_Text>();
            obj.transform.SetParent(_ingredientListUI);
            obj.GetComponentInChildren<Image>().sprite = ingredient.Item.Sprite;
            UpdateIngredientDisplay(ingredient, objText);
        }
    }

    private void UpdateIngredientDisplay(InventoryItem ingredient, TMP_Text objText)
    {
        int currentAmount = _inventoryManager.GetItemQty(ingredient);
        objText.text = $"{currentAmount}/{ingredient.Quantity}";
        if (currentAmount >= ingredient.Quantity)
            objText.color = _greenColor;
        else
            objText.color = _redColor;
    }

    private void UnloadCurrentRecipe()
    {
        foreach (Transform transform in _ingredientListUI.transform)
        {
            Destroy(transform.gameObject);
        }
    }

    private bool CanCraftRecipe()
    {
        List<InventoryItem> inventory = _inventoryManager.Inventory;
        foreach (InventoryItem ingredient in _loadedRecipe.Ingredients)
        {
            bool isIngredientInInventory = inventory.Any((i) => i.Item.Name == ingredient.Item.Name && i.Quantity >= ingredient.Quantity);
            if (!isIngredientInInventory) return false;
        }
        return true;
    }

    private void DepleteRecipeResources()
    {
        List<InventoryItem> inventory = new List<InventoryItem>(_inventoryManager.Inventory);
        foreach (InventoryItem ingredient in _loadedRecipe.Ingredients)
        {
            int index = inventory.FindIndex(i => i.Item.Name == ingredient.Item.Name);
            if (index != -1)
            {
                InventoryItem updatedItem = new InventoryItem
                {
                    Item = inventory[index].Item,
                    Quantity = inventory[index].Quantity - ingredient.Quantity
                };
                inventory[index] = updatedItem;
            }
        }
        InventoryManager.UpdateInventoryAction(inventory);
    }

    public void ToggleCraftingUI()
    {
        _craftingUI.SetActive(!_craftingUI.activeSelf);   
    }

    private int GetCurrentRecipeIndex()
    {
        for (int i = 0; i < _recipeList.Length; i++)
        {
            if (_recipeList[i].Name == _loadedRecipe.Name) return i;
        }
        Debug.LogError("Recipe index not found");
        return 0;
    }

    public void OnClick_CraftItem()
    {
        if (!CanCraftRecipe()) return;
        DepleteRecipeResources();
        InventoryManager.AddItemAction(_loadedRecipe.CraftedItem, _loadedRecipe.Quantity);
    }

    public void OnClick_CycleRecipesLeft()
    {
        int index = GetCurrentRecipeIndex();
        if (index == 0)
            LoadCraftingRecipe(_recipeList[_recipeList.Length - 1]);
        else
            LoadCraftingRecipe(_recipeList[index - 1]);
    }

    public void OnClick_CycleRecipesRight()
    {
        int index = GetCurrentRecipeIndex();
        if (index == _recipeList.Length - 1)
            LoadCraftingRecipe(_recipeList[0]);
        else
            LoadCraftingRecipe(_recipeList[index + 1]);
    }
}
