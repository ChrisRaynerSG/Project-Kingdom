using UnityEngine;

public class DrinkTileWaterButtonController : ContextMenuButtonController
{
    public override void OnClick()
    {
        PlayerMovementController playerMovementController = GameObject.Find("Player").GetComponent<PlayerMovementController>();
        PlayerThirstController playerThirstController = GameObject.Find("Player").GetComponent<PlayerThirstController>();
        playerThirstController.DrinkWater(contextMenuController.tile, playerMovementController, 10f);
        
    }
}