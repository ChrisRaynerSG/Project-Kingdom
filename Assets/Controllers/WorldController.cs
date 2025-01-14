using System;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour{

    public static WorldController Instance { get; private set; }
    [SerializeField] Transform playerTransform;
    [SerializeField] GameObject chunkPrefab;
    [SerializeField] int seed = 0;

    Dictionary<Vector2Int, Chunk> activeChunks = new Dictionary<Vector2Int, Chunk>();

    private World world;

    public void Awake(){
        if(Instance == null){
            Instance = this;
        }
        else{
            Destroy(gameObject);
        }
        world = World.GetInstance();
        World.Seed = seed;
    }

    public void Start(){
        GenerateWorld();
    }

    public void Update(){
        Vector2Int playerChunkPosition = GetPlayerChunkPosition();
        // foreach(KeyValuePair<Vector2Int, Chunk> chunk in activeChunks){
        //     if(Vector2Int.Distance(playerChunkPosition, chunk.Key) > world.ViewDistance){
        //         activeChunks.Remove(chunk.Key);
        //     }
        // }
        for(int x = playerChunkPosition.x - world.ViewDistance; x < playerChunkPosition.x + world.ViewDistance; x++){
            for(int y = playerChunkPosition.y - world.ViewDistance; y < playerChunkPosition.y + world.ViewDistance; y++){
                Vector2Int chunkPosition = new Vector2Int(x, y);
                if(activeChunks.ContainsKey(chunkPosition)){
                    continue;
                }
                else if(world.cachedChunks.ContainsKey(chunkPosition)){
                    activeChunks.Add(chunkPosition, world.cachedChunks[chunkPosition]);
                }
                else{
                    GenerateChunk(chunkPosition, x, y);
                }
            }
        }
    }

    // public void Update(){
    //     Chunk playerChunk = GetPlayerChunk();
    //     foreach(KeyValuePair<Vector2Int, Chunk> chunk in activeChunks){
    //         //if the chunk is not in the view distance, remove it
    //         if(Vector2Int.Distance(playerChunk.position, chunk.Key) > world.ViewDistance){
    //             activeChunks.Remove(chunk.Key);
    //         }
    //     }
    //     // create new chunks within the view distance
    //     for(int x = playerChunk.position.x - world.ViewDistance; x < playerChunk.position.x + world.ViewDistance; x++){
    //         for(int y = playerChunk.position.y - world.ViewDistance; y < playerChunk.position.y + world.ViewDistance; y++){
    //             Vector2Int chunkPosition = new Vector2Int(x, y);

    //             // id the chunk is cached, use the cached chunk, otherwise create a new chunk and add to cache
    //             if(world.cachedChunks.ContainsKey(chunkPosition)){
    //                 activeChunks.Add(chunkPosition, world.cachedChunks[chunkPosition]);
    //             }
    //             else{
    //                 Chunk chunk = new Chunk(chunkPosition);
    //                 activeChunks.Add(chunkPosition, chunk);
    //                 world.cachedChunks.Add(chunkPosition, chunk);
    //             }
    //         }
    //     }
    // }

    private void GenerateWorld(){
        for(int x = -3; x <= 3; x++){
            for(int y = -3; y <= 3; y++){
                Vector2Int chunkPosition = new Vector2Int(x, y);
                GenerateChunk(chunkPosition, x, y);
            }
        }
    }

    private Vector2Int GetPlayerChunkPosition(){
        return new Vector2Int(Mathf.FloorToInt(playerTransform.position.x / World.chunkSize), Mathf.FloorToInt(playerTransform.position.z / World.chunkSize));
    }

    public Tile GetTileFromMousePosition(Vector3 mousePosition){
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2Int chunkPosition = new Vector2Int(Mathf.FloorToInt(worldPosition.x / World.chunkSize), Mathf.FloorToInt(worldPosition.y / World.chunkSize));
        Vector2Int tilePosition = new Vector2Int(Mathf.FloorToInt(worldPosition.x % World.chunkSize), Mathf.FloorToInt(worldPosition.y % World.chunkSize));

        if(tilePosition.x < 0){
            tilePosition.x = World.chunkSize + tilePosition.x;
        }
        if(tilePosition.y < 0){
            tilePosition.y = World.chunkSize + tilePosition.y;
        }

        Chunk chunk = world.GetChunkFromCoordinates(chunkPosition);
        return chunk.Tiles[tilePosition.x, tilePosition.y];

    }

    public Tile GetTileFromGlobalPosition(Vector2Int globalPosition){
        
        
        Vector2Int chunkPosition = new Vector2Int(globalPosition.x / World.chunkSize, globalPosition.y / World.chunkSize);
        Vector2Int tilePosition = new Vector2Int(globalPosition.x % World.chunkSize, globalPosition.y % World.chunkSize);

        // fix negative tile and chunk positions
        if(tilePosition.x < 0){
            tilePosition.x = World.chunkSize + tilePosition.x;
            chunkPosition.x -= 1;
        }
        if(tilePosition.y < 0){
            tilePosition.y = World.chunkSize + tilePosition.y;
            chunkPosition.y -= 1;
        }

        Chunk chunk = world.GetChunkFromCoordinates(chunkPosition);
        return chunk.Tiles[tilePosition.x, tilePosition.y];
    }

    private void GenerateChunk(Vector2Int chunkPosition, int x, int y){
        Chunk chunk = new Chunk(chunkPosition);
                world.cachedChunks.Add(chunkPosition, chunk);
                activeChunks.Add(chunkPosition, chunk);
                GameObject newChunk = Instantiate(chunkPrefab, new Vector3(x,y,0), Quaternion.identity, transform);
                newChunk.name = $"Chunk {chunkPosition.x} {chunkPosition.y}";
                newChunk.GetComponent<ChunkController>().Initialise(chunk);
    }     
}