using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    PlayerMovement playerMovementData;
    Transform playerTransform;
    public Rigidbody2D playerRigidbody;
    void Start()
    {
        playerMovementData = PlayerMovement.CreatePlayerMovement();
        playerTransform = transform;
        SetPlayerStartPosition();
    }
    void Update()
    {
        MovePlayer();
        Camera.main.transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, Camera.main.transform.position.z);
    }

    public void MovePlayer()
    {
        Vector3 newPosition = playerTransform.position;
        if(Input.GetAxis("Vertical") != 0)
        {
            newPosition.y += Input.GetAxis("Vertical") * playerMovementData.MovementSpeed * Time.deltaTime;
        }
        if(Input.GetAxis("Horizontal") != 0)
        {
            newPosition.x += Input.GetAxis("Horizontal") * playerMovementData.MovementSpeed * Time.deltaTime;
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
}
