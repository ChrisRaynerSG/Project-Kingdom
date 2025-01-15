using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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

        // need to update this to remove chunks that are outside the view distance

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
    private void GenerateWorld(){
        for(int x = -3; x <= 3; x++){
            for(int y = -3; y <= 3; y++){
                Vector2Int chunkPosition = new Vector2Int(x, y);
                GenerateChunk(chunkPosition, x, y);
            }
        }
    }
    private Vector2Int GetPlayerChunkPosition(){
        return new Vector2Int(Mathf.FloorToInt(playerTransform.position.x / World.chunkSize), Mathf.FloorToInt(playerTransform.position.y / World.chunkSize));
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

        Chunk chunk;

        if(activeChunks.TryGetValue(chunkPosition, out chunk) == false){
            return null;
        }

        // fix negative tile and chunk positions
        if(tilePosition.x < 0){
            tilePosition.x = World.chunkSize + tilePosition.x;
            chunkPosition.x -= 1;
        }
        if(tilePosition.y < 0){
            tilePosition.y = World.chunkSize + tilePosition.y;
            chunkPosition.y -= 1;
        }

        chunk = world.GetChunkFromCoordinates(chunkPosition);
        return chunk.Tiles[tilePosition.x, tilePosition.y];
    }

    public Chunk GetChunkFromGlobalPosition(Vector2Int globalPosition){
        Vector2Int chunkPosition = new Vector2Int(globalPosition.x / World.chunkSize, globalPosition.y / World.chunkSize);
        Chunk chunk;
        if(globalPosition.x < 0){
            chunkPosition.x -= 1;
        }
        if(globalPosition.y < 0){
            chunkPosition.y -= 1;
        }
        if(activeChunks.TryGetValue(chunkPosition, out chunk) == false){
            return null;
        }
        return chunk;
    }

    private void GenerateChunk(Vector2Int chunkPosition, int x, int y){
        Chunk chunk = new Chunk(chunkPosition);
        world.cachedChunks.Add(chunkPosition, chunk);
        activeChunks.Add(chunkPosition, chunk);
        GameObject newChunk = Instantiate(chunkPrefab, new Vector3(x,y,0), Quaternion.identity, transform);
        newChunk.name = $"Chunk {chunkPosition.x} {chunkPosition.y}";
        newChunk.GetComponent<ChunkController>().Initialise(chunk);

        //need to go over tileDetail again to update adjacent rocks/walls etc may cause issues with tiles in adjacent chunks this could also be a massive performance hit? Tested it, it is...
        // what would be a better way to do this?
        foreach(Tile t in chunk.Tiles){
            if (t.HasTileDetail){
                TileDetail td = t.TileDetailData;
                if(td.Type == TileDetail.TileDetailType.Rock){
                    td.Type = TileDetail.TileDetailType.Rock;
                }
            }
        }
        //check if any adjacent chunks have rocks and update the tiles
        List<Chunk> chunks = new List<Chunk>();
        for(int i = 0; i<8; i++){
            int offsetX = 0;
            int offsetY = 0;
            switch(i){
                case 0:
                    offsetX = -1;
                    offsetY = 1;
                    break;
                case 1:
                    offsetX = 0;
                    offsetY = 1;
                    break;
                case 2:
                    offsetX = 1;
                    offsetY = 1;
                    break;
                case 3:
                    offsetX = 1;
                    offsetY = 0;
                    break;
                case 4:
                    offsetX = 1;
                    offsetY = -1;
                    break;
                case 5:
                    offsetX = 0;
                    offsetY = -1;
                    break;
                case 6:
                    offsetX = -1;
                    offsetY = -1;
                    break;
                case 7:
                    offsetX = -1;
                    offsetY = 0;
                    break;
                default:
                    break;
            }
            if(activeChunks.TryGetValue(new Vector2Int(chunkPosition.x + offsetX, chunkPosition.y + offsetY), out Chunk c)){
                chunks.Add(c);
            }
        }
        foreach(Chunk c in chunks){
            foreach(Tile t in c.Tiles){ //could change this so only looking at the edge tiles
                if (t.HasTileDetail){
                    TileDetail td = t.TileDetailData;
                    if(td.Type == TileDetail.TileDetailType.Rock){
                        td.Type = TileDetail.TileDetailType.Rock;
                    }
                }
            }
        }
    }     
}