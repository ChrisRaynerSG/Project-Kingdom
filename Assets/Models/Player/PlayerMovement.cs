using Unity.VisualScripting;

public class PlayerMovement{

    static PlayerMovement Instance;
    public float MovementSpeed {get; set;} = 4.0f;
    public bool IsMoving {get; set;} = false;
    public bool IsSufferingFromStatusEffect {get; set;} = false;

    protected PlayerMovement(){
        MovementSpeed = 4.0f;
        IsMoving = false;
        IsSufferingFromStatusEffect = false;
        Instance = this;
    }

    public static PlayerMovement CreatePlayerMovement(){

        if(Instance != null){
            return Instance;
        }
        return new PlayerMovement();
    }
}