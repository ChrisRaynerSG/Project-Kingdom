using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class BuildController : MonoBehaviour {

    public Button buildWallButton;
    public Button bulldozeButton;

    public static bool isBuildingWall = false;
    public static bool isBulldozing = false;

    public static bool isDoingNothing = true;

    public event Action onCancel;

    void Update(){
        if(Input.GetMouseButtonUp(1)){ // right click
            isBuildingWall = false;
            isBulldozing = false;
            isDoingNothing = true;
        }
        if(Input.GetKeyDown(KeyCode.Escape)){ // escape key
            isBuildingWall = false;
            isBulldozing = false;
            isDoingNothing = true;
        }
    }

    public void ModifyTileDetail(List<Tile> tiles){
        if(EventSystem.current.IsPointerOverGameObject()){
            // if the mouse is over a UI element, don't build
            return;
        }
        if(isBuildingWall){
            foreach(Tile tile in tiles){
                if(tile.HasTileDetail){
                    //Debug.Log("Tile already has a detail");
                //return; // don't build on top of existing tile details
                }
                else{
                    if(isBuildingWall)
                    {
                        tile.HasTileDetail = true;
                        tile.TileDetailData.Type = TileDetail.TileDetailType.Wall;
                        tile.TileDetailData.DescriptionShort = "A wall";
                        tile.TileDetailData.MaxHitPoints = 100;
                        UpdateAdjacentTiles(tile);
                    }
                }
            }
        }
        if(isBulldozing){
            foreach(Tile tile in tiles){
                if(tile.HasTileDetail){
                    tile.HasTileDetail = false;
                    tile.TileDetailData.Type = TileDetail.TileDetailType.None;
                    tile.TileDetailData.DescriptionShort = "No detail";
                    UpdateAdjacentTiles(tile);
                }
            }
        }
    }

    public static void UpdateAdjacentTiles(Tile tile) //maybe move this to a helper class?
    {
        for (int i = 0; i < 8; i++)
        {
            int x = 0;
            int y = 0;
            switch (i)
            {
                case 0:
                    x = -1; y = 1; break;
                case 1:
                    x = 0; y = 1; break;
                case 2:
                    x = 1; y = 1; break;
                case 3:
                    x = 1; y = 0; break;
                case 4:
                    x = 1; y = -1; break;
                case 5:
                    x = 0; y = -1; break;
                case 6:
                    x = -1; y = -1; break;
                case 7:
                    x = -1; y = 0; break;
                default:
                    x = 0; y = 0; break;
            }

            Tile adjacentTile = WorldController.Instance.GetTileFromGlobalPosition(new Vector2Int(tile.GlobalPosX + x, tile.GlobalPosY + y));
            if (adjacentTile != null)
            {
                if (adjacentTile.HasTileDetail)
                {
                    if (adjacentTile.TileDetailData.Type == TileDetail.TileDetailType.Wall)
                    {
                        adjacentTile.TileDetailData.Type = TileDetail.TileDetailType.Wall;
                    }
                    if (adjacentTile.TileDetailData.Type == TileDetail.TileDetailType.Rock)
                    {
                        adjacentTile.TileDetailData.Type = TileDetail.TileDetailType.Rock;
                    }
                }
            }
        }
    }

    private void OnEnable(){
        // add all different types of buildings here?
        // MouseController.onTileClicked += BuildWall;
        buildWallButton.onClick.AddListener(() => {isBuildingWall = true; isBulldozing = false;});
        bulldozeButton.onClick.AddListener(() => {isBulldozing = true; isBuildingWall = false;});
        MouseController.onTilesSelected += ModifyTileDetail;

    }
    private void OnDisable(){
        MouseController.onTilesSelected -= ModifyTileDetail;
    }
}