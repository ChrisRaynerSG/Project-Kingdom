using System.Collections.Generic;
using TMPro;
using UnityEngine;

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

        ExamineMenu.SetActive(true);
        ExamineMenuOpen = true;

        if (tile.HasTileDetail)
        {

            tileDetailContainer.SetActive(true);
            tileDetailItem.text = $"Detail: {tile.TileDetailData.name}";
            tileDetailDescription.text = tile.TileDetailData.descriptionShort;
            tileDetailHitpoints.text = $"Hitpoints: {tile.TileDetailData.CurrentHitPoints:0F}/{tile.TileDetailData.MaxHitPoints:0F}";

            if (tile.TileDetailData.isHarvestable)
            {
                GameObject attribute = Instantiate(tileDetailAttributesPrefab, tileDetailAttributeContainer.transform);
                attribute.GetComponent<TextMeshProUGUI>().text = "Harvestable";
                tileDetailAttributes.Add(attribute);
            }
            if (tile.TileDetailData.isChoppable)
            {
                GameObject attribute = Instantiate(tileDetailAttributesPrefab, tileDetailAttributeContainer.transform);
                attribute.GetComponent<TextMeshProUGUI>().text = "Choppable";
                tileDetailAttributes.Add(attribute);
            }
            if (tile.TileDetailData.isMineable)
            {
                GameObject attribute = Instantiate(tileDetailAttributesPrefab, tileDetailAttributeContainer.transform);
                attribute.GetComponent<TextMeshProUGUI>().text = "Mineable";
                tileDetailAttributes.Add(attribute);
            }
            if (tile.TileDetailData.IsTraversable)
            {
                GameObject attribute = Instantiate(tileDetailAttributesPrefab, tileDetailAttributeContainer.transform);
                attribute.GetComponent<TextMeshProUGUI>().text = "Traversable";
                tileDetailAttributes.Add(attribute);
            }
        }
        else if (tile.HasInventoryItem)
        {
            tileDetailContainer.SetActive(true);
            tileDetailItem.text = $"{tile.inventoryItem.Item.name}";
            tileDetailDescription.text = tile.inventoryItem.Item.description;
            tileDetailHitpoints.text = $"Quantity: {tile.inventoryItem.Quantity}";
            if (tile.inventoryItem.Item.isEdible)
            {
                GameObject attribute = Instantiate(tileDetailAttributesPrefab, tileDetailAttributeContainer.transform);
                attribute.GetComponent<TextMeshProUGUI>().text = "Edible";
                tileDetailAttributes.Add(attribute);
            }
            if (tile.inventoryItem.Item.isEquipable)
            {
                GameObject attribute = Instantiate(tileDetailAttributesPrefab, tileDetailAttributeContainer.transform);
                attribute.GetComponent<TextMeshProUGUI>().text = "Equipable";
                tileDetailAttributes.Add(attribute);
            }
            if (tile.inventoryItem.Item.isUsable)
            {
                GameObject attribute = Instantiate(tileDetailAttributesPrefab, tileDetailAttributeContainer.transform);
                attribute.GetComponent<TextMeshProUGUI>().text = "Usable";
                tileDetailAttributes.Add(attribute);
            }
            if (tile.inventoryItem.Item.isDrinkable)
            {
                GameObject attribute = Instantiate(tileDetailAttributesPrefab, tileDetailAttributeContainer.transform);
                attribute.GetComponent<TextMeshProUGUI>().text = "Drinkable";
                tileDetailAttributes.Add(attribute);
            }
            if (tile.inventoryItem.Item.isStackable)
            {
                GameObject attribute = Instantiate(tileDetailAttributesPrefab, tileDetailAttributeContainer.transform);
                attribute.GetComponent<TextMeshProUGUI>().text = "Stackable";
                tileDetailAttributes.Add(attribute);
            }
        }
        else
        {
            tileDetailContainer.SetActive(false);
        }

        tileName.text = $"{tile.Type.ToString()} Tile";
        tileDescription.text = tile.DescriptionLong;
        tileCoordinates.text = $"Co-ordinates: {tile.GlobalPosX}, {tile.GlobalPosY}";
        Chunk chunk = WorldController.Instance.GetChunkFromGlobalPosition(new Vector2Int(tile.GlobalPosX, tile.GlobalPosY));
        tileChunk.text = $"Chunk: {chunk.position.x}, {chunk.position.y}";
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

    public void ExamineTileDetail(Tile tile){

        DestroyTileAttributes();

        ExamineMenu.SetActive(true);
        ExamineMenuOpen = true;

        tileName.text = $"{tile.TileDetailData.name}";
        tileDescription.text = tile.TileDetailData.descriptionLong;
        tileCoordinates.text = $"Co-ordinates: {tile.GlobalPosX}, {tile.GlobalPosY}";
        Chunk chunk = WorldController.Instance.GetChunkFromGlobalPosition(new Vector2Int(tile.GlobalPosX, tile.GlobalPosY));
        tileChunk.text = $"Chunk: {chunk.position.x}, {chunk.position.y}";

        tileDetailContainer.SetActive(true);
        tileDetailItem.text = $"Tile: {tile.Type.ToString()} Tile";
        tileDetailDescription.text = tile.DescriptionShort;
        tileDetailHitpoints.text = "";
    }

    public void ExamineInventoryItem(Tile tile){

        DestroyTileAttributes();

        ExamineMenu.SetActive(true);
        ExamineMenuOpen = true;

        tileName.text = $"{tile.inventoryItem.Item.name}";
        tileDescription.text = tile.inventoryItem.Item.description;
        tileCoordinates.text = $"Co-ordinates: {tile.GlobalPosX}, {tile.GlobalPosY}";
        Chunk chunk = WorldController.Instance.GetChunkFromGlobalPosition(new Vector2Int(tile.GlobalPosX, tile.GlobalPosY));
        tileChunk.text = $"Chunk: {chunk.position.x}, {chunk.position.y}";

        tileDetailContainer.SetActive(true);
        tileDetailItem.text = $"Tile: {tile.Type.ToString()} Tile";
        tileDetailDescription.text = tile.DescriptionShort;
        tileDetailHitpoints.text = "";

    }

    public void Update(){
        if(Input.GetKeyDown(KeyCode.Escape) && ExamineMenuOpen){
            CloseExamineMenu();
        }
    }

    public void CloseExamineMenu()
    {
        ExamineMenu.SetActive(false);
        ExamineMenuOpen = false;
        DestroyTileAttributes();
    }
}