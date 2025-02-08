using UnityEngine;
public class ChopButtonController : ContextMenuButtonController{

    PlayerMovementController playerMovementController;
    TileDetailController tileDetailController;

    public override void OnClick(){
        PlayerMovementController playerMovementController = GameObject.Find("Player").GetComponent<PlayerMovementController>();
        Tile tile = contextMenuController.tile;
        tile.TileDetailData.TileDetailController.ChopTree(tile,playerMovementController);
    }
}