using UnityEngine;
using System.Linq;
using System.Collections;

public class EatButtonController : ContextMenuButtonController
{
    PlayerHungerController playerHungerController;
    PlayerMovementController playerMovementController;
    
    public override void OnClick()
    {
        playerMovementController = GameObject.Find("Player").GetComponent<PlayerMovementController>();
        playerHungerController = GameObject.Find("Player").GetComponent<PlayerHungerController>();
        playerHungerController.DoCoroutineMethods(contextMenuController.tile, playerMovementController);
    }
}