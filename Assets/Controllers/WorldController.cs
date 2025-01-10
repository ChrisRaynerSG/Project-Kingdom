using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour{

    public static WorldController Instance { get; private set; }
    [SerializeField] Transform playerTransform;
    [SerializeField] GameObject chunkPrefab;

    Dictionary<Vector2Int, Chunk> activeChunks = new Dictionary<Vector2Int, Chunk>();

    private World world;

    public void Awake(){
        world = World.GetInstance();
    }

    public void Start(){
        GenerateWorld();
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
        for(int x = 0; x < 1; x++){
            for(int y = 0; y < 1; y++){
                Vector2Int chunkPosition = new Vector2Int(x, y);
                Chunk chunk = new Chunk(chunkPosition);
                world.cachedChunks.Add(chunkPosition, chunk);
                activeChunks.Add(chunkPosition, chunk);
                GameObject newChunk = Instantiate(chunkPrefab, new Vector3(chunkPosition.x,chunkPosition.y,0), Quaternion.identity, transform);
                newChunk.name = $"Chunk {chunkPosition.x} {chunkPosition.y}";
                newChunk.GetComponent<ChunkController>().Initialise(chunk);
            }
        }
    }

    private Chunk GetPlayerChunk(){
        Vector2Int playerChunkPosition = new Vector2Int(Mathf.FloorToInt(playerTransform.position.x / World.chunkSize), Mathf.FloorToInt(playerTransform.position.z / World.chunkSize));
        return world.GetChunkFromCoordinates(playerChunkPosition);
    }
}