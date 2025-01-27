using System;
using UnityEngine;

public class InventoryItem{
    public InventoryItemSO Item {get; private set;}
    public static event Action<InventoryItem> OnInventoryItemChanged;
    public int Quantity {get; private set;}
    public float TotalMass => Quantity * Item.massPerUnit;
    public float TotalValue => Quantity * Item.value;
    public bool isInInventory = false; 
    //If the item is on the floor this will be false, allows for later moving the 
    //player to the item if it is on the floor and the player is out of range and 
    //wants to interact with it
    public Tile tile;
    //If the item is on the floor this will be the tile it is on
    //If the item is in the inventory this will be null
    
    public InventoryItem(InventoryItemSO item, int quantity, Tile tile){
        Item = item;
        Quantity = quantity;
        this.tile = tile;
        tile.inventoryItem = this;
        tile.HasInventoryItem = true;
    }

    public void AddQuantity(int amount){
        if(Item.isStackable == false){
            Quantity = 1;
            return;
        }
        if(Quantity + amount > Item.maxStackSize){
            Quantity = Item.maxStackSize;
            // int remaining = Quantity + amount - Item.MaxStackSize;
            OnInventoryItemChanged?.Invoke(this);
            // create a new InventoryItem with the remaining amount should I be doing this here or should the controller be doing this? I think the controller should be doing this...
            //new InventoryItem(Item, remaining);
            return;
        }
        Quantity += amount;
        OnInventoryItemChanged?.Invoke(this);
    }

    public void RemoveQuantity(int amount){
        Quantity -= amount;
        if(Quantity <= 0){
            Quantity = 0;
        }
        OnInventoryItemChanged?.Invoke(this);
    }
    // what other methods do I need here?
    // I need a method to use the item
    // I need a method to equip the item
    // I need a method to drop the item
    // I need a method to destroy the item
    // I need a method to split the stack
    // I need a method to merge the stack

    // I need a method to get the item description
    // I need a method to get the item value
    // I need a method to get the item type
    // I need a method to get the item name
    // I need a method to get the item mass
    // I need a method to get the item stack size
    // I need a method to get the item stackable status
    // I need a method to get the item edible status
    // I need a method to get the item drinkable status
    // I need a method to get the item equipable status
    // I need a method to get the item flammable status
    // I need a method to get the item types
}