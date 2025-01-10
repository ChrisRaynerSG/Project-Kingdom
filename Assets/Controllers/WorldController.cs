using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour{

    public static WorldController Instance { get; private set; }
    [SerializeField] GameObject chunkPrefab;
    [SerializeField] Transform playerTransform;

    Dictionary<Vector2Int, Chunk> activeChunks = new Dictionary<Vector2Int, Chunk>();

    public void Awake(){
        World.GetInstance();
    }

    public void Start(){
        GenerateWorld();
    }

    private void GenerateWorld(){
        for(int x = 0; x < 1; x++){
            for(int y = 0; y < 1; y++){
                Vector2Int chunkPosition = new Vector2Int(x, y);
                Chunk chunk = new Chunk(chunkPosition);
                activeChunks.Add(chunkPosition, chunk);
            }
        }
    }
}