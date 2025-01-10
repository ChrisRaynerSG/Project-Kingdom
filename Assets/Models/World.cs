using Unity.VisualScripting;

public class World{
    public const int chunkSize = 16;
    public int seed = 0;
    
    public static World Instance { get; private set; }

       

    private World(){

    }

    /// <summary>
    /// Returns the instance of the World class
    /// </summary>
    /// <returns></returns>
    public static World GetInstance(){
        if(Instance == null){
            CreateWorld();
        }
        return Instance;
    }

    /// <summary>
    /// Creates a new instance of the World class
    /// </summary>
    private static void CreateWorld(){
        if(Instance != null){
            return;
        }
        else{
            Instance = new World();
        }
    }
}