using System.Numerics;
using TMPro;
using UnityEngine;

public class ContextMenuController : MonoBehaviour {

    public GameObject contextMenuButtonPrefab;
    private Tile tile;
    private TileDetail tileDetail;

    public void Initialise(Tile tile)
    {
        this.tile = tile;
        tileDetail = tile.TileDetailData;
        SetupButtons();
        
    }

    private void SetupButtons()
    {
        GameObject examineTileButton = Instantiate(contextMenuButtonPrefab, transform);
        examineTileButton.GetComponentInChildren<TextMeshProUGUI>().text = "Examine Tile";
        GameObject moveToTileButton = Instantiate(contextMenuButtonPrefab, transform);
        moveToTileButton.GetComponentInChildren<TextMeshProUGUI>().text = "Move To Tile";

        if (tile.Type == Tile.TileType.Water)
        {
            Debug.Log("Water tile");
            GameObject drinkWaterButton = Instantiate(contextMenuButtonPrefab, transform);
            drinkWaterButton.GetComponentInChildren<TextMeshProUGUI>().text = "Drink Water";
            //fish button only if player has fishing rod
            GameObject fishButton = Instantiate(contextMenuButtonPrefab, transform);
            fishButton.GetComponentInChildren<TextMeshProUGUI>().text = "Fish";
            //fill button only if player has container
            GameObject fillContainerButton = Instantiate(contextMenuButtonPrefab, transform);
            fillContainerButton.GetComponentInChildren<TextMeshProUGUI>().text = "Fill Container";

            // may need more buttons here if extra things can be done with water tiles
        }

        // how to put a line break in the context menu here?

        if (tileDetail.Type != TileDetail.TileDetailType.None)
        {
            GameObject examineTileDetailButton = Instantiate(contextMenuButtonPrefab, transform);
            examineTileDetailButton.GetComponentInChildren<TextMeshProUGUI>().text = "Examine " + tileDetail.name;
            if (tileDetail.isHarvestable)
            {
                // harvest button only if player has correct tool?
                GameObject harvestButton = Instantiate(contextMenuButtonPrefab, transform);
                harvestButton.GetComponentInChildren<TextMeshProUGUI>().text = "Harvest " + tileDetail.name;
            }
            if (tileDetail.isInteractable)
            {
                GameObject interactButton = Instantiate(contextMenuButtonPrefab, transform);
                interactButton.GetComponentInChildren<TextMeshProUGUI>().text = "Use " + tileDetail.name;
            }
            // may need more buttons here for different types of tile details
        }

        // another line break here.

        if (tile.HasInventoryItem)
        {
            InventoryItem inventoryItem = tile.inventoryItem;

            GameObject examineInventoryItemButton = Instantiate(contextMenuButtonPrefab, transform);
            examineInventoryItemButton.GetComponentInChildren<TextMeshProUGUI>().text = "Examine " + inventoryItem.Item.itemName;

            GameObject pickUpInventoryItemButton = Instantiate(contextMenuButtonPrefab, transform);
            pickUpInventoryItemButton.GetComponentInChildren<TextMeshProUGUI>().text = "Pick Up " + inventoryItem.Item.itemName;

            if (inventoryItem.Item.isUsable)
            {
                GameObject useButton = Instantiate(contextMenuButtonPrefab, transform);
                useButton.GetComponentInChildren<TextMeshProUGUI>().text = "Use " + inventoryItem.Item.itemName;
            }
            if (inventoryItem.Item.isEdible)
            {
                GameObject eatButton = Instantiate(contextMenuButtonPrefab, transform);
                eatButton.GetComponentInChildren<TextMeshProUGUI>().text = "Eat " + inventoryItem.Item.itemName;
            }
            if (inventoryItem.Item.isDrinkable)
            {
                GameObject drinkButton = Instantiate(contextMenuButtonPrefab, transform);
                drinkButton.GetComponentInChildren<TextMeshProUGUI>().text = "Drink " + inventoryItem.Item.itemName;
            }
            if (inventoryItem.Item.isEquipable)
            {
                GameObject equipWeaponButton = Instantiate(contextMenuButtonPrefab, transform);
                equipWeaponButton.GetComponentInChildren<TextMeshProUGUI>().text = "Equip " + inventoryItem.Item.itemName;
            }
            // may need more buttons here for different types of inventory items
        }
    }
    // public void ShowContextMenu(){
    //     contextMenuInstance.SetActive(true);
    // }
    // public void HideContextMenu(){
    //     contextMenuInstance.SetActive(false);
    // }
    public void Update(){
    //     if(Input.GetMouseButtonDown(1)){
    //         ShowContextMenu();
    //     }
    //     if(Input.GetMouseButtonDown(0)){
    //         HideContextMenu();
    //     }
    }
    
}