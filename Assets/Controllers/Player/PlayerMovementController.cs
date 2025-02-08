using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using System;
using System.Collections.Generic;
public class PlayerMovementController : MonoBehaviour
{
    public PlayerMovement playerMovementData;
    public Transform playerTransform;
    public Rigidbody2D playerRigidbody;

    public static event Action playerMoving;
    public static event Action playerNotMoving;

    public bool isMoving = false;

    void Start()
    {
        playerMovementData = PlayerMovement.CreatePlayerMovement();
        playerTransform = transform;
        SetPlayerStartPosition();
    }
    void Update()
    {
        MovePlayer();
        Camera.main.transform.position = new Vector3(playerTransform.position.x +0.5f, playerTransform.position.y +0.5f, Camera.main.transform.position.z);

    }

    public void MovePlayer()
    {
        Vector3 newPosition = playerTransform.position;
        if(Input.GetAxis("Vertical") != 0)
        {
            newPosition.y += Input.GetAxis("Vertical") * playerMovementData.MovementSpeed * Time.deltaTime;
            playerMoving?.Invoke();
            isMoving = true;
        }
        if(Input.GetAxis("Horizontal") != 0)
        {
            newPosition.x += Input.GetAxis("Horizontal") * playerMovementData.MovementSpeed * Time.deltaTime;
            playerMoving?.Invoke();
            isMoving = true;
        }
        if(Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0)
        {
            playerNotMoving?.Invoke();
            isMoving = false;
        }
        // how to slide along non-traversable tiles?
        // need to change this logic as its pretty bad, and causes the player to jitter when they are on the edge of a non-traversable tile
        Tile tileToCheck = WorldController.Instance.GetTileFromGlobalPosition(new Vector2Int((int)newPosition.x, (int)newPosition.y));

        if(tileToCheck.IsTraversable && tileToCheck.TileDetailData.IsTraversable){
            playerTransform.position = newPosition;
        }

    }

    public void SetPlayerStartPosition()
    {
        Tile tileToCheck = WorldController.Instance.GetTileFromGlobalPosition(new Vector2Int((int)playerTransform.position.x, (int)playerTransform.position.y));
        while(!tileToCheck.IsTraversable){
            playerTransform.position = new Vector3(playerTransform.position.x, playerTransform.position.y + 1, playerTransform.position.z);
            tileToCheck = WorldController.Instance.GetTileFromGlobalPosition(new Vector2Int((int)playerTransform.position.x, (int)playerTransform.position.y));
        }
    }

    public void MoveToTile(Tile tile){

        Debug.Log($"Moving to tile {tile.GlobalPosX}, {tile.GlobalPosY}");

        // Vector3 targetPosition = new Vector3(tile.GlobalPosX, tile.GlobalPosY, playerTransform.position.z);

        List<Tile> path = Pathfinder.findPath(WorldController.Instance.GetTileFromGlobalPosition(new Vector2Int((int)playerTransform.position.x, (int)playerTransform.position.y)), tile);
        // StartCoroutine(SmoothMove(playerTransform, targetPosition));
        if (path == null || path.Count == 0){
            Debug.LogWarning("No path found!");
            return;
        }
        StartCoroutine(FollowPath(path));
    }

    private IEnumerator SmoothMove(Transform playerTransform, Vector3 targetPosition){
        
        float elapsedTime = 0f;
        float distance = Vector3.Distance(playerTransform.position, targetPosition); 
        float duration = distance/playerMovementData.MovementSpeed;
        
        Vector3 startingPosition = playerTransform.position;

        while(elapsedTime < duration){
            Debug.Log($"Distance remaining: {distance}");
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            playerTransform.position = Vector3.Lerp(startingPosition, targetPosition, t);
            yield return null;
        }
        playerTransform.position = targetPosition;
    }
    private IEnumerator FollowPath(List<Tile> path)
    {
        Debug.Log("Following path");
        foreach (Tile tile in path)
        {
            Vector3 targetPosition = new Vector3(tile.GlobalPosX, tile.GlobalPosY, playerTransform.position.z);
            yield return StartCoroutine(SmoothMove(playerTransform, targetPosition));
        }
    }
}
