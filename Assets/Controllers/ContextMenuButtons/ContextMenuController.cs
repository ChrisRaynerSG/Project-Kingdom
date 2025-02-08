using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ContextMenuController : MonoBehaviour {

    public GameObject contextMenuButtonPrefab;
    public GameObject lineBreakPrefab;
    public Tile tile;
    private TileDetail tileDetail;

    public void Initialise(Tile tile)
    {
        this.tile = tile;
        tileDetail = tile.TileDetailData;
        SetupButtons();
        Resize();
        
    }

    private void SetupButtons()
    {
        GameObject examineTileButton = Instantiate(contextMenuButtonPrefab, transform);
        string examineButtonText = examineTileButton.GetComponentInChildren<TextMeshProUGUI>().text = "Examine Tile";
        ExamineButtonController ebc = examineTileButton.AddComponent<ExamineButtonController>();
        ebc.Initialise(this, examineButtonText);

        GameObject moveToTileButton = Instantiate(contextMenuButtonPrefab, transform);
        string moveToTileButtonText = moveToTileButton.GetComponentInChildren<TextMeshProUGUI>().text = "Move To Tile";
        MoveToButtonController mtbc = moveToTileButton.AddComponent<MoveToButtonController>();
        mtbc.Initialise(this, moveToTileButtonText);

        if (tile.Type == Tile.TileType.Water)
        {
            //drink water button
            GameObject drinkWaterButton = Instantiate(contextMenuButtonPrefab, transform);
            string drinkWaterButtonText = drinkWaterButton.GetComponentInChildren<TextMeshProUGUI>().text = "Drink Water";
            DrinkTileWaterButtonController drinkTileWaterButtonController = drinkWaterButton.AddComponent<DrinkTileWaterButtonController>();
            drinkTileWaterButtonController.Initialise(this, drinkWaterButtonText);

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

            PlaceLineBreak();

            GameObject examineTileDetailButton = Instantiate(contextMenuButtonPrefab, transform);
            string examineTileDetailText = examineTileDetailButton.GetComponentInChildren<TextMeshProUGUI>().text = "Examine " + tileDetail.name;
            ExamineButtonController etdbc = examineTileDetailButton.AddComponent<ExamineButtonController>();
            etdbc.Initialise(this, examineTileDetailText);
            
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
            if( tileDetail.isMineable){
                // mine button only if player has correct tool?
                GameObject mineButton = Instantiate(contextMenuButtonPrefab, transform);
                mineButton.GetComponentInChildren<TextMeshProUGUI>().text = "Mine " + tileDetail.name;


            }
            if( tileDetail.isChoppable){
                GameObject chopButton = Instantiate(contextMenuButtonPrefab, transform);
                string chopButtonText = chopButton.GetComponentInChildren<TextMeshProUGUI>().text = "Chop " + tileDetail.name;
                ChopButtonController chopButtonController = chopButton.AddComponent<ChopButtonController>();
                chopButtonController.Initialise(this, chopButtonText);

            }
            // may need more buttons here for different types of tile details
        }

        // another line break here.

        if (tile.HasInventoryItem && !tile.HasTileDetail)
        {
            PlaceLineBreak();
            InventoryItem inventoryItem = tile.inventoryItem;

            GameObject examineInventoryItemButton = Instantiate(contextMenuButtonPrefab, transform);
            string examineInventoryItemButtonText = examineInventoryItemButton.GetComponentInChildren<TextMeshProUGUI>().text = "Examine " + inventoryItem.Item.itemName;
            ExamineButtonController eiibc = examineInventoryItemButton.AddComponent<ExamineButtonController>();
            eiibc.Initialise(this, examineInventoryItemButtonText);

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
                string eatButtonText = eatButton.GetComponentInChildren<TextMeshProUGUI>().text = "Eat " + inventoryItem.Item.itemName;
                EatButtonController eatButtonController = eatButton.AddComponent<EatButtonController>();
                eatButtonController.Initialise(this, eatButtonText);
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

    public void Resize(){
        RectTransform rt = GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(rt.sizeDelta.x, (rt.childCount * 30)+10);
    }

    public void Update(){
 
    }

    private void PlaceLineBreak(){
        GameObject lineBreak = Instantiate(lineBreakPrefab, transform);

    }
    
}