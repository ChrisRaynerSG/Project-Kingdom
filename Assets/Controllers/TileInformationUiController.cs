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
        if(tile.TileDetailData.Type != TileDetail.TileDetailType.None){
            tileInformationText.text = $"Tile: {tile.Type} tile \nCo-ords: {tile.GlobalPosX},{tile.GlobalPosY} \nTile Detail: {tile.TileDetailData.Type}";
        }
        else{
            tileInformationText.text = $"Tile: {tile.Type} tile \nCo-ords: {tile.GlobalPosX},{tile.GlobalPosY}";
        }
    }
}