using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class EatButtonController : ContextMenuButtonController
{
    PlayerHungerController playerHungerController;
    public override void OnClick()
    {
        playerHungerController = GameObject.Find("Player").GetComponent<PlayerHungerController>();
        FoodEffect foodEffect = contextMenuController.tile.inventoryItem.Item.itemEffects.OfType<FoodEffect>().FirstOrDefault();
        playerHungerController.EatFood(contextMenuController.tile.inventoryItem);
        Debug.Log($"Eating: {contextMenuController.tile.inventoryItem.Item.name}\nAdding hunger: {foodEffect.hunger}\nAdding saturation: {foodEffect.saturation}");
        contextMenuController.tile.inventoryItem.RemoveQuantity(1);
    }
}