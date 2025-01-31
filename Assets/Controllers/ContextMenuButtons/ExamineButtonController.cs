using System;
using UnityEngine;
using UnityEngine.UI;
public class ExamineButtonController : ContextMenuButtonController
{
    
    public override void OnClick()
    {

        if(buttonText.Equals("Examine Tile"))
        {
            ExamineTile();
        }
        else if(buttonText.Equals($"Examine {contextMenuController.tile.TileDetailData.name}"))
        {
            ExamineTileDetail();
        }
        else if(buttonText.Equals($"Examine {contextMenuController.tile.inventoryItem.Item.name}"))
        {
            ExamineInventoryItem();
        }
    }

    private void ExamineTile()
    {
        Debug.Log($"Examing tile: {contextMenuController.tile.Type}\nCo-ords: {contextMenuController.tile.GlobalPosX}, {contextMenuController.tile.GlobalPosY}");
    }
    private void ExamineTileDetail()
    {
        Debug.Log($"Examing tile detail: {contextMenuController.tile.TileDetailData.name}\nDescription: {contextMenuController.tile.TileDetailData.description}");
    }

    private void ExamineInventoryItem()
    {
        Debug.Log($"Examing inventory item: {contextMenuController.tile.inventoryItem.Item.name}\nDescription: {contextMenuController.tile.inventoryItem.Item.description}");
    }
}