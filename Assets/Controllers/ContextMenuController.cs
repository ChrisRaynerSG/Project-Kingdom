using System.Numerics;
using UnityEngine;

public class ContextMenuController : MonoBehaviour {

    public GameObject contextMenuPrefab;
    public GameObject contextMenuInstance;
    public Tile tile;

    public void Initialise(Tile tile){
        contextMenuInstance = Instantiate(contextMenuPrefab, transform);
        this.tile = tile;
    }
    public void ShowContextMenu(){
        contextMenuInstance.SetActive(true);
    }
    public void HideContextMenu(){
        contextMenuInstance.SetActive(false);
    }
    public void Update(){
        if(Input.GetMouseButtonDown(1)){
            ShowContextMenu();
        }
        if(Input.GetMouseButtonDown(0)){
            HideContextMenu();
        }
    }
    
}