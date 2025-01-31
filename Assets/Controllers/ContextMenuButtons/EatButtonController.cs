using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class EatButtonController : ContextMenuButtonController
{
    PlayerHungerController playerHungerController;
    PlayerMovementController playerMovementController;
    public override void OnClick()
    {
        playerMovementController = GameObject.Find("Player").GetComponent<PlayerMovementController>();
        playerHungerController = GameObject.Find("Player").GetComponent<PlayerHungerController>();

        float distance = Vector2.Distance(playerMovementController.playerTransform.position, new Vector2(contextMenuController.tile.GlobalPosX, contextMenuController.tile.GlobalPosY));
        if(distance > 1.5){
            
            while(distance >= 2){ // move to adjacent tile if not adjacent
                playerMovementController.MoveToTile(contextMenuController.tile);
                distance = Vector2.Distance(playerMovementController.playerTransform.position, new Vector2(contextMenuController.tile.GlobalPosX, contextMenuController.tile.GlobalPosY));   
            }
        }
        //then eat food
        FoodEffect foodEffect = contextMenuController.tile.inventoryItem.Item.itemEffects.OfType<FoodEffect>().FirstOrDefault();
        playerHungerController.EatFood(contextMenuController.tile.inventoryItem);
        Debug.Log($"Eating: {contextMenuController.tile.inventoryItem.Item.name}\nAdding hunger: {foodEffect.hunger}\nAdding saturation: {foodEffect.saturation}");
        contextMenuController.tile.inventoryItem.RemoveQuantity(1);
    }
}