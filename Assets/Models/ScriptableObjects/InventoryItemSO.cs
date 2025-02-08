using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "InventoryItem", menuName = "InventoryItem", order = 0)]
public class InventoryItemSO : ScriptableObject {
    public enum ItemType{
        None,
        Food,
        Drink,
        Equipment,
        Material
        // add more types here as and when needed
    }
    public int maxStackSize;
    public string itemName;
    public string description;
    public int value;
    public bool isUsable;
    public bool isStackable;
    public bool isEdible;
    public bool isDrinkable;
    public bool isEquipable;
    public bool isFlammable;
    public float massPerUnit;
    public HashSet<ItemType> itemTypes;
    public List<ScriptableObject> itemEffects;
    public Sprite icon;
}

[CreateAssetMenu(fileName = "FoodEffect", menuName = "ItemEffects/FoodEffect", order = 0)]
public class FoodEffect : ScriptableObject{

    public string effectName;
    public int health;
    public float saturation;
    public int stamina;
    public float hunger;
    public int thirst;
    public int happiness;
    public int energy;
    public float eatTime;
}

[CreateAssetMenu(fileName = "DrinkEffect", menuName = "ItemEffects/DrinkEffect", order = 0)]
public class DrinkEffect : ScriptableObject{
    public int health;
    public int stamina;
    public int hunger;
    public int thirst;
    public int happiness;
    public int energy;
}

[CreateAssetMenu(fileName = "EquipmentEffect", menuName = "ItemEffects/EquipmentEffect", order = 0)]
public class EquipmentEffect : ScriptableObject{
    public int durability;
    public int damage;
    public int armour;
    public int attackSpeed;
    public int movementSpeed;
}

// more scriptable objects for different types of items to store in a list in the inventory scriptable object