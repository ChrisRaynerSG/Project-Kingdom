using System;
using UnityEngine;
using UnityEngine.UI;
public class ExamineButtonController : ContextMenuButtonController
{
    
    private ExamineItemController examineItemController;

    public override void OnClick()
    {
        examineItemController = GameObject.Find("Player").GetComponent<ExamineItemController>();
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
        examineItemController.ExamineTile(contextMenuController.tile);
        Debug.Log($"Examing tile: {contextMenuController.tile.Type}\nCo-ords: {contextMenuController.tile.GlobalPosX}, {contextMenuController.tile.GlobalPosY}");
    }
    private void ExamineTileDetail()
    {
        examineItemController.ExamineTileDetail(contextMenuController.tile);
        Debug.Log($"Examing tile detail: {contextMenuController.tile.TileDetailData.name}\nDescription: {contextMenuController.tile.TileDetailData.descriptionShort}");
    }

    private void ExamineInventoryItem()
    {
        examineItemController.ExamineInventoryItem(contextMenuController.tile);
        Debug.Log($"Examing inventory item: {contextMenuController.tile.inventoryItem.Item.name}\nDescription: {contextMenuController.tile.inventoryItem.Item.description}");
    }
}