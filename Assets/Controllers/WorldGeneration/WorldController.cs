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
    Dictionary<Vector2Int, GameObject> chunkGameObjects = new Dictionary<Vector2Int, GameObject>();
    private World world;
    private bool isGeneratingChunks = false;
    private Vector2Int lastPlayerChunkPosition;

    private event Action OnChunksGenerated;

    public void Awake(){
        if(Instance == null){
            Instance = this;
        }
        else{
            Destroy(gameObject);
        }
        world = World.GetInstance();
        World.Seed = seed;
        OnChunksGenerated += () => UpdateConnectedTiles(lastPlayerChunkPosition, activeChunks[lastPlayerChunkPosition]);
    }

    public void Start(){
        SpriteLoader sl = SpriteLoader.GetInstance; // set up sprite loader when world is created so that sprites are loaded before chunks are generated
        GenerateWorld();
    }

    public void Update(){
        
        Vector2Int playerChunkPosition = GetPlayerChunkPosition();
        if(playerChunkPosition != lastPlayerChunkPosition){
            if(!isGeneratingChunks)
            {
                StartCoroutine(GenerateChunkCoroutine(playerChunkPosition));
            }

            List<Vector2Int> chunksToUnload = new List<Vector2Int>();
            foreach(KeyValuePair<Vector2Int, Chunk> chunk in activeChunks)
            {
                if (Mathf.Abs(playerChunkPosition.x - chunk.Key.x) > world.ViewDistance || 
                Mathf.Abs(playerChunkPosition.y - chunk.Key.y) > world.ViewDistance) 
                {
                    chunksToUnload.Add(chunk.Key);
                }
            }
            foreach(Vector2Int chunk in chunksToUnload)
            {
                unloadChunk(chunk);
            }
            lastPlayerChunkPosition = playerChunkPosition;
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

        int x = Mathf.FloorToInt(playerTransform.position.x / (float)World.chunkSize);
        int y = Mathf.FloorToInt(playerTransform.position.y / (float)World.chunkSize);
        return new Vector2Int(x, y);
        //return new Vector2Int(Mathf.FloorToInt(playerTransform.position.x / World.chunkSize), Mathf.FloorToInt(playerTransform.position.y / World.chunkSize));
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

    public Tile GetTileFromGlobalPosition(Vector2Int globalPosition)
    {
    
        Vector2Int chunkPosition = new Vector2Int(
            Mathf.FloorToInt((float)globalPosition.x / World.chunkSize),
            Mathf.FloorToInt((float)globalPosition.y / World.chunkSize)
        );

       
        Vector2Int tilePosition = new Vector2Int(
            globalPosition.x % World.chunkSize,
            globalPosition.y % World.chunkSize
        );

        
        if (tilePosition.x < 0)
        {
            tilePosition.x += World.chunkSize;
        }
        if (tilePosition.y < 0)
        {
            tilePosition.y += World.chunkSize;
        }

        
        if (!activeChunks.TryGetValue(chunkPosition, out Chunk chunk))
        {
            return null;
        }

        
        if (chunk.Tiles[tilePosition.x, tilePosition.y] == null)
        {
            return null;
        }
        else
        {
            return chunk.Tiles[tilePosition.x, tilePosition.y];
        }
    }

    public Chunk GetChunkFromGlobalPosition(Vector2Int globalPosition){
        int x = Mathf.FloorToInt(globalPosition.x / (float)World.chunkSize);
        int y = Mathf.FloorToInt(globalPosition.y / (float)World.chunkSize);
        Vector2Int chunkPosition = new Vector2Int(x, y);
        Chunk chunk;
        if(activeChunks.TryGetValue(chunkPosition, out chunk)){
            return chunk;
        }
        return null;
    }

    private void unloadChunk(Vector2Int chunkPosition){
        if(activeChunks.TryGetValue(chunkPosition, out Chunk chunk)){
            activeChunks.Remove(chunkPosition);
            if (chunkGameObjects.TryGetValue(chunkPosition, out GameObject chunkObj)){
                chunkObj.GetComponent<ChunkController>().UnloadChunk();
                ChunkObjectPool.Instance.ReturnObject(chunkObj);
                //Destroy(chunkObj);
                chunkGameObjects.Remove(chunkPosition);
            }
        }
    }

    private void GenerateChunk(Vector2Int chunkPosition, int x, int y){

        //Debug.Log($"cachedChunks count: {world.cachedChunks.Count}");

        if(activeChunks.ContainsKey(chunkPosition)){
            // chunk already exists
            return;
        }
        // if chunk is in cache make a gameobject for it
        else if(world.cachedChunks.TryGetValue(chunkPosition, out Chunk cachedChunk)){
            //Debug.Log("Generating chunk from cache");
            activeChunks.Add(chunkPosition, cachedChunk);
            GameObject newCachedChunk = ChunkObjectPool.Instance.GetObject(new Vector3(x, y, 0), transform);
            newCachedChunk.name = $"Chunk {chunkPosition.x} {chunkPosition.y}";
            newCachedChunk.GetComponent<ChunkController>().Initialise(cachedChunk);
            chunkGameObjects.Add(chunkPosition, newCachedChunk);
        }
        else
        {
            //Debug.Log("Generating new chunk");
            Chunk chunk = new Chunk(chunkPosition);
            world.cachedChunks.Add(chunkPosition, chunk);
            activeChunks.Add(chunkPosition, chunk);
            GameObject newChunk = ChunkObjectPool.Instance.GetObject(new Vector3(x, y, 0), transform);
            newChunk.name = $"Chunk {chunkPosition.x} {chunkPosition.y}";
            newChunk.GetComponent<ChunkController>().Initialise(chunk);
            chunkGameObjects.Add(chunkPosition, newChunk);
        }
        //need to go over tileDetail again to update adjacent rocks/walls etc may cause issues with tiles in adjacent chunks this could also be a massive performance hit? Tested it, it is...
        // what would be a better way to do this?
    }

    // to fix walls and rocks looking like chocolate mmm...
    private void UpdateConnectedTiles(Vector2Int chunkPosition, Chunk chunk)
    {
        RefreshTileDetails(chunk);
        //check if any adjacent chunks have rocks and update the tiles
        List<Chunk> chunks = new List<Chunk>();
        for (int i = 0; i < 8; i++)
        {
            int offsetX = 0;
            int offsetY = 0;
            switch (i)
            {
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
            if (activeChunks.TryGetValue(new Vector2Int(chunkPosition.x + offsetX, chunkPosition.y + offsetY), out Chunk c))
            {
                chunks.Add(c);
            }
        }
        foreach (Chunk c in chunks)
        {
            RefreshTileDetails(c);
        }
    }

    private void RefreshTileDetails(Chunk chunk)
    {
        foreach (Tile t in chunk.Tiles)
        {
            if (t.HasTileDetail)
            {
                TileDetail td = t.TileDetailData;
                if (td.Type == TileDetail.TileDetailType.Rock)
                {
                    td.Type = TileDetail.TileDetailType.Rock;
                }
                if (td.Type == TileDetail.TileDetailType.Wall)
                {
                    td.Type = TileDetail.TileDetailType.Wall;
                }
            }
        }
    }

    private IEnumerator GenerateChunkCoroutine(Vector2Int playerChunkPosition){

        isGeneratingChunks = true;

        for(int x = playerChunkPosition.x - world.ViewDistance; x <= playerChunkPosition.x + world.ViewDistance; x++){
            for(int y = playerChunkPosition.y - world.ViewDistance; y <= playerChunkPosition.y + world.ViewDistance; y++){
                Vector2Int chunkPosition = new Vector2Int(x, y);
                if(activeChunks.ContainsKey(chunkPosition)){
                    continue;
                }
                else{
                    GenerateChunk(chunkPosition, x, y);
                    yield return null;
                }
            }
        }
        isGeneratingChunks = false;
        OnChunksGenerated?.Invoke();
    }
}