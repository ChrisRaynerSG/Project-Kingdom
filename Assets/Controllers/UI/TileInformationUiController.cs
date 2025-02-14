using TMPro;
using UnityEngine;

public class TileInformationUiController : MonoBehaviour
{
    public TextMeshProUGUI tileInformationText;

    public void OnEnable(){
        MouseController.onTileHoveredOver += UpdateTileInformation;
    }

    public void OnDisable(){
        MouseController.onTileHoveredOver -= UpdateTileInformation;
    }

    public void UpdateTileInformation(Tile tile){
        int chunkPosX = WorldController.Instance.GetChunkFromGlobalPosition(new Vector2Int(tile.GlobalPosX, tile.GlobalPosY)).position.x;
        int chunkPosY = WorldController.Instance.GetChunkFromGlobalPosition(new Vector2Int(tile.GlobalPosX, tile.GlobalPosY)).position.y;
        
        if(tile.TileDetailData.Type != TileDetail.TileDetailType.None){
            tileInformationText.text = $"Tile: {tile.Type} tile \nCo-ords: {tile.GlobalPosX},{tile.GlobalPosY} \nChunk: {chunkPosX},{chunkPosY}\nTile Detail: {tile.TileDetailData.Type} \nSprite: {tile.TileDetailData.name}" + 
            $"\nDescription: {tile.TileDetailData.DescriptionShort}\nHit Points: {tile.TileDetailData.CurrentHitPoints}/{tile.TileDetailData.MaxHitPoints}";
        }
        else{
            if(tile.inventoryItem != null){
                if(tile.inventoryItem.Item.isEdible){
                    

                }
                else{
                    tileInformationText.text = $"Tile: {tile.Type} tile \nCo-ords: {tile.GlobalPosX},{tile.GlobalPosY} \nChunk: {chunkPosX},{chunkPosY} \nTile Detail: None \nInventory Item: {tile.inventoryItem.Item.name} \nQuantity: {tile.inventoryItem.Quantity}";
                }
                

            }
            else{
                tileInformationText.text = $"Tile: {tile.Type} tile \nCo-ords: {tile.GlobalPosX},{tile.GlobalPosY} \nChunk: {chunkPosX},{chunkPosY} \nTile Detail: None";
            }
        }
    }
}