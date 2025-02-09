using UnityEngine;
using System.Collections.Generic;


public class World{
    public const int chunkSize = 16;
    public static int Seed {get; set;} = 0;

    public int ViewDistance {get; set;} = 1;
    
    public static World Instance { get; private set; }

    public Dictionary<Vector2Int, Chunk> cachedChunks {get; private set;}

    private World(){
        cachedChunks = new Dictionary<Vector2Int, Chunk>();
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

    public Chunk GetChunkFromCoordinates(Vector2Int position){
        if(cachedChunks.ContainsKey(position)){
            return cachedChunks[position];
        }
        else{
            //Debug.LogError($"Chunk not found at position: {position.x} {position.y}");
            return null;
        } 
    }
}