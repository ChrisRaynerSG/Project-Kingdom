using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
public class BuildController : MonoBehaviour {

    public Button buildWallButton;
    public Button bulldozeButton;

    bool isBuilding = false;
    bool isBulldozing = false;

    void Update(){
        if(Input.GetMouseButtonUp(1)){ // right click
            isBuilding = false;
            isBulldozing = false;
        }
        if(Input.GetKeyDown(KeyCode.Escape)){ // escape key
            isBuilding = false;
            isBulldozing = false;
        }
    }

    public void ModifyTileDetail(List<Tile> tiles){
        if(isBuilding){
            foreach(Tile tile in tiles){
                if(tile.HasTileDetail){
                    Debug.Log("Tile already has a detail");
                //return; // don't build on top of existing tile details
            }
            else{
                if(isBuilding){
                    tile.HasTileDetail = true;
                    tile.TileDetailData.SetTileDetailType(TileDetail.TileDetailType.Wall); // to trigger the event, could this be done better?
                    // need to instantiate a new tile detail controller maybe with observer pattern?
                    }
                }
            }
        }
        if(isBulldozing){
            foreach(Tile tile in tiles){
                if(tile.HasTileDetail){
                    tile.HasTileDetail = false;
                    tile.TileDetailData.SetTileDetailType(TileDetail.TileDetailType.None);
                }
            }
        }
    }

    private void OnEnable(){
        // MouseController.onTileClicked += BuildWall;
        buildWallButton.onClick.AddListener(() => {isBuilding = true; isBulldozing = false;});
        bulldozeButton.onClick.AddListener(() => {isBulldozing = true; isBuilding = false;});
        MouseController.onTilesSelected += ModifyTileDetail;

    }
    private void OnDisable(){
        MouseController.onTilesSelected -= ModifyTileDetail;
    }
}