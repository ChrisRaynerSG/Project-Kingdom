using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ExamineItemController : MonoBehaviour {

    public static bool ExamineMenuOpen = false;
    public TextMeshProUGUI tileName;
    public TextMeshProUGUI tileDescription;
    public TextMeshProUGUI tileCoordinates;
    public TextMeshProUGUI tileChunk;
    public GameObject tileDetailContainer;
    public TextMeshProUGUI tileDetailItem;
    public TextMeshProUGUI tileDetailDescription;
    public TextMeshProUGUI tileDetailHitpoints;
    public GameObject tileDetailAttributesPrefab;
    public GameObject tileDetailAttributeContainer;
    private List<GameObject> tileDetailAttributes = new List<GameObject>();
    public GameObject ExamineMenu;

    public void ExamineTile(Tile tile)
    {
        // Destroy any existing attributes
        DestroyTileAttributes();
        ShowExamineMenu();

        if (tile.HasTileDetail)
        {
            tile.TileDetailData.OnTileDetailHitPointsChanged += ShowTileDetailData;
            ShowTileDetailData(tile.TileDetailData);
        }
        else if (tile.HasInventoryItem)
        {
            tile.inventoryItem.OnInventoryItemChanged += ShowInventoryItemData;
            tileDetailContainer.SetActive(true);
            ShowInventoryItemData(tile.inventoryItem);
        }
        else
        {
            tileDetailContainer.SetActive(false);
        }

        tileName.text = $"{tile.Type} Tile";
        tileDescription.text = tile.DescriptionLong;
        ShowChunkData(tile);
    }

    public void ExamineTileDetail(Tile tile)
    {

        DestroyTileAttributes();
        ShowExamineMenu();

        tileName.text = $"{tile.TileDetailData.name}";
        tileDescription.text = tile.TileDetailData.descriptionLong;
        ShowChunkData(tile);

        tileDetailContainer.SetActive(true);
        ShowTileDataShort(tile);
    }

    

    public void ExamineInventoryItem(Tile tile)
    {

        DestroyTileAttributes();
        ShowExamineMenu();

        tileName.text = $"{tile.inventoryItem.Item.name}";
        tileDescription.text = tile.inventoryItem.Item.description;
        ShowChunkData(tile);

        tileDetailContainer.SetActive(true);
        ShowTileDataShort(tile);
    }

    public void Update(){
        {
            CloseExamineMenu();
        }

    }

    private void ShowExamineMenu()
    {
        ExamineMenu.SetActive(true);
        ExamineMenuOpen = true;
    }

    public void CloseExamineMenu()
    {
        if(((!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonDown(0)) || Input.GetKeyDown(KeyCode.Escape)) && ExamineMenuOpen){
            ExamineMenu.SetActive(false);
            ExamineMenuOpen = false;
            DestroyTileAttributes();
        }
    }

    private void DestroyTileAttributes()
    {
        if (tileDetailAttributes.Count > 0)
        {
            foreach (GameObject attribute in tileDetailAttributes)
            {
                Destroy(attribute);
            }
            tileDetailAttributes.Clear();
        }
    }

    private void ShowChunkData(Tile tile)
    {
        tileCoordinates.text = $"Co-ordinates: {tile.GlobalPosX}, {tile.GlobalPosY}";
        Chunk chunk = WorldController.Instance.GetChunkFromGlobalPosition(new Vector2Int(tile.GlobalPosX, tile.GlobalPosY));
        tileChunk.text = $"Chunk: {chunk.position.x}, {chunk.position.y}";
    }

    private void ShowTileDataShort(Tile tile)
    {
        tileDetailItem.text = $"Tile: {tile.Type.ToString()} Tile";
        tileDetailDescription.text = tile.DescriptionShort;
        tileDetailHitpoints.text = "";
    }

    private void ShowTileDetailData(TileDetail tileDetail)
    {
        tileDetailContainer.SetActive(true);
        tileDetailItem.text = $"Detail: {tileDetail.name}";
        tileDetailDescription.text = tileDetail.descriptionShort;
        tileDetailHitpoints.text = $"Hitpoints: {tileDetail.CurrentHitPoints:F0}/{tileDetail.MaxHitPoints:F0}";

        DestroyTileAttributes(); // Destroy any existing attributes

        if (tileDetail.isHarvestable){
            GameObject attribute = Instantiate(tileDetailAttributesPrefab, tileDetailAttributeContainer.transform);
            attribute.GetComponent<TextMeshProUGUI>().text = "Harvestable";
            tileDetailAttributes.Add(attribute);
        }
        if (tileDetail.isChoppable){
            GameObject attribute = Instantiate(tileDetailAttributesPrefab, tileDetailAttributeContainer.transform);
            attribute.GetComponent<TextMeshProUGUI>().text = "Choppable";
            tileDetailAttributes.Add(attribute);
        }
        if (tileDetail.isMineable){
            GameObject attribute = Instantiate(tileDetailAttributesPrefab, tileDetailAttributeContainer.transform);
            attribute.GetComponent<TextMeshProUGUI>().text = "Mineable";
            tileDetailAttributes.Add(attribute);
        }
        if (tileDetail.IsTraversable){
            GameObject attribute = Instantiate(tileDetailAttributesPrefab, tileDetailAttributeContainer.transform);
            attribute.GetComponent<TextMeshProUGUI>().text = "Traversable";
            tileDetailAttributes.Add(attribute);
        }

        if(tileDetail.CurrentHitPoints <= 0){ // if the tile has been destroyed
            if(tileDetail.TileData.inventoryItem != null){
                tileDetail.TileData.inventoryItem.OnInventoryItemChanged += ShowInventoryItemData;
                ShowInventoryItemData(tileDetail.TileData.inventoryItem);
            }
            else{
                tileDetailContainer.SetActive(false);
            }
        }
    }

    private void ShowInventoryItemData(InventoryItem inventoryItem)
    {
        tileDetailItem.text = $"{inventoryItem.Item.name}";
        tileDetailDescription.text = inventoryItem.Item.description;
        tileDetailHitpoints.text = $"Quantity: {inventoryItem.Quantity}";

        DestroyTileAttributes(); // Destroy any existing attributes

        if (inventoryItem.Item.isEdible)
        {
            GameObject attribute = Instantiate(tileDetailAttributesPrefab, tileDetailAttributeContainer.transform);
            attribute.GetComponent<TextMeshProUGUI>().text = "Edible";
            tileDetailAttributes.Add(attribute);
        }
        if (inventoryItem.Item.isEquipable)
        {
            GameObject attribute = Instantiate(tileDetailAttributesPrefab, tileDetailAttributeContainer.transform);
            attribute.GetComponent<TextMeshProUGUI>().text = "Equipable";
            tileDetailAttributes.Add(attribute);
        }
        if (inventoryItem.Item.isUsable)
        {
            GameObject attribute = Instantiate(tileDetailAttributesPrefab, tileDetailAttributeContainer.transform);
            attribute.GetComponent<TextMeshProUGUI>().text = "Usable";
            tileDetailAttributes.Add(attribute);
        }
        if (inventoryItem.Item.isDrinkable)
        {
            GameObject attribute = Instantiate(tileDetailAttributesPrefab, tileDetailAttributeContainer.transform);
            attribute.GetComponent<TextMeshProUGUI>().text = "Drinkable";
            tileDetailAttributes.Add(attribute);
        }
        if (inventoryItem.Item.isStackable)
        {
            GameObject attribute = Instantiate(tileDetailAttributesPrefab, tileDetailAttributeContainer.transform);
            attribute.GetComponent<TextMeshProUGUI>().text = "Stackable";
            tileDetailAttributes.Add(attribute);
        }
        if(inventoryItem.Quantity <= 0){ // hide the tile detail container if the item has been destroyed/used/consumed
            tileDetailContainer.SetActive(false);
        }
    }
}