using UnityEngine;
using UnityEngine.UI;
public class ProgressBarController : MonoBehaviour{

    public Slider progressBar;
    
    public void Start(){
        TileDetailController.chopTimeProgress += UpdateProgressBar;
        PlayerThirstController.DrinkProgress += UpdateProgressBar;
    }

    public void Update(){
        if(progressBar.value >= 0.99){
            progressBar.gameObject.SetActive(false);
        }
        else if(progressBar.value <= 0.01){
            progressBar.gameObject.SetActive(false);
        }
        else{
            progressBar.gameObject.SetActive(true);
        }
    }

    public void UpdateProgressBar(float progress){
        progressBar.value = progress;
    }
}