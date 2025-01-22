using System.Collections.Generic;
using UnityEngine;
public class TileDetailController : MonoBehaviour{

    [SerializeField] Sprite tileDetailSprite;
    [SerializeField] GameObject InventoryItemPrefab;
    SpriteRenderer sr;
    public TileDetail TileDetailData { get; set; }
    public void Initialise(TileDetail tileDetailData){
        TileDetailData = tileDetailData;
        sr = GetComponent<SpriteRenderer>();
        HandleTileDetailType(TileDetailData);
        TileDetailData.OnTileDetailPropertyChanged += TileDetailTypeChanged;
        TileDetailData.OnTileHitpointsZero += TileDetailDestroyed;
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
    private void TileDetailTypeChanged(TileDetail tileDetail)
    {
        HandleTileDetailType(tileDetail);

        if (!tileDetail.IsTraversable)
        {
            AddCollider();
        }
        else
        {
            RemoveCollider();
        }

    }

    private void HandleTileDetailType(TileDetail tileDetail)
    {
        switch (tileDetail.Type)
        {
            case TileDetail.TileDetailType.Bush:
                sr.material.color = Color.green;
                sr.sprite = Resources.Load<Sprite>("Sprites/Bushes32x32_06");
                break;
            case TileDetail.TileDetailType.Tree:
                sr.material.color = Color.green;
                sr.sprite = SpriteLoader.GetInstance.BasicDetailSprites[2];
                break;
            case TileDetail.TileDetailType.Rock:
                sr.material.color = new Color(0.6f, 0.3f, 0.1f);
                SetWallTileSprite("WallTilesAdvanced_");
                break;
            case TileDetail.TileDetailType.None:
                sr.sprite = null;
                break;
            case TileDetail.TileDetailType.Wall:
                sr.material.color = new Color(0.3f, 0.15f, 0.05f);
                SetWallTileSprite("WallTilesAdvanced_");
                break;
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
        sr.sprite = SpriteLoader.GetInstance.WallTileDictionary.TryGetValue(wallSpriteName, out Sprite wallSprite) ? wallSprite : null;
    }
    public void TileDetailDestroyed(TileDetail td){
        //Debug.Log("Tile destroyed");
        if(td.isHarvestable){
            //if space in player or pawn?? inventory put in inventory
            //implement this later for now just drop on the ground
            //if(space in player or pawn inventory){}
            //else{
            if(td.DroppedItem.Quantity > 0){
            GameObject newItem = Instantiate(InventoryItemPrefab, new Vector3(td.TileData.GlobalPosX, td.TileData.GlobalPosY, 0), Quaternion.identity,transform.parent);
            newItem.GetComponent<InventoryItemController>().Initialise(td.DroppedItem);
            SpriteRenderer invItemSr = newItem.GetComponent<SpriteRenderer>();
            invItemSr.sortingLayerName="TileDetail";
            invItemSr.sprite = td.DroppedItem.Item.icon;
            }
            else{
                td.TileData.inventoryItem = null;
                td.DroppedItem = null;
            }
            // maybe change this to inventoryitem later
            //}
            // drop resources
        }
        td.Type = TileDetail.TileDetailType.None;
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