using UnityEngine;
public class MoveToButtonController : ContextMenuButtonController
{
    public override void OnClick()
    {
        Debug.Log($"Moving to tile: {contextMenuController.tile.Type} at co-ords: {contextMenuController.tile.GlobalPosX}, {contextMenuController.tile.GlobalPosY}");
        PlayerMovementController playerMovementController = GameObject.Find("Player").GetComponent<PlayerMovementController>();
        playerMovementController.MoveToTile(contextMenuController.tile);
    }
}