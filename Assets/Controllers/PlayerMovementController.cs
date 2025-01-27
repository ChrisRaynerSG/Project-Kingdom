using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    PlayerMovement playerMovementData;
    Transform playerTransform;
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
        playerTransform.position = newPosition;
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
