using UnityEngine;
using UnityEngine.UI;
public abstract class ContextMenuButtonController : MonoBehaviour
{
    protected ContextMenuController contextMenuController;
    protected string buttonText;
    public void Initialise(ContextMenuController contextMenuController, string buttonText){
        this.contextMenuController = contextMenuController;
        this.buttonText = buttonText;
        gameObject.GetComponent<Button>().onClick.AddListener(OnClick);
    }
    
    public abstract void OnClick();
    
}