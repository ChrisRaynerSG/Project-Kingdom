using UnityEngine;
using TMPro;
public class InventoryItemController : MonoBehaviour {

    public TextMeshPro countPrefab;
    public TextMeshPro countInstance;

    InventoryItem item;
    public void Initialise(InventoryItem inventoryItem){
        item = inventoryItem;
        if(item.Item.isStackable){
            countInstance = Instantiate(countPrefab, transform);
            UpdateCount();
            // if item is stackable then show the quantity if it is greater than 1
            // else if the quantity is 1 then don't show the quantity
        }
    }
    public void Update(){
        UpdateCount();
        if(item.Quantity<=0){
            item.tile.HasInventoryItem = false;
            item.tile.inventoryItem = null;
            Destroy(gameObject);
        }
    }

    private void UpdateCount()
    {
        if(item.Quantity>1){
            countInstance.text = item.Quantity.ToString();
            countInstance.gameObject.SetActive(true);

        }else{
            countInstance.gameObject.SetActive(false);
        }
    }
}