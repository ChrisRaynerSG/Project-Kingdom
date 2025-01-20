using System.Collections.Generic;
using UnityEngine;
public class TileDetailController : MonoBehaviour{

    Sprite[] basicTiles; 
    //Dictionary<string,Sprite> wallTiles;// needs changing later when we have more tile types
    [SerializeField] Sprite tileDetailSprite;
    [SerializeField] GameObject InventoryItemPrefab;
    SpriteRenderer sr;
    public TileDetail TileDetailData { get; set; }
    public void Initialise(TileDetail tileDetailData){

        TileDetailData = tileDetailData;
        sr = GetComponent<SpriteRenderer>();
        if(TileDetailData.Type == TileDetail.TileDetailType.Bush){
            sr.material.color = new Color(0f,0.6f,0f);
            sr.sprite = Resources.Load<Sprite>("Sprites/Bushes32x32_06");
            tileDetailData.IsTraversable = true;
        }
        else if(TileDetailData.Type == TileDetail.TileDetailType.Tree){
            sr.material.color = new Color(0f,0.3f,0f);
            sr.sprite = SpriteLoader.GetInstance.BasicDetailSprites[2];
            tileDetailData.IsTraversable = false;
            tileDetailData.isHarvestable = true;
            tileDetailData.Description = "A tree, can be harvested for wood";

        }
        else if(TileDetailData.Type == TileDetail.TileDetailType.Rock){
            sr.material.color = new Color(0.6f, 0.3f, 0.1f);
            //sr.sprite = SpriteLoader.GetInstance.WallTileDictionary.TryGetValue("WallTilesAdvanced_", out Sprite wallSprite) ? wallSprite : null;
            SetWallTileSprite("WallTilesAdvanced_");
            tileDetailData.IsTraversable = false;
            // GetWallTile(tileDetailData, "WallTilesAdvanced_");
        }
        else if(TileDetailData.Type == TileDetail.TileDetailType.Wall){
            sr.material.color = new Color(0.3f, 0.15f, 0.05f);
            SetWallTileSprite("WallTilesAdvanced_");
            tileDetailData.IsTraversable = false;
        }
        else if(TileDetailData.Type == TileDetail.TileDetailType.None){
            sr.sprite = null;
            tileDetailData.IsTraversable = true;
        }
        TileDetailData.OnTileDetailPropertyChanged += TileDetailTypeChanged;
        TileDetailData.OnTileHitpointsZero += TileDestroyed;
        sr.sortingLayerName = "TileDetail";

        if(!tileDetailData.IsTraversable){
            AddCollider();
        }
    }
    private void OnDisable(){
        if(TileDetailData != null){
            TileDetailData.OnTileDetailPropertyChanged -= TileDetailTypeChanged;
        }
    }
    private void TileDetailTypeChanged(TileDetail tileDetail){
        //Debug.Log("Tile detail type changed");

        switch(tileDetail.Type){
            case TileDetail.TileDetailType.Bush:
                sr.material.color = Color.green;
                sr.sprite = Resources.Load<Sprite>("Sprites/Bushes32x32_06");
                tileDetail.IsTraversable = true;
                break;
            case TileDetail.TileDetailType.Tree:
                sr.material.color = Color.green;
                sr.sprite = basicTiles[2];
                tileDetail.IsTraversable = true;
                break;
            case TileDetail.TileDetailType.Rock:
                sr.material.color = new Color(0.6f, 0.3f, 0.1f);
                SetWallTileSprite("WallTilesAdvanced_");
                tileDetail.IsTraversable = false;
                tileDetail.Description = tileDetail.Description;
                break;
            case TileDetail.TileDetailType.None:
                sr.sprite = null;
                tileDetail.IsTraversable = true;
                tileDetail.TileData.HasTileDetail = false;
                break;
            case TileDetail.TileDetailType.Wall:
                sr.material.color = new Color(0.3f, 0.15f, 0.05f);
                SetWallTileSprite("WallTilesAdvanced_");
                tileDetail.IsTraversable = false;
                tileDetail.Description = tileDetail.Description;
                break;
        }
        if(!tileDetail.IsTraversable){
            AddCollider();
        }
        else{
            RemoveCollider();
        }
    }

    private void SetWallTileSprite(string wallSpriteName)
    {
        // string wallSpriteName = "WallTilesAdvanced_"
        string nameBeforeNumbers = wallSpriteName;
        TileDetail[] adjacentTileDetails = TileDetailsUtils.GetAdjacentTileDetails(TileDetailData.TileData);
        for (int i = 0; i < 8; i++)
        {
            //Debug.Log($"{adjacentTileDetails[i].TileData.GlobalPosX},{adjacentTileDetails[i].TileData.GlobalPosY}");
            // 0 2 4 6 are the corner pieces, 1 3 5 7 are cardinal directions, can ignore the co
            if (adjacentTileDetails[i] != null)
            {
                // Debug.Log($"Adjacent tile detail type: {adjacentTileDetails[i].Type} TilePos: {adjacentTileDetails[i].TileData.GlobalPosX},{adjacentTileDetails[i].TileData.GlobalPosY}");
                if (adjacentTileDetails[i].Type == TileDetail.TileDetailType.Wall || adjacentTileDetails[i].Type == TileDetail.TileDetailType.Rock)
                {
                    wallSpriteName += i;
                    //Debug.Log("Wall sprite name: "+wallSpriteName);
                }
            }
        }
        wallSpriteName = TileDetailsUtils.CleanWallSpriteName(wallSpriteName, nameBeforeNumbers);
        //47 combinations of wall sprites how to get the right one?
        sr.sprite = SpriteLoader.GetInstance.WallTileDictionary.TryGetValue(wallSpriteName, out Sprite wallSprite) ? wallSprite : null;
    }

    public void TileDestroyed(TileDetail td){
        //Debug.Log("Tile destroyed");
        if(td.isHarvestable){
            //if space in player or pawn?? inventory put in inventory
            //implement this later for now just drop on the ground
            //if(space in player or pawn inventory){}
            //else{
            GameObject newItem = Instantiate(InventoryItemPrefab, new Vector3(td.TileData.GlobalPosX, td.TileData.GlobalPosY, 0), Quaternion.identity,transform.parent);
            newItem.GetComponent<InventoryItemController>().Initialise(td.DroppedItem);
            SpriteRenderer invItemSr = newItem.GetComponent<SpriteRenderer>();
            invItemSr.sortingLayerName="TileDetail";
            invItemSr.sprite = td.DroppedItem.Item.icon; // maybe change this to inventoryitem later
            //}
            // drop resources
        }
        td.Type = TileDetail.TileDetailType.None;
        td.IsTraversable = true;
        BuildController.UpdateAdjacentTiles(td.TileData);
    }

    private void AddCollider(){
        if(GetComponent<BoxCollider2D>() == null){
            gameObject.AddComponent<BoxCollider2D>();
        }
    }
    private void RemoveCollider(){
        if(GetComponent<BoxCollider2D>() != null){
            Destroy(GetComponent<BoxCollider2D>());
        }
    }
}